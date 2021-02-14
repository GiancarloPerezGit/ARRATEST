using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.EventSystems;
using ViveSR.anipal.Eye;

namespace RoboticsAcademy.DataCollection
{
    /// <summary>
    /// Data writer for eye gaze data.
    /// </summary>
    [RequireComponent(typeof(SRanipal_Eye_Framework))]
    class EyeGazeWriter : DataWriter
    {
        // EyeWriter specific variables.
        [SerializeField] EventSystem eventSystem;
        [SerializeField] Camera raycastCamera;

        [SerializeField] LineRenderer line;

        static EyeData_v2 eyeData = new EyeData_v2();
        Dictionary<EyeShape, float> EyeWeightings = new Dictionary<EyeShape, float>();
        bool eye_callback_registered = false;

        /// <summary>
        /// Get eye gaze capability.
        /// </summary>
        protected override bool InitializeDataProvider()
        {
            // Get eye gaze provider service.
            if (!SRanipal_Eye_Framework.Instance.EnableEye) return false;
            else return true;
        }

        /// <summary>
        /// Write eye Gaze header.
        /// </summary>
        protected override void WriteHeader()
        {
            string header = "";
            foreach( var v in Enum.GetValues(typeof(EyeShape)))
            {
                header += v.ToString() + ",";
            }
            header += "FixationPointX,FixationPointY,FixationPointZ,FixationDirectionX,FixationDirectionY,FixationDirectionZ,FixationObject,SecondsElapsed";
            // Write header.
            dataStream.WriteLine(header);
        }

        /// <summary>
        /// Write eye Gaze data.
        /// </summary>
        protected override void WriteData()
        {
            if (SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.WORKING &&
            SRanipal_Eye_Framework.Status != SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT) return;

            if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == true && eye_callback_registered == false)
            {
                SRanipal_Eye_v2.WrapperRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
                eye_callback_registered = true;
            }
            else if (SRanipal_Eye_Framework.Instance.EnableEyeDataCallback == false && eye_callback_registered == true)
            {
                SRanipal_Eye_v2.WrapperUnRegisterEyeDataCallback(Marshal.GetFunctionPointerForDelegate((SRanipal_Eye_v2.CallbackBasic)EyeCallback));
                eye_callback_registered = false;
            }

            // Check if eyes are active.
            bool isLeftEyeActive = false;
            bool isRightEyeActive = false;
            if (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.WORKING)
            {
                isLeftEyeActive = eyeData.verbose_data.left.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY);
                isRightEyeActive = eyeData.verbose_data.right.GetValidity(SingleEyeDataValidity.SINGLE_EYE_DATA_GAZE_ORIGIN_VALIDITY);
            }
            else if (SRanipal_Eye_Framework.Status == SRanipal_Eye_Framework.FrameworkStatus.NOT_SUPPORT)
            {
                isLeftEyeActive = true;
                isRightEyeActive = true;
            }

            if (isLeftEyeActive || isRightEyeActive)
            {
                if (eye_callback_registered == true)
                    SRanipal_Eye_v2.GetEyeWeightings(out EyeWeightings, eyeData);
                else
                    SRanipal_Eye_v2.GetEyeWeightings(out EyeWeightings);
            }

            Vector3 GazeOriginCombinedLocal, GazeDirectionCombinedLocal;

            if (eye_callback_registered)
            {
                if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
                else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
                else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal, eyeData)) { }
                else return;
            }
            else
            {
                if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.COMBINE, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
                else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.LEFT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
                else if (SRanipal_Eye_v2.GetGazeRay(GazeIndex.RIGHT, out GazeOriginCombinedLocal, out GazeDirectionCombinedLocal)) { }
                else return;
            }

            Vector3 GazeDirectionCombined = Camera.main.transform.TransformDirection(GazeDirectionCombinedLocal);

            RaycastHit eyeGazeHit;
            string hitGameObject = "";

            PointerEventData data = new PointerEventData(eventSystem);

            raycastCamera.transform.position = Camera.main.transform.position;
            raycastCamera.transform.forward = GazeDirectionCombined.normalized;

            Vector3 centerPos = new Vector3(0.5f, 0.5f, 1.0f);
            data.position = raycastCamera.ViewportToScreenPoint(centerPos);

            Debug.Log("writing: " + raycastCamera.transform.rotation.eulerAngles);
            List<RaycastResult> results = new List<RaycastResult>();
            eventSystem.RaycastAll(data, results);

            line.positionCount = 2;
            line.SetPosition(0, raycastCamera.transform.position);
            line.SetPosition(1, data.position);

            foreach (var result in results)
            {
                Debug.Log(result.gameObject);
            }

            // Get hit to see object targeted by eye gaze.
            if (Physics.Raycast(Camera.main.transform.position, GazeDirectionCombined, out eyeGazeHit, Mathf.Infinity))
                hitGameObject = eyeGazeHit.transform.gameObject.name;

            // Create array of floats for inference data.
            List<float> modelData = new List<float>();

            // Write the weights for different expressions
            foreach (var v in (EyeShape[])Enum.GetValues(typeof(EyeShape)))
            {
                if (EyeWeightings.ContainsKey(v)) modelData.Add(EyeWeightings[v]);
                else modelData.Add(float.NaN);
            }

            // Write rest of data.
            modelData.AddRange(new float[]{
            eyeGazeHit.point.x,
            eyeGazeHit.point.y,
            eyeGazeHit.point.z,
            GazeDirectionCombined.normalized.x,
            GazeDirectionCombined.normalized.y,
            GazeDirectionCombined.normalized.z
            });

            // Create data string.
            string currentEyes = string.Format("{0},{1},{2}",
                                 string.Join(",", modelData),
                                 hitGameObject,
                                 GetElapsedSeconds());

            // Write data and Keep it in memory for inference.
            dataStream.WriteLine(currentEyes);
            AddToInferenceQueue(modelData);
        }

        private static void EyeCallback(ref EyeData_v2 eye_data)
        {
            eyeData = eye_data;
        }
    }
}