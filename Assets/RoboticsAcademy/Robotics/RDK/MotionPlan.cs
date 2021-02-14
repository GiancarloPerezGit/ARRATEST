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
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
namespace ProjectRoboDK
{
    public class MotionPlan : MonoBehaviour
    {
        RoboDK RDK = null;
        RoboDK.Item ROBOT;
        RoboDK.Item prog;
        public TextAsset motion;
        public KR10IKW robot;
        public GameObject path;
        public GameObject path2;
        public GameObject path3;
        public GameObject demoUI1;
        public GameObject demoUI2;
        public GameObject demoUI3;
        public GameObject demoUI4;
        public GameObject demoUI5;
        public GameObject demoUI6;
        public GameObject demoUI7;
        public GameObject node1;
        public GameObject node2;
        public GameObject node3;
        public GameObject ee;
        private bool done = false;
        private int step = 0;
        int parseInt = 10;
        private Transform a1;
        private Transform a2;
        private Transform a3;
        private Transform a4;
        private Transform a5;
        private Transform a6;
        // Start is called before the first frame update
        void Start()
        {
            RDK = new RoboDK();
            RDK.Render(false);
            ROBOT = RDK.getItem("KUKA KR 10 R1100 sixx");
            String error = "";
            Mat jlist = new Mat(10, 10);
            prog = RDK.getItem("Prog1");
            prog.InstructionListJoints(out error, out jlist, 10, 5, "C:/Users/RDFLab-Admin/AR-RA-Training-Demo/Assets/RoboticsAcademy/Robotics/RDK/JLIST.csv");
            a1 = GameObject.Find("[environment]/AxisBot/AxisBot/KR10_V3/robot_GRP/robot_GEOM/Base/axis1").transform;
            a2 = GameObject.Find("[environment]/AxisBot/AxisBot/KR10_V3/robot_GRP/robot_GEOM/Base/axis1/axis2").transform;
            a3 = GameObject.Find("[environment]/AxisBot/AxisBot/KR10_V3/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3").transform;
            a4 = GameObject.Find("[environment]/AxisBot/AxisBot/KR10_V3/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3/axis4").transform;
            a5 = GameObject.Find("[environment]/AxisBot/AxisBot/KR10_V3/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3/axis4/axis5").transform;
            a6 = GameObject.Find("[environment]/AxisBot/AxisBot/KR10_V3/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3/axis4/axis5/axis6").transform;
            demoUI1.SetActive(false);
            demoUI2.SetActive(false);
            demoUI3.SetActive(false);
            demoUI4.SetActive(false);
            demoUI5.SetActive(false);
            demoUI6.SetActive(false);
            demoUI7.SetActive(false);
            node1.SetActive(false);
            node2.SetActive(false);
            node3.SetActive(false);
        }
        private IEnumerator StartMove(TextAsset move, Transform A1, Transform A2, Transform A3, Transform A4, Transform A5, Transform A6)
        {
            String sJoints = move.text;
            String[] joints = Regex.Split(sJoints, "[,\r\n]+");
            print(joints[20]);
            float[] simulMove = new float[6];
            int simulIndex = 0;
            WaitForSeconds wait = new WaitForSeconds(0.05f);
            while (parseInt < joints.Length)
            {
                print(parseInt);
                simulMove[simulIndex] = float.Parse(joints[parseInt]);
                if (parseInt % 10 < 5)
                {
                    parseInt++;
                    simulIndex++;
                }
                else
                {
                    print(simulMove[0]);
                    print(simulMove[1]);
                    print(simulMove[2]);
                    print(simulMove[3]);
                    print(simulMove[4]);
                    print(simulMove[5]);
                    A1.localEulerAngles = new Vector3(0, simulMove[0], -180);
                    A2.localEulerAngles = new Vector3(-simulMove[1], 0, -180);
                    A3.localEulerAngles = new Vector3(simulMove[2], 0, 0);
                    A4.localEulerAngles = new Vector3(0, 180, -simulMove[3]);
                    A5.localEulerAngles = new Vector3(simulMove[4], 180, 0);
                    A6.localEulerAngles = new Vector3(0, 180, simulMove[5]);
                    //A1.Rotate(0, simulMove[0], 0);
                    //A2.Rotate(-simulMove[1] - A2.rotation.eulerAngles.x, 0, 0);
                    //A3.Rotate(simulMove[2] - A3.rotation.eulerAngles.x, 0, 0);
                    //A4.Rotate(0, 0, simulMove[3] - A4.rotation.eulerAngles.z);
                    //A5.Rotate(simulMove[4] - A5.rotation.eulerAngles.x, 0, 0);
                    //A6.Rotate(0, 0, simulMove[5] - A6.rotation.eulerAngles.z);
                    parseInt = parseInt + 5;
                    simulIndex = 0;
                    yield return wait;
                }
            }
        }
        public void robAdd(Vector3 end, float speed, int type)
        {
            RoboDK.Item t1 = RDK.AddTarget("Targ");
            t1.setPose(Mat.FromTxyzRxyz(end.x, end.y, end.z, 0, 90, 0));
            
        }
        public void createLine(int i, GameObject start, GameObject end)
        {
            var offset = end.transform.position - start.transform.position;
            var scale = new Vector3(0.015f, offset.magnitude / 2.0f, 0.015f);
            var position = start.transform.position + (offset / 2.0f);
            GameObject cylinder;
            if (i == 1)
            {
                cylinder = Instantiate(path, position, Quaternion.identity);
                cylinder.transform.up = offset;
                cylinder.transform.localScale = scale;
            }
            else if(i == 2)
            {
                cylinder = Instantiate(path2, position, Quaternion.identity);
                cylinder.transform.up = offset;
                cylinder.transform.localScale = scale;
            }
            else if (i == 3)
            {
                cylinder = Instantiate(path3, position, Quaternion.identity);
                cylinder.transform.up = offset;
                cylinder.transform.localScale = scale;
            }


        }
        // Update is called once per frame
        void Update()
        {
            if(Input.GetKeyDown("space"))
            {
                print("PRESSED");
                step++;
            }
            if(step == 1 && !done)
            {
                done = true;
                node1.SetActive(true);
            }
            if (step == 2 && done)
            {
                done = false;
                node2.SetActive(true);
            }
            if (step == 3 && !done)
            {
                done = true;
                node3.SetActive(true);
            }
            if (step == 4 && done)
            {
                done = false;
                StartCoroutine(StartMove(motion, a1, a2, a3, a4, a5, a6));
            }
            //if (step == 5 && !done)
            //{
            //    done = true;
            //    demoUI4.SetActive(true);
            //    createLine(1, ee, node1);
            //}
            //if (step == 6 && done)
            //{
            //    done = false;
            //    demoUI2.SetActive(true);
            //}
            //if (step == 7 && !done)
            //{
            //    done = true;
            //    demoUI5.SetActive(true);
            //    createLine(2, node1, node2);
            //}
            //if (step == 8 && done)
            //{
            //    done = false;
            //    demoUI3.SetActive(true);
            //}
            //if (step == 9 && !done)
            //{
            //    done = true;
            //    demoUI6.SetActive(true);
            //}
            //if (step == 10 && done)
            //{
            //    done = false;
            //    demoUI7.SetActive(true);
            //    createLine(3, node2, node3);
            //    createLine(3, node3, node1);
            //}
            //if (step == 11 && !done)
            //{
            //    done = true;
                
            //}
            
        }
    }
}