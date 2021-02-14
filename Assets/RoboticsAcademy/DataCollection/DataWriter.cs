using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RoboticsAcademy.DataCollection
{
    /// <summary>
    /// Base class for creating an object that writes from a XR device into files/inference data.
    /// </summary>
    public abstract class DataWriter : MonoBehaviour, IDataCollectionHandler
    {
        // Event system for data collection.
        [SerializeField] DataCollectionSystem collectionSystem;

        // File related variables.
        [SerializeField] string fileName;
        string fullPathToFile;

        // StreamWriter for writing data.
        protected StreamWriter dataStream;

        // Queue of data for inference.
        Queue<List<float>> dataQueue;
        int queueLength;

        // Start time of the collection.
        DateTime startTime;

        /// <summary>
        /// Abstract code to initialize any data provider needed for collection.
        /// </summary>
        protected abstract bool InitializeDataProvider();

        /// <summary>
        /// Writing header for file.
        /// </summary>
        protected abstract void WriteHeader();

        /// <summary>
        /// Function executed on loop for writing specific data.
        /// </summary>
        protected abstract void WriteData();

        /// <summary>
        /// Whenever enabled, register to system.
        /// </summary>
        protected void OnEnable()
        {
            collectionSystem.RegisterDataCollectionHandler(this);
        }

        /// <summary>
        /// Whenever disabled, remove from system.
        /// </summary>
        protected void OnDisable()
        {
            collectionSystem.UnregisterDataCollectionHandler(this);
        }

        /// <summary>
        /// Create file and initialize data collection for specific data provider.
        /// </summary>
        public void OnDataCollectionStart(DataCollectionEventData eventData)
        {
            // Initialize data provider references/services.
            if (!InitializeDataProvider())
            {
                // If not successful, set false and stop execution.
                gameObject.SetActive(false);
                return;
            }

            // Initialize queue for inference data.
            InitializeInferenceQueue();

            // Create data stream with name and filepath to write data locally.
            fullPathToFile = Application.persistentDataPath;

            if (!Directory.Exists(fullPathToFile))
            {
                Directory.CreateDirectory(fullPathToFile);
            }

            fullPathToFile = Path.Combine(fullPathToFile, fileName);
            dataStream = new StreamWriter(fullPathToFile);

            // Write date on top of everything.
            startTime = DateTime.UtcNow;
            dataStream.WriteLine(startTime.ToString("MM-dd-yyyy_HH-mm-ss-fff"));

            // Write header.
            WriteHeader();
        }

        /// <summary>
        /// Housekeeping whenever data collection is stopped.
        /// </summary>
        public void OnDataCollectionStop(DataCollectionEventData eventData)
        {
            // Annihilate the stream.
            if (dataStream != null)
            {
                dataStream.Flush();
                dataStream.Close();
                dataStream = null;
            }
        }

        /// <summary>
        /// Event for whenever data is written.
        /// </summary>
        public void OnDataCollectionWrite(DataCollectionEventData eventData)
        {
            WriteData();
        }

        /// <summary>
        /// Event for whenever the data is going to be uploaded.
        /// </summary>
        public void OnDataCollectionUpload(DataCollectionEventData eventData)
        {
            // Upload current data file to the SSH server, and delete it locally.
            SSHServerManager.UploadFileToDir(fullPathToFile, eventData.uploadPath);
            //File.Delete(fullPathToFile);
        }

        /// <summary>
        /// Get seconds since beginning of data collection.
        /// </summary>
        protected double GetElapsedSeconds()
        {
            return (DateTime.UtcNow - startTime).TotalSeconds;
        }

        /// <summary>
        /// Initialize queue to get a stream of inference data.
        /// </summary>
        protected void InitializeInferenceQueue()
        {
            // Queue needs enough data for x seconds, and needs to be floating point.
            queueLength = (int)(collectionSystem.secondsOfDataInMemory / collectionSystem.secondsPerDataRecord);
            dataQueue = new Queue<List<float>>();

            // Init queue with garbage values to check for errors.
            for (int i = 0; i < queueLength; ++i)
            {
                dataQueue.Enqueue(new List<float> { float.MaxValue });
            }
        }

        /// <summary>
        /// Add to inference data queue.
        /// </summary>
        protected void AddToInferenceQueue(List<float> data)
        {
            // Enqueue data (dequeue data if it passes the max length).
            dataQueue.Enqueue(data);
            if (dataQueue.Count > queueLength)
            {
                dataQueue.Dequeue();
            }
        }

        /// <summary>
        /// Get data from queue.
        /// </summary>
        public List<float>[] GetInferenceQueueData()
        {
            // Convert to array.
            return dataQueue.ToArray();
        }
    }
}