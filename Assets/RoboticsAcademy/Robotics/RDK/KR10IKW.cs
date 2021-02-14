using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;

namespace ProjectRoboDK
{
    public class KR10IKW : MonoBehaviour
    {
        RoboDK RDK = null;
        RoboDK RDK2 = null;
        RoboDK.Item ROBOT = null;
        RoboDK.Item ROBOT2 = null;
        RoboDK.Item target = null;
        RoboDK.Item robFrame;
        RoboDK.Item toolFrame;
        RoboDK.Item robFrame2;
        RoboDK.Item toolFrame2;
        //List of the 6 axis
        public Transform A1;
        public Transform A2;
        public Transform A3;
        public Transform A4;
        public Transform A5;
        public Transform A6;
        //public GameObject bar1;
        //public GameObject bar2;
        //public GameObject bar3;
        //public GameObject bar4;
        //public GameObject bar5;
        //public GameObject bar6;
        //public TextMeshProUGUI a1Text;
        //public TextMeshProUGUI a2Text;
        //public TextMeshProUGUI a3Text;
        //public TextMeshProUGUI a4Text;
        //public TextMeshProUGUI a5Text;
        //public TextMeshProUGUI a6Text;
        private ButtonManagerScript bhs;
        public bool online;
        public bool startOff;
        private double[] currJoints;
        private GameObject world;
        private ArrayList actions = new ArrayList();
        private String prevCommand;
        //How much to move the end of the robot per button press. In mm.
        private double movstp = 10.0;
        //Default joints for the robot
        private double[] hom = new double[6] { 0, -90, 90, 0, 0, 0 };
        private bool roboActive = false;
        private bool successConnect = false;
        private GameObject[] axes;
        public float jogSpeed = 50;
        // Start is called before the first frame update
        void Awake()
        {
            try
            {
                
                UnityEngine.Debug.Log("Start");
                RDK = new RoboDK();
                successConnect = true;
                UnityEngine.Debug.Log("Dunno");
                    //RDK2 = new RoboDK("10.101.48.141", false, 20500);
                    RDK.Render(false);
                    //RDK2.Render(false);
                    ROBOT = RDK.getItem("KUKA KR 10 R1100 sixx");
                    robFrame = RDK.getItem("Tool 1");
                    //ROBOT2 = RDK2.getItem("KUKA KR 10 R1100 sixx");
                    //robFrame2 = RDK2.getItem("Tool 2");
                    ROBOT.setPoseTool(robFrame);
                    UnityEngine.Debug.Log("Dunno");
                    //RDK2 = new RoboDK("10.101.48.141", false, 20500);
                    //RDK.Render(false);
                    //RDK2.Render(false);
                    //ROBOT = RDK.getItem("KUKA KR 10 R1100 sixx");
                    //robFrame = RDK.getItem("Tool 1");
                    //ROBOT2 = RDK2.getItem("KUKA KR 10 R1100 sixx");
                    //robFrame2 = RDK2.getItem("Tool 2");
                    //ROBOT.setPoseTool(robFrame);
                    //ROBOT2.setPoseTool(robFrame2);
                    //UnityEngine.Debug.Log("Dunno");
                    //RDK2 = new RoboDK("10.101.48.141", false, 20500);
                    //RDK.Render(false);
                    //RDK2.Render(false);
                    //ROBOT = RDK.getItem("KUKA KR 10 R1100 sixx");
                    //robFrame = RDK.getItem("Tool 1");
                    //ROBOT2 = RDK2.getItem("KUKA KR 10 R1100 sixx");
                    //robFrame2 = RDK2.getItem("Tool 2");
                    //ROBOT.setPoseTool(robFrame);
                    roboActive = true;

                axes = GameObject.FindGameObjectsWithTag("LessonAxis");

                A1 = axes[0].transform;
                A2 = axes[1].transform;
                A3 = axes[2].transform;
                A4 = axes[3].transform;
                A5 = axes[4].transform;
                A6 = axes[5].transform;
                //A1 = GameObject.Find("[environment]/WorldBot/KUKA_KR10_R1100_AGILUS_0/robot_GRP/robot_GEOM/Base/axis1").transform;
                //A2 = GameObject.Find("[environment]/WorldBot/KUKA_KR10_R1100_AGILUS_0/robot_GRP/robot_GEOM/Base/axis1/axis2").transform;
                //A3 = GameObject.Find("[environment]/WorldBot/KUKA_KR10_R1100_AGILUS_0/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3").transform;
                //A4 = GameObject.Find("[environment]/WorldBot/KUKA_KR10_R1100_AGILUS_0/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3/axis4").transform;
                //A5 = GameObject.Find("[environment]/WorldBot/KUKA_KR10_R1100_AGILUS_0/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3/axis4/axis5").transform;
                //A6 = GameObject.Find("[environment]/WorldBot/KUKA_KR10_R1100_AGILUS_0/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3/axis4/axis5/axis6").transform;
                world = GameObject.Find("[environment]/WorldBot");
                bhs = GameObject.FindGameObjectWithTag("BMS").GetComponent<ButtonManagerScript>();
                
            }
            catch (Win32Exception e)
            {
                UnityEngine.Debug.Log("RoboDK is not installed");
            }
        }
        
        // Update is called once per frame
        public void simulMove(int xyzwpr, double signedMovstp)
        {
            
            ROBOT.setPoseTool(robFrame);
            double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
            move_xyzwpr[xyzwpr] = signedMovstp * bhs.jogSp/100f;
            Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
            Mat robot_pose = ROBOT.Pose();
            double[] olJoints = ROBOT.Joints();
            Mat new_robot_pose;
            new_robot_pose = robot_pose * movement_pose;
            ROBOT.setPose(new_robot_pose);
            double[] joints = ROBOT.Joints();
            UnityEngine.Debug.Log("SteamVR Button pressed!");
            UnityEngine.Debug.Log(joints[0]);
            UnityEngine.Debug.Log(joints[1]);
            UnityEngine.Debug.Log(joints[2]);
            UnityEngine.Debug.Log(joints[3]);
            UnityEngine.Debug.Log(joints[4]);
            UnityEngine.Debug.Log(joints[5]);
            //ROBOT.MoveJ(new_robot_pose, false);

            A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
            A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
            A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
            A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
            A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
            A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);
            //bar1.transform.localScale += new Vector3((float)olJoints[0] - (float)joints[0], 0, 0);
            //bar2.transform.localScale += new Vector3((float)joints[1] - (float)olJoints[1], 0, 0);
            //bar3.transform.localScale += new Vector3((float)joints[2] - (float)olJoints[2], 0, 0);
            //bar4.transform.localScale += new Vector3((float)olJoints[3] - (float)joints[3], 0, 0);
            //bar5.transform.localScale += new Vector3((float)joints[4] - (float)olJoints[4], 0, 0);
            //bar6.transform.localScale += new Vector3((float)olJoints[5] - (float)joints[5], 0, 0);
            float a1t = (float)joints[0];
            float a2t = (float)joints[1];
            float a3t = (float)joints[2];
            float a4t = (float)joints[3];
            float a5t = (float)joints[4];
            float a6t = (float)joints[5];

            a1t = Mathf.Round(a1t);
            a2t = Mathf.Round(a2t);
            a3t = Mathf.Round(a3t);
            a4t = Mathf.Round(a4t);
            a5t = Mathf.Round(a5t);
            a6t = Mathf.Round(a6t);

            //a1Text.SetText(a1t.ToString());
            //a2Text.SetText(a2t.ToString());
            //a3Text.SetText(a3t.ToString());
            //a4Text.SetText(a4t.ToString());
            //a5Text.SetText(a5t.ToString());
            //a6Text.SetText(a6t.ToString());
            ROBOT.setJoints(ROBOT.Joints());
        }
        public void realMove(int xyzwpr, double signedMovstp)
        {
            
            ROBOT.setPoseTool(robFrame);
            double[] move_xyzwpr = new double[6] { 0, 0, 0, 0, 0, 0 };
            move_xyzwpr[xyzwpr] = signedMovstp;
            Mat movement_pose = Mat.FromTxyzRxyz(move_xyzwpr);
            Mat robot_pose = ROBOT.Pose();
            double[] olJoints = ROBOT.Joints();
            Mat new_robot_pose;
            new_robot_pose = robot_pose * movement_pose;
            RDK.setRunMode(1);
            ROBOT.setPose(new_robot_pose);
            double[] joints = ROBOT.Joints();
            UnityEngine.Debug.Log("SteamVR Button pressed!");
            UnityEngine.Debug.Log(joints[0]);
            UnityEngine.Debug.Log(joints[1]);
            UnityEngine.Debug.Log(joints[2]);
            UnityEngine.Debug.Log(joints[3]);
            UnityEngine.Debug.Log(joints[4]);
            UnityEngine.Debug.Log(joints[5]);
            //ROBOT.MoveJ(new_robot_pose, false);

            A1.Rotate(0, (float)olJoints[0] - (float)joints[0], 0);
            A2.Rotate((float)joints[1] - (float)olJoints[1], 0, 0);
            A3.Rotate((float)joints[2] - (float)olJoints[2], 0, 0);
            A4.RotateAround(A4.position, A4.forward, (float)olJoints[3] - (float)joints[3]);
            A5.Rotate((float)joints[4] - (float)olJoints[4], 0, 0);
            A6.Rotate(0, 0, (float)olJoints[5] - (float)joints[5]);
            //bar1.transform.localScale += new Vector3((float)olJoints[0] - (float)joints[0], 0, 0);
            //bar2.transform.localScale += new Vector3((float)joints[1] - (float)olJoints[1], 0, 0);
            //bar3.transform.localScale += new Vector3((float)joints[2] - (float)olJoints[2], 0, 0);
            //bar4.transform.localScale += new Vector3((float)olJoints[3] - (float)joints[3], 0, 0);
            //bar5.transform.localScale += new Vector3((float)joints[4] - (float)olJoints[4], 0, 0);
            //bar6.transform.localScale += new Vector3((float)olJoints[5] - (float)joints[5], 0, 0);
            float a1t = (float)joints[0];
            float a2t = (float)joints[1];
            float a3t = (float)joints[2];
            float a4t = (float)joints[3];
            float a5t = (float)joints[4];
            float a6t = (float)joints[5];

            a1t = Mathf.Round(a1t);
            a2t = Mathf.Round(a2t);
            a3t = Mathf.Round(a3t);
            a4t = Mathf.Round(a4t);
            a5t = Mathf.Round(a5t);
            a6t = Mathf.Round(a6t);

            //a1Text.SetText(a1t.ToString());
            //a2Text.SetText(a2t.ToString());
            //a3Text.SetText(a3t.ToString());
            //a4Text.SetText(a4t.ToString());
            //a5Text.SetText(a5t.ToString());
            //a6Text.SetText(a6t.ToString());
            RDK.setRunMode(6);
            ROBOT.MoveJ(new_robot_pose, false);
        }
        private void OnEnable()
        {
            axes = GameObject.FindGameObjectsWithTag("LessonAxis");
            A1 = axes[0].transform;
            A2 = axes[1].transform;
            A3 = axes[2].transform;
            A4 = axes[3].transform;
            A5 = axes[4].transform;
            A6 = axes[5].transform;
        }
        private void OnDisable()
        {
            //world.SetActive(false);
        }
        void Update()
        {
            movstp = 10f * (jogSpeed/100f);
            if(roboActive && successConnect)
            {
                if (bhs.BtnA1PlusOn && !ROBOT.Busy())
                {


                    if (online)
                    {

                        realMove(0, movstp);
                    }
                    else
                    {
                        simulMove(0, movstp);
                    }


                }
                else if (bhs.BtnA1MinusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(0, -movstp);
                    }
                    else
                    {
                        simulMove(0, -movstp);
                    }

                }
                else if (bhs.BtnA2PlusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(1, movstp);
                    }
                    else
                    {
                        simulMove(1, movstp);
                    }

                }
                else if (bhs.BtnA2MinusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(1, -movstp);
                    }
                    else
                    {
                        simulMove(1, -movstp);
                    }

                }
                else if (bhs.BtnA3PlusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(2, movstp);
                    }
                    else
                    {
                        simulMove(2, movstp);
                    }

                }
                else if (bhs.BtnA3MinusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(2, -movstp);
                    }
                    else
                    {
                        simulMove(2, -movstp);
                    }
                }
                else if (bhs.BtnA4PlusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(3, movstp);
                    }
                    else
                    {
                        simulMove(3, movstp);
                    }
                }
                else if (bhs.BtnA4MinusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(3, -movstp);
                    }
                    else
                    {
                        simulMove(3, -movstp);
                    }
                }
                else if (bhs.BtnA5PlusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(4, movstp);
                    }
                    else
                    {
                        simulMove(4, movstp);
                    }
                }
                else if (bhs.BtnA5MinusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(4, -movstp);
                    }
                    else
                    {
                        simulMove(4, -movstp);
                    }
                }
                else if (bhs.BtnA6PlusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(5, movstp);
                    }
                    else
                    {
                        simulMove(5, movstp);
                    }
                }
                else if (bhs.BtnA6MinusOn && !ROBOT.Busy())
                {
                    if (online)
                    {
                        realMove(5, -movstp);
                    }
                    else
                    {
                        simulMove(5, -movstp);
                    }
                }
            }

            
        }
        void OnApplicationQuit()
        {
            if(roboActive && successConnect)
            {
                ROBOT.setJoints(hom);
            }
           
            
        }
    }
}