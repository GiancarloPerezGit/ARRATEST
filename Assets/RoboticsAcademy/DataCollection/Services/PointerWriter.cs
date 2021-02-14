using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections.Generic;
using UnityEngine;

namespace RoboticsAcademy.DataCollection
{
    /// <summary>
    /// Data writer for Pointer data.
    /// </summary>
    class PointerWriter : DataWriter, IMixedRealityPointerHandler
    {
        // Data structures to store pointers and their press state.
        Dictionary<IMixedRealityPointer, int> pointerDownStatus= new Dictionary<IMixedRealityPointer, int>();
        HashSet<IMixedRealityPointer> pointers = new HashSet<IMixedRealityPointer>();

        /// <summary>
        /// Register writer for both data collection events and pointer events.
        /// </summary>
        new void OnEnable()
        {
            base.OnEnable();

            // Register Handler to not need focusing.
            CoreServices.InputSystem.RegisterHandler<IMixedRealityPointerHandler>(this);
        }

        /// <summary>
        /// Unregister writer for both data collection events and pointer events.
        /// </summary>
        new void OnDisable()
        {
            base.OnDisable();

            // Remove Handler.
            CoreServices.InputSystem.UnregisterHandler<IMixedRealityPointerHandler>(this);
        }

        /// <summary>
        /// When pointer is pressed, change it's down status.
        /// </summary>
        public void OnPointerDown(MixedRealityPointerEventData eventData)
        {
            pointerDownStatus[eventData.Pointer] = 1;
        }

        /// <summary>
        /// When pointer is dragging.
        /// </summary>
        public void OnPointerDragged(MixedRealityPointerEventData eventData) { }

        /// <summary>
        /// When pointer is released, change it's down status.
        /// </summary>
        public void OnPointerUp(MixedRealityPointerEventData eventData)
        {
            pointerDownStatus[eventData.Pointer] = 0;
        }

        /// <summary>
        /// When pointer registers a click.
        /// </summary>
        public void OnPointerClicked(MixedRealityPointerEventData eventData) { }

        /// <summary>
        /// Get all pointers currently being used.
        /// </summary>
        private void GetCurrentPointers()
        {
            // Clear data structures if there is a change.
            pointers.Clear();
            pointerDownStatus.Clear();

            // Get all input sources and pointers within the sources.
            foreach (var inputSource in CoreServices.InputSystem.DetectedInputSources)
            {
                foreach (var pointer in inputSource.Pointers)
                {
                    // Add to data structures as long as they did not previously exist.
                    if (!pointers.Contains(pointer)) pointers.Add(pointer);
                    if (!pointerDownStatus.ContainsKey(pointer)) pointerDownStatus.Add(pointer, 0);
                }
            }
        }

        /// <summary>
        /// Initialize pointers.
        /// </summary>
        protected override bool InitializeDataProvider()
        {
            // Get all the pointers.
            GetCurrentPointers();

            // Check if there are any pointers.
            if (pointers.Count == 0)
            {
                Debug.LogWarning("No pointers!");
                return false;
            }
            else return true;
        }

        /// <summary>
        /// Write controller data header.
        /// </summary>
        protected override void WriteHeader()
        {
            // Write header.
            dataStream.WriteLine("PointerID,Handedness,PointerX,PointerY,PointerZ,PointerDown,PointerTarget,SecondsElapsed");
        }

        /// <summary>
        /// Write pointer data.
        /// </summary>
        protected override void WriteData()
        {
            // Get all pointers repeatedly in case there is a change.
            GetCurrentPointers();

            // Get all necessary pounters.
            if (pointers != null)
            {
                List<float> modelData = new List<float>();

                foreach (var pointer in pointers)
                {
                    if (pointer.IsInteractionEnabled && pointer.IsActive)
                    {
                        // Add necessary data.
                        float[] pointerData = new float[]
                        {
                        (int)pointer.PointerId,
                        (int)pointer.Controller.ControllerHandedness,
                        pointer.Position.x,
                        pointer.Position.y,
                        pointer.Position.z,
                        pointerDownStatus[pointer]
                        };

                        string target = "";
                        if (pointer.Result.CurrentPointerTarget != null) target = pointer.Result.CurrentPointerTarget.name;

                        // Create data string.
                        string currentPtr = string.Format("{0},{1},{2}",
                                                string.Join(",", pointerData),
                                                target,
                                                GetElapsedSeconds());

                        dataStream.WriteLine(currentPtr);

                        // Add all of the temp float list to the model data.
                        modelData.AddRange(pointerData);
                    }
                }

                // Write data and Keep it in memory for inference.
                AddToInferenceQueue(modelData);
            }
        }
    }
}
