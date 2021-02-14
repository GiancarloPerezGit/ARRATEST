using UnityEngine.EventSystems;

namespace RoboticsAcademy.DataCollection
{
    public class DataCollectionEventData
    {
        public string uploadPath = "";
    }
    /// <summary>
    /// Interface to implement for subscribing to data collection events.
    /// </summary>
    public interface IDataCollectionHandler : IEventSystemHandler
    {
        /// <summary>
        /// Event Whenever data collection starts.
        /// </summary>
        void OnDataCollectionStart(DataCollectionEventData eventData);

        /// <summary>
        /// Event Whenever data collection ends.
        /// </summary>
        void OnDataCollectionStop(DataCollectionEventData eventData);

        /// <summary>
        /// Write Event adds data from service you choose to use.
        /// </summary>
        void OnDataCollectionWrite(DataCollectionEventData eventData);

        /// <summary>
        /// Event Whenever data collection is uploaded to a server.
        /// </summary>
        void OnDataCollectionUpload(DataCollectionEventData eventData);
    }
}