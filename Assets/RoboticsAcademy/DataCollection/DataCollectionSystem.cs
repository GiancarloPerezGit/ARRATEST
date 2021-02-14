using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsAcademy.DataCollection
{ 
    /// <summary>
    /// Execute events with functions that can be hooked up to inputs.
    /// </summary>
    public class DataCollectionSystem : MonoBehaviour
    {
        // SSH server variables.
        [SerializeField] string host;
        [SerializeField] int port;
        [SerializeField] string username;
        [SerializeField] string password;

        // Time based variables.
        public float secondsPerDataRecord;
        public float secondsOfDataInMemory;

        // Collection flag.
        bool collecting = false;

        // Check if collecting.
        public bool getCollectionStatus() { return collecting; }

        // handlers to execute functions from.
        List<IDataCollectionHandler> handlers = new List<IDataCollectionHandler>();

        // event data to use.
        DataCollectionEventData data = new DataCollectionEventData();

        public void SetUploadPath(string path)
        {
            data.uploadPath = path;
        }

        /// <summary>
        /// Register GameObject scripts to the events.
        /// </summary>
        public void RegisterDataCollectionHandler(IDataCollectionHandler handler)
        {
            // Add to handlers list.
            handlers.Add(handler);
        }

        /// <summary>
        /// Unregister GameObject scripts to the events.
        /// </summary>
        public void UnregisterDataCollectionHandler(IDataCollectionHandler handler)
        {
            // Remove from handlers list.
            handlers.Remove(handler);
        }

        /// <summary>
        /// Function that can be called from input to start data collection.
        /// </summary>
        public void DataCollectionStart()
        {
            Debug.Log(Application.persistentDataPath);

            // Start all start functions and begin data write loop.
            collecting = true;
            InvokeDataCollectionEvent((x, y) => x.OnDataCollectionStart(y));
            StartCoroutine("DataWriteCoroutine");
        }

        /// <summary>
        /// Function that can be called from input to stop data collection/upload data.
        /// </summary>
        public void DataCollectionStop()
        {
            // Start all stop functions and stop data write loop.
            StopCoroutine("DataWriteCoroutine");
            Debug.Log("couroutine stopped");

            InvokeDataCollectionEvent((x, y) => x.OnDataCollectionStop(y));

            // Change collecting flag.
            collecting = false;
        }

        /// <summary>
        /// Execute event to write data.
        /// </summary>
        private void DataCollectionWrite()
        {
            // Execute write event on all handlers.
            InvokeDataCollectionEvent((x, y) => x.OnDataCollectionWrite(y));
        }

        /// <summary>
        /// Write data in a loop depending on the amount of time between records.
        /// </summary>
        private IEnumerator DataWriteCoroutine()
        {
            // Infinite loop delayed by user specified amount of seconds.
            while (true)
            {
                DataCollectionWrite();
                yield return new WaitForSeconds(secondsPerDataRecord);
            }
        }

        /// <summary>
        /// Execute data upload event.
        /// </summary>
        public void DataCollectionUpload()
        {
            // Open server.
            SSHServerManager.OpenServer(host, port, username, password);

            // Execute upload event on all handlers.
            InvokeDataCollectionEvent((x, y) => x.OnDataCollectionUpload(y));

            // Close server after finishing uploads.
            SSHServerManager.CloseServer();
        }

        /// <summary>
        /// Helper function to call all event handlers.
        /// </summary>
        private void InvokeDataCollectionEvent(Action<IDataCollectionHandler, DataCollectionEventData> func)
        {

            // For all the handlers, execute specified function for all scripts within.
            if (handlers.Count != 0)
            {
                foreach (IDataCollectionHandler handler in handlers)
                {
                    func(handler, data);
                }
            }
        }
    }
}