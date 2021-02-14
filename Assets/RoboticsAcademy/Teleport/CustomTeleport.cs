using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;
//using Leap.Unity;
namespace Microsoft.MixedReality.Toolkit.Teleport
{
    public class CustomTeleport : MonoBehaviour
    {
        public bool lesson;
        private GameObject pointer;
        private RaycastHit hit;
        private RaycastHit temp;
        private GameObject player;
        public GameObject cam;
        private GameObject lessonBot;
        private bool casting = false;
        private bool startTick = true;
        private bool toggle = true;
        private bool one = true;
        private bool two = false;
        public float angle = 45f;
        private GameObject cursor;
        private NoTelePointer nTP;
        private float deadZone = 0.4f;
        public Material pline;
        GameObject[] grids = new GameObject[2];
        //[SerializeField] private LeapXRServiceProvider leapXrServiceProvider;
        public void Start()
        {
            //leapXrServiceProvider.enabled = false;
            player = GameObject.FindGameObjectWithTag("PlayerCam");
            cam = GameObject.FindGameObjectWithTag("MainCamera");
        }
        public void castRaycast()
        {

            player = GameObject.FindGameObjectWithTag("PlayerCam");
            cam = GameObject.FindGameObjectWithTag("MainCamera");
            pointer = GameObject.FindGameObjectWithTag("TeleportHand");
            cursor = GameObject.FindGameObjectWithTag("Cursor");

            two = false;
            if (one)
            {
                nTP = pointer.GetComponent<NoTelePointer>();
                one = false;
            }
            if (nTP.ang.x == 0 && nTP.ang.y == 0)
            {

            }
            else if (nTP.ang.y > deadZone)
            {
                casting = true;
                two = true;
                Debug.Log("Test");
            }
            else if (nTP.ang.y > -deadZone && nTP.ang.y < deadZone && Mathf.Sign(nTP.ang.x) < 0)
            {
                player.transform.Rotate(new Vector3(0, -45, 0));
            }
            else if (nTP.ang.y > -deadZone && nTP.ang.y < deadZone && Mathf.Sign(nTP.ang.x) > 0)
            {
                player.transform.Rotate(new Vector3(0, 45, 0));
            }
            else if (nTP.ang.y < -deadZone)
            {

                if (UnityEngine.Physics.Raycast(cam.transform.position, -cam.transform.forward, out temp))
                {
                    if (temp.distance < 0.5)
                    {

                    }
                    else
                    {
                        //The playspace is the parent of the camera. Since the camera auto updates, 
                        //The height of the play area
                        float height = MixedRealityPlayspace.Position.y;
                        //Vector3 newPos = -cam.transform.forward + MixedRealityPlayspace.Position;
                        //The new position is a mix between the Playspace and the direction of the camera
                        Vector3 newPos = -cam.transform.forward + MixedRealityPlayspace.Position;
                        //Replace camera height with playspace height
                        newPos.y = height;
                        //Set playspace to new position
                        MixedRealityPlayspace.Position = newPos;

                    }
                }

            }

        }

        public void teleport()
        {
            foreach (GameObject teleGrid in grids)
            {
                teleGrid.SetActive(false);
            }
            casting = false;
            GameObject teleCursor = GameObject.FindGameObjectWithTag("TeleCursor");
            //RaycastHit temp2 = new RaycastHit();
            //if (!hit.Equals(temp2))
            //{
            //    Debug.Log(hit.point.x);
            //    Debug.Log(-1 * cam.transform.localPosition.x);
            //    Debug.Log(hit.point.z);
            //    Debug.Log(-1 * cam.transform.localPosition.z);

            //player.transform.position = new Vector3(hit.point.x + cam.transform.localPosition.x, 0, hit.point.z + cam.transform.localPosition.z);
            print("Tele");
            if (pointer.GetComponent<NoTelePointer>().Result.CurrentPointerTarget != null && two)
            {
                if (pointer.GetComponent<NoTelePointer>().Result.CurrentPointerTarget.tag != "Wall")
                {
                    print("Tele");
                    //player.transform.position = new Vector3(pointer.GetComponent<LineRenderer>().GetPosition(15).x + cam.transform.localPosition.x, 0, pointer.GetComponent<LineRenderer>().GetPosition(15).z + cam.transform.localPosition.z);
                    player.transform.position = new Vector3(teleCursor.transform.position.x + cam.transform.localPosition.x, teleCursor.transform.position.y, teleCursor.transform.position.z + cam.transform.localPosition.z);
                }

            }
            if (lesson)
            {
                Debug.Log("Test");
                player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, lessonBot.transform.rotation, 360);
                Debug.Log("Test");
            }



            //}
            cursor.SetActive(true);
            pointer.GetComponent<BaseMixedRealityLineRenderer>().enabled = false;
        }

        private void LateUpdate()
        {
            cam.transform.position = MixedRealityPlayspace.Position;
        }

        private void Update()
        {
            //UnityEngine.Debug.Log("Test");
            if(UnityEngine.Input.GetKeyDown("space"))
            {

            }
            if (startTick)
            {

                //arc = pointer.GetComponent<LineRenderer>();
                startTick = false;
                
                grids = GameObject.FindGameObjectsWithTag("TeleGrid");
                foreach(GameObject teleGrid in grids)
                {
                    teleGrid.SetActive(false);
                }
                
            }
            if (lesson && toggle)
            {
                if (GameObject.FindGameObjectWithTag("LessonRobot"))
                {

                    lessonBot = GameObject.FindGameObjectWithTag("LessonRobot");
                    toggle = false;
                }
            }
            if (casting)
            {
                player = GameObject.FindGameObjectWithTag("PlayerCam");
                cam = GameObject.FindGameObjectWithTag("MainCamera");
                pointer = GameObject.FindGameObjectWithTag("TeleportHand");
                foreach (GameObject teleGrid in grids)
                {
                    teleGrid.SetActive(true);
                }
                if (one)
                {
                    nTP = pointer.GetComponent<NoTelePointer>();
                    one = false;
                }
                if (cursor == null)
                {

                }
                if (cursor.activeSelf)
                {
                    cursor.SetActive(false);
                    pointer.GetComponent<BaseMixedRealityLineRenderer>().enabled = true;
                }
                if(pointer.GetComponent<NoTelePointer>().Result.CurrentPointerTarget != null)
                {
                    if(pointer.GetComponent<NoTelePointer>().Result.CurrentPointerTarget.tag != "TeleGrid" && pointer.GetComponent<NoTelePointer>().Result.CurrentPointerTarget.tag != "Floor")
                    {
                        pline.SetColor("_BaseColor", Color.red);
                    }
                    else
                    {
                        pline.SetColor("_BaseColor", Color.green);
                    }
                }
                Debug.Log("Test");
                //UpdatePath();
                if (UnityEngine.Physics.Raycast(pointer.transform.position, pointer.transform.TransformDirection(Vector3.forward), out temp, Mathf.Infinity))
                {
                    hit = temp;
                    //Debug.Log(hit.point.x + " / " + hit.point.z);
                }
                else
                {
                    hit = new RaycastHit();
                }

            }
        }
    }
}