using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;
using System.Text.RegularExpressions;
namespace ProjectRoboDK
{
    public class CodeWindow : MonoBehaviour
    {
        public stateSwap stateSwappin;
        RoboDK RDK = null;
        RoboDK.Item ROBOT = null;
        RoboDK.Item target = null;
        RoboDK.Item target2 = null;
        RoboDK.Item prevTarget = null;
        RoboDK.Item[] targets = null;
        RoboDK.Item FRAME = null;
        RoboDK.Item prog = null;
        RoboDK.Item homeTarg = null;
        RoboDK.Item tool = null;
        public bool buttonOn = false;
        public GameObject node;
        public GameObject flange;
        public GameObject baseR;
        public GameObject pointsLook;
        private GameObject[] points;
        (GameObject, GameObject)[] t = new (GameObject, GameObject)[1000];
        (double[], double[])[] axesStored;
        public int index = 0;
        public GameObject code;
        Dictionary<String, Vector3> dict = new Dictionary<string, Vector3>();
        Dictionary<String, Double[]> pointDict = new Dictionary<string, double[]>();
        Dictionary<String, GameObject> sphereDict = new Dictionary<string, GameObject>();

        private string[] parsedLine = new string[4];
        public string[,] allLines;
        public int auxEnd = 0;
        public Vector3 home;
        public GameObject auxO;
        public GameObject endO;
        public GameObject warningPopup;
        public GameObject circPopup;
        public GameObject modePopup;
        public bool chang = false;
        private bool waits = true;

        public GameObject ghostBall;

        private GameObject[] axes;
        private Transform A1;
        private Transform A2;
        private Transform A3;
        private Transform A4;
        private Transform A5;
        private Transform A6;

        public AxisMode am;
        public TextAsset motion;
        int parseInt = 10;
        private Double[] non;
        private int indy = 0;
        public int readyUp = 0;
        private ButtonManagerScript bhs;
        private bool motioning = false;
        public pointList pl;
        private int frameNum;
        private Keyframe[] keys1;
        private Keyframe[] keys2;
        private Keyframe[] keys3;
        private Keyframe[] keys4;
        private Keyframe[] keys5;
        private Keyframe[] keys6;
        private Keyframe[] keys1b;
        private Keyframe[] keys2b;
        private Keyframe[] keys3b;
        private Keyframe[] keys4b;
        private Keyframe[] keys5b;
        private Keyframe[] keys6b;
        private int currFrame;
        private float currMov;
        public float mms;
        private float cmms;
        public AnimationClip animClip1;
        public AnimationClip animClip2;
        public AnimationClip animClip3;
        public AnimationClip animClip4;
        public AnimationClip animClip5;
        public AnimationClip animClip6;

        public Animation A1A;
        public Animation A2A;
        public Animation A3A;
        public Animation A4A;
        public Animation A5A;
        public Animation A6A;

        public bool frontStart = true;
        public bool done = false;
        private bool homeDone = false;
        private bool notHoming = false;
        private bool notManual = false;
        private bool tempStore;
        // Start is called before the first frame update
        void Start()
        {
            RDK = new RoboDK();
            ROBOT = RDK.getItem("KUKA KR 10 R1100 sixx");
            axes = GameObject.FindGameObjectsWithTag("LessonAxis");
            FRAME = RDK.getItem("KUKA KR 10 R1100 sixx Base");
            prog = RDK.getItem("Prog1");
            prevTarget = RDK.getItem("Target 2");
            homeTarg = RDK.getItem("Target 2");
            A1 = axes[0].transform;
            A2 = axes[1].transform;
            A3 = axes[2].transform;
            A4 = axes[3].transform;
            A5 = axes[4].transform;
            A6 = axes[5].transform;
            non = new double[6];
            non[0] = -1000;
            non[1] = -1000;
            non[2] = -1000;
            non[3] = -1000;
            non[4] = -1000;
            non[5] = -1000;
            axesStored = new (double[], double[])[1000];
            for (int i = 0; i < 1000; i++)
            {
                axesStored[i].Item1 = new double[6];
                axesStored[i].Item2 = new double[6];
            }
            bhs = GameObject.FindGameObjectWithTag("BMS").GetComponent<ButtonManagerScript>();
            A1A.Stop("Why");
            A2A.Stop("Why2");
            A3A.Stop("Why3");
            A4A.Stop("Why4");
            A5A.Stop("Why5");
            A6A.Stop("Why6");
        }

        // Update is called once per frame
        

        //public void stopAnim()
        //{
        //    StopAllCoroutines();
        //}

        public IEnumerator pplay()
        {
            yield return new WaitForSecondsRealtime(5);
            //Coroutine r;
            StartCoroutine(StartMove(motion, A1, A2, A3, A4, A5, A6));
            
            //yield return null;
        }

        public IEnumerator create()
        {
            /*
             * print("A1 " + axesStored[indy].Item1[0]);
            print("A2 " + axesStored[indy].Item1[1]);
            print("A3 " + axesStored[indy].Item1[2]);
            print("A4 " + axesStored[indy].Item1[3]);
            print("A5 " + axesStored[indy].Item1[4]);
            print("A6 " + axesStored[indy].Item1[5]);

            for (int i = 0; i < indy; i++)
            {
                print(i + "TELL");
                //if (axesStored[i].Item1[0] != -1000)
                //{
                //    //axesStored[i].Item1[0] = am.stop1;
                //    //axesStored[i].Item1[1] = am.stop2 * -1f;
                //    //axesStored[i].Item1[2] = am.stop3;
                //    //axesStored[i].Item1[3] = am.stop4;
                //    //axesStored[i].Item1[4] = am.stop5;
                //    //axesStored[i].Item1[5] = am.stop6;

                //    ROBOT.setJoints(axesStored[i].Item1);
                //    target = RDK.AddTarget("Test", FRAME, ROBOT);
                //    target.setAsCartesianTarget();
                //    target.setJoints(ROBOT.Joints());
                //}
                //if (axesStored[i].Item2[0] != -1000)
                //{
                //    //axesStored[i].Item2[0] = am.stop1;
                //    //axesStored[i].Item2[1] = am.stop2 * -1f;
                //    //axesStored[i].Item2[2] = am.stop3;
                //    //axesStored[i].Item2[3] = am.stop4;
                //    //axesStored[i].Item2[4] = am.stop5;
                //    //axesStored[i].Item2[5] = am.stop6;

                //    ROBOT.setJoints(axesStored[i].Item2);
                //    target2 = RDK.AddTarget("Test2", FRAME, ROBOT);
                //    target2.setAsCartesianTarget();
                //    target2.setJoints(ROBOT.Joints());
                //}
                ROBOT.setJoints(pointDict[allLines[i, 1]]);
                target = RDK.AddTarget("Test", FRAME, ROBOT);
                target.setAsCartesianTarget();
                target.setJoints(ROBOT.Joints());
                if (allLines[i, 0] == "PTP")
                {
                    if (ROBOT.MoveJ_Test(prevTarget.Joints(), target.Joints()) == 0)
                    {
                        prog.MoveJ(target);
                        prevTarget = target;
                    }
                    else
                    {
                        print("Error moving from previous point to " + allLines[i, 1]);
                        //bhs.swapMode(swap);
                        break;
                    }

                }
                else if (allLines[i, 0] == "LIN")
                {
                    if (ROBOT.MoveL_Test(prevTarget.Joints(), target.Pose()) == 0)
                    {
                        prog.MoveL(target);
                        prevTarget = target;
                    }
                    else
                    {
                        print("Error moving from previous point to " + allLines[i, 1]);
                        // bhs.swapMode(swap);
                        break;
                    }

                }
                else if (allLines[i, 0] == "CIRC")
                {
                    ROBOT.setJoints(pointDict[allLines[i, 2]]);
                    target2 = RDK.AddTarget("Test 2", FRAME, ROBOT);
                    target2.setAsCartesianTarget();
                    target2.setJoints(ROBOT.Joints());
                    //prog.tes
                    if (ROBOT.MoveL_Test(prevTarget.Joints(), target.Pose()) == 0 && ROBOT.MoveL_Test(target.Joints(), target2.Pose()) == 0)
                    {
                        prog.MoveC(target, target2);
                        prevTarget = target2;
                    }
                    else
                    {
                        print("Error moving from " + allLines[i, 1] + " to " + allLines[i, 2]);
                        //bhs.swapMode(swap);
                        break;
                    }

                }

            }
             */
            yield return new WaitForSecondsRealtime(5);
            //wait = false;
        }

        public void resetAnim()
        {
            //notManual = true;
            homeDone = false;
            stateSwappin.swapNorm();
            tempStore = bhs.tStop;
            //bhs.tStop = true;
            notManual = true;
            homed();
            
            
        }

        public void hold()
        {
            
            if (done && bhs.modeState != 2)
            {
                if (!A1A.isPlaying && !frontStart)
                {
                    print("We here?");
                    homeDone = true;
                    //stateSwappin.swapFin();
                }
                else
                {
                    frontStart = false;
                    //if(homeDone)
                    //{
                    //    stateSwappin.swapRun();

                    //}
                    if (!A1A.isPlaying)
                    {
                        A1A.Play("Why");
                        A2A.Play("Why2");
                        A3A.Play("Why3");
                        A4A.Play("Why4");
                        A5A.Play("Why5");
                        A6A.Play("Why6");
                        foreach (AnimationState state in A1A)
                        {
                            //state.normalizedTime = 0f;
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A2A)
                        {
                            //state.normalizedTime = 0f;
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A3A)
                        {
                            //state.normalizedTime = 0f;
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A4A)
                        {
                            //state.normalizedTime = 0f;
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A5A)
                        {
                            //state.normalizedTime = 0f;
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A6A)
                        {
                            //state.normalizedTime = 0f;
                            state.speed = 1;
                        }

                    }
                    else
                    {
                        foreach (AnimationState state in A1A)
                        {
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A2A)
                        {
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A3A)
                        {
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A4A)
                        {
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A5A)
                        {
                            state.speed = 1;
                        }
                        foreach (AnimationState state in A6A)
                        {
                            state.speed = 1;
                        }
                    }
                }
            }
        }

        public void holdNeg()
        {
            if (done && bhs.modeState != 2 && homeDone)
            {
                if(!A1A.isPlaying && frontStart)
                {
                    stateSwappin.swapReady();
                }
                else
                {
                    frontStart = true;
                    stateSwappin.swapReady();
                    if (!A1A.isPlaying)
                    {
                        foreach (AnimationState state in A1A)
                        {
                            state.normalizedTime = 1f;
                            state.speed = -1;
                        }
                        foreach (AnimationState state in A2A)
                        {
                            state.normalizedTime = 1f;
                            state.speed = -1;
                        }
                        foreach (AnimationState state in A3A)
                        {
                            state.normalizedTime = 1f;
                            state.speed = -1;
                        }
                        foreach (AnimationState state in A4A)
                        {
                            state.normalizedTime = 1f;
                            state.speed = -1;
                        }
                        foreach (AnimationState state in A5A)
                        {
                            state.normalizedTime = 1f;
                            state.speed = -1;
                        }
                        foreach (AnimationState state in A6A)
                        {
                            state.normalizedTime = 1f;
                            state.speed = -1;
                        }
                    }
                    foreach (AnimationState state in A1A)
                    {
                        
                        state.speed = -1;
                    }
                    foreach (AnimationState state in A2A)
                    {
                        
                        state.speed = -1;
                    }
                    foreach (AnimationState state in A3A)
                    {
                        
                        state.speed = -1;
                    }
                    foreach (AnimationState state in A4A)
                    {
                        
                        state.speed = -1;
                    }
                    foreach (AnimationState state in A5A)
                    {
                        
                        state.speed = -1;
                    }
                    foreach (AnimationState state in A6A)
                    {
                        
                        state.speed = -1;
                    }
                    if (!A1A.isPlaying)
                    {
                        A1A.Play("Why");
                        A2A.Play("Why2");
                        A3A.Play("Why3");
                        A4A.Play("Why4");
                        A5A.Play("Why5");
                        A6A.Play("Why6");
                    }
                }
            }
        }

        public void unHold()
        {
            if (done && bhs.modeState != 2)
            {
                stateSwappin.swapReady();
                foreach (AnimationState state in A1A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A2A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A3A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A4A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A5A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A6A)
                {
                    state.speed = 0;
                }
            }
            else if(!done && !notManual && !homeDone)
            {

                StartCoroutine(animCool());
            }
        }
        IEnumerator animCool()
        {
            WaitForSeconds w = new WaitForSeconds(3);
            yield return w;
            readyUp = 0;
        }
        public void homed()
        {
            prog.Delete();
            RDK.AddProgram("Prog1", ROBOT);
            prog = RDK.getItem("Prog1");
            tool = RDK.getItem("Tool 1");
            prog.setPoseFrame(RDK.getItem("KUKA KR 10 R1100 sixx Base"));
            prog.setTool(tool);
            prevTarget = RDK.getItem("Target 2");
            homeTarg = RDK.getItem("Target 2");
            double[] curr = new double[6];
            curr[0] = am.stop1;
            curr[1] = am.stop2 * -1f;
            curr[2] = am.stop3;
            curr[3] = am.stop4;
            curr[4] = am.stop5;
            curr[5] = am.stop6;
            ROBOT.setJoints(curr);
            target = RDK.AddTarget("Test", FRAME, ROBOT);
            target.setAsCartesianTarget();
            target.setJoints(ROBOT.Joints());
            prog.MoveJ(target);
            prog.MoveJ(prevTarget);
            String error = "";
            Mat jlist = new Mat(10, 10);
            ROBOT.setJoints(curr);
            prog.InstructionListJoints(out error, out jlist, 1, 5, @"C:\Users\RDFLab-Admin\AR-RA-Training-Demo - Copy\2021VR\Assets\RoboticsAcademy\Robotics\RDK\JLIST.csv");
            stateSwappin.swapReady();
            StartCoroutine(pplay());
        }

        public void playParse()
        {
            if (readyUp == 3)
            {
                //Add reset check code here
            }
            else
            {
                if (readyUp == 0)
                {
                    readyUp += 1;
                    homed();
                    stateSwappin.swapReady();
                    return;
                }
                else if(!homeDone)
                {
                    
                }
                else
                {

                    frontStart = true;
                    //StopAllCoroutines();
                    readyUp = 3;
                    prog.Delete();
                    RDK.AddProgram("Prog1", ROBOT);
                    prog = RDK.getItem("Prog1");
                    tool = RDK.getItem("Tool 1");
                    prog.setPoseFrame(RDK.getItem("KUKA KR 10 R1100 sixx Base"));
                    prog.setTool(tool);
                    prevTarget = RDK.getItem("Target 2");
                    homeTarg = RDK.getItem("Target 2");
                    print("A1 " + axesStored[indy].Item1[0]);
                    print("A2 " + axesStored[indy].Item1[1]);
                    print("A3 " + axesStored[indy].Item1[2]);
                    print("A4 " + axesStored[indy].Item1[3]);
                    print("A5 " + axesStored[indy].Item1[4]);
                    print("A6 " + axesStored[indy].Item1[5]);
                    double[] curr = new double[6];
                    curr[0] = am.stop1;
                    curr[1] = am.stop2 * -1f;
                    curr[2] = am.stop3;
                    curr[3] = am.stop4;
                    curr[4] = am.stop5;
                    curr[5] = am.stop6;
                    ROBOT.setJoints(curr);
                    target = RDK.AddTarget("Test", FRAME, ROBOT);
                    target.setAsCartesianTarget();
                    target.setJoints(ROBOT.Joints());
                    prog.MoveJ(target);
                    
                    for (int i = 0; i < indy; i++)
                    {
                        print(i + "TELL");
                        //if (axesStored[i].Item1[0] != -1000)
                        //{
                        //    //axesStored[i].Item1[0] = am.stop1;
                        //    //axesStored[i].Item1[1] = am.stop2 * -1f;
                        //    //axesStored[i].Item1[2] = am.stop3;
                        //    //axesStored[i].Item1[3] = am.stop4;
                        //    //axesStored[i].Item1[4] = am.stop5;
                        //    //axesStored[i].Item1[5] = am.stop6;

                        //    ROBOT.setJoints(axesStored[i].Item1);
                        //    target = RDK.AddTarget("Test", FRAME, ROBOT);
                        //    target.setAsCartesianTarget();
                        //    target.setJoints(ROBOT.Joints());
                        //}
                        //if (axesStored[i].Item2[0] != -1000)
                        //{
                        //    //axesStored[i].Item2[0] = am.stop1;
                        //    //axesStored[i].Item2[1] = am.stop2 * -1f;
                        //    //axesStored[i].Item2[2] = am.stop3;
                        //    //axesStored[i].Item2[3] = am.stop4;
                        //    //axesStored[i].Item2[4] = am.stop5;
                        //    //axesStored[i].Item2[5] = am.stop6;

                        //    ROBOT.setJoints(axesStored[i].Item2);
                        //    target2 = RDK.AddTarget("Test2", FRAME, ROBOT);
                        //    target2.setAsCartesianTarget();
                        //    target2.setJoints(ROBOT.Joints());
                        //}
                        ROBOT.setJoints(pointDict[allLines[i, 1]]);
                        target = RDK.AddTarget("Test", FRAME, ROBOT);
                        target.setAsCartesianTarget();
                        target.setJoints(ROBOT.Joints());
                        if (allLines[i, 0] == "PTP")
                        {
                            if (ROBOT.MoveJ_Test(prevTarget.Joints(), target.Joints()) == 0)
                            {
                                prog.MoveJ(target);
                                prevTarget = target;
                            }
                            else
                            {
                                print("Error moving from previous point to " + allLines[i, 1]);
                                //bhs.swapMode(swap);
                                return;
                            }

                        }
                        else if (allLines[i, 0] == "LIN")
                        {
                            if (ROBOT.MoveL_Test(prevTarget.Joints(), target.Pose()) == 0)
                            {
                                prog.MoveL(target);
                                prevTarget = target;
                            }
                            else
                            {
                                print("Error moving from previous point to " + allLines[i, 1]);
                                // bhs.swapMode(swap);
                                return;
                            }

                        }
                        else if (allLines[i, 0] == "CIRC")
                        {
                            ROBOT.setJoints(pointDict[allLines[i, 2]]);
                            print("T");
                            target2 = RDK.AddTarget("Test 2", FRAME, ROBOT);
                            print("E");
                            target2.setAsCartesianTarget();
                            print("A");
                            target2.setJoints(ROBOT.Joints());
                            //prog.tes
                            print("F");
                            if (ROBOT.MoveL_Test(prevTarget.Joints(), target.Pose()) == 0 && ROBOT.MoveL_Test(target.Joints(), target2.Pose()) == 0)
                            {
                                print("U");
                                prog.MoveC(target.Joints(), target2.Joints());
                                print("R");
                                prevTarget = target2;
                                print("P");
                            }
                            else
                            {
                                print("Error moving from " + allLines[i, 1] + " to " + allLines[i, 2]);
                                //bhs.swapMode(swap);
                                return;
                            }
                            print("L");
                        }
                        print(i + "TELL");
                    }
                    prog.MoveJ(prevTarget);
                    String error = "";

                    Mat jlist = new Mat(10, 10);

                    ROBOT.setJoints(curr);
                    prog.InstructionListJoints(out error, out jlist, 1, 5, @"C:\Users\RDFLab-Admin\AR-RA-Training-Demo - Copy\2021VR\Assets\RoboticsAcademy\Robotics\RDK\JLIST.csv");

                    //stateSwappin.swapRun();
                    StartCoroutine(pplay());
                }
            }
            //bhs.swapMode(swap);
        }

        public void parse()
        {
            //int total = code.transform.childCount;
            int total = 0;
            pl.setAllRed();
            foreach(Transform child in code.transform)
            {
                if(child.gameObject.activeSelf)
                {
                    total += 1;
                }
            }
            allLines = new string[total, 4];
            print("TOTAL" + total);

            for (int i = 0; i < total; i++)
            {

                for (int k = 1; k < 5; k++)
                {
                    allLines[i, k - 1] = code.transform.GetChild(i).GetChild(k).gameObject.GetComponentInChildren<TextMeshProUGUI>().text;

                }
                String g = allLines[i, 1].Substring(allLines[i, 1].LastIndexOf('P') + 1);
                int b;
                if (Int32.TryParse(g, out b))
                {
                    pl.setGreen(b - 1);
                }
                if (code.transform.GetChild(i).GetChild(3).gameObject.activeSelf)
                {
                    String p = allLines[i, 2].Substring(allLines[i, 2].LastIndexOf('P') + 1);
                    if (Int32.TryParse(p, out b))
                    {
                        pl.setGreen(b - 1);
                    }
                }
                
                
                


            }

        }

        public void toRoboDK()
        {

        }

        public void yesTouch()
        {
            GameObject x = GameObject.FindGameObjectWithTag("Highlight");
            print("Highlight");
            if (x == null)
            {
                print("Nope");
                return;
            }
            int z = x.GetComponent<count>().counta;
            print("Yes");
            if (allLines[z, 0] == "PTP" || allLines[z, 0] == "LIN")
            {
                print("Lin");
                //t[z].Item1.transform.position = flange.transform.position;
                
                sphereDict[allLines[z, 1]].transform.parent = flange.transform;
                sphereDict[allLines[z, 1]].transform.localPosition = new Vector3(0, 0, 0);
                sphereDict[allLines[z, 1]].transform.parent = null;
                int swap = 0;
                if (bhs.BtnWorldOn)
                {
                    bhs.swapMode(0);
                    swap = 1;
                }
                else if (bhs.BtnToolOn)
                {
                    bhs.swapMode(0);
                    swap = 2;
                }
                axesStored[z].Item1[0] = am.stop1;
                axesStored[z].Item1[1] = am.stop2 * -1f;
                axesStored[z].Item1[2] = am.stop3;
                axesStored[z].Item1[3] = am.stop4;
                axesStored[z].Item1[4] = am.stop5;
                axesStored[z].Item1[5] = am.stop6;
                pointDict[allLines[z, 1]] = axesStored[z].Item1;
                bhs.swapMode(swap);
            }
            else if (allLines[z, 0] == "CIRC" && auxEnd == 1)
            {
                print("ALL" + allLines[z, 1]);
                sphereDict[allLines[z, 1]].transform.position = flange.transform.position;
                int swap = 0;
                if (bhs.BtnWorldOn)
                {
                    bhs.swapMode(0);
                    swap = 1;
                }
                else if (bhs.BtnToolOn)
                {
                    bhs.swapMode(0);
                    swap = 2;
                }
                axesStored[z].Item1[0] = am.stop1;
                axesStored[z].Item1[1] = am.stop2 * -1f;
                axesStored[z].Item1[2] = am.stop3;
                axesStored[z].Item1[3] = am.stop4;
                axesStored[z].Item1[4] = am.stop5;
                axesStored[z].Item1[5] = am.stop6;
                pointDict[allLines[z, 1]] = axesStored[z].Item1;
                t[z].Item1.transform.parent = null;
                bhs.swapMode(swap);
            }
            else if (allLines[z, 0] == "CIRC" && auxEnd == 2)
            {
                print("ALL2" + allLines[z, 2]);
                sphereDict[allLines[z, 2]].transform.position = flange.transform.position;
                int swap = 0;
                if (bhs.BtnWorldOn)
                {
                    bhs.swapMode(0);
                    swap = 1;
                }
                else if (bhs.BtnToolOn)
                {
                    bhs.swapMode(0);
                    swap = 2;
                }
                axesStored[z].Item2[0] = am.stop1;
                axesStored[z].Item2[1] = am.stop2 * -1f;
                axesStored[z].Item2[2] = am.stop3;
                axesStored[z].Item2[3] = am.stop4;
                axesStored[z].Item2[4] = am.stop5;
                axesStored[z].Item2[5] = am.stop6;
                pointDict[allLines[z, 2]] = axesStored[z].Item2;
                t[z].Item1.transform.parent = null;
                bhs.swapMode(swap);
            }

        }

        public void confirmM()
        {
            parse();
            print("Parsed");
            GameObject x = GameObject.FindGameObjectWithTag("Highlight");
            if (!chang && x != null)
            {
                if (allLines[index - 1, 0] == "PTP" || allLines[index - 1, 0] == "LIN")
                {

                    int swap = 0;
                    if (bhs.BtnWorldOn)
                    {
                        bhs.swapMode(0);
                        swap = 1;
                    }
                    else if (bhs.BtnToolOn)
                    {
                        bhs.swapMode(0);
                        swap = 2;
                    }
                    axesStored[indy].Item1[0] = am.stop1;
                    axesStored[indy].Item1[1] = am.stop2 * -1f;
                    axesStored[indy].Item1[2] = am.stop3;
                    axesStored[indy].Item1[3] = am.stop4;
                    axesStored[indy].Item1[4] = am.stop5;
                    axesStored[indy].Item1[5] = am.stop6;
                    bhs.swapMode(swap);
                    if (pointDict.ContainsKey(allLines[index - 1, 1]))
                    {
                        
                    }
                    else
                    {
                        GameObject a = Instantiate(node);

                        //t[index - 1] = (a, null);
                        sphereDict[allLines[index - 1, 1]] =  a;
                        //a.transform.position = flange.transform.position;
                        a.transform.parent = flange.transform;
                        a.transform.localPosition = new Vector3(0, 0, 0);
                        a.transform.parent = baseR.transform;
                        pointDict.Add(allLines[index - 1, 1], axesStored[indy].Item1);
                    }
                    
                    print("A1 " + axesStored[indy].Item1[0]);
                    print("A2 " + axesStored[indy].Item1[1]);
                    print("A3 " + axesStored[indy].Item1[2]);
                    print("A4 " + axesStored[indy].Item1[3]);
                    print("A5 " + axesStored[indy].Item1[4]);
                    print("A6 " + axesStored[indy].Item1[5]);
                }
                else if (allLines[index - 1, 0] == "CIRC")
                {
                    GameObject a;
                    GameObject b;
                    if (pointDict.ContainsKey(allLines[indy, 1]))
                    {
                        
                    }
                    else
                    {
                        a = Instantiate(node);
                        a.transform.position = auxO.transform.position;
                        a.transform.parent = baseR.transform;
                        pointDict.Add(allLines[indy, 1], axesStored[indy].Item1);
                        sphereDict[allLines[indy, 1]] =  a;
                    }



                    print("CIRC");

                    //t[index - 1] = (a, b);
                    if (pointDict.ContainsKey(allLines[indy, 2]))
                    {

                    }
                    else
                    {

                        b = Instantiate(node);
                        b.transform.position = endO.transform.position;
                        b.transform.parent = baseR.transform;
                        pointDict.Add(allLines[indy, 2], axesStored[indy].Item2);
                        sphereDict[allLines[indy, 2]] = b;
                    }



                }
                GameObject[] drops = GameObject.FindGameObjectsWithTag("Dropdown");
                if (drops.Length != 0)
                {
                    for (int i = 0; i < drops.Length; i++)
                    {
                        drops[i].SetActive(false);
                    }
                }
                x.SetActive(false);
            }
            else if (x != null)
            {
                chang = false;
                GameObject[] drops = GameObject.FindGameObjectsWithTag("Dropdown");
                if (drops.Length != 0)
                {
                    for (int i = 0; i < drops.Length; i++)
                    {
                        drops[i].SetActive(false);
                    }
                }
                x.SetActive(false);
            }
            indy += 1;
            pointsLook.SetActive(false);
            ghostBall.SetActive(false);
            motioning = false;
            bhs.motioning = false;
            endO.transform.position = new Vector3(-9.044f, 1.906f, -35.306f);
            auxO.transform.position = new Vector3(-9.044f, 1.906f, -35.306f);
            buttonOn = false;
        }

        public void teachAux()
        {
            auxO.transform.parent = flange.transform;
            auxO.transform.localPosition = new Vector3(0, 0, 0);
            auxO.transform.parent = null;
            int swap = 0;
            if (bhs.BtnWorldOn)
            {
                bhs.swapMode(0);
                swap = 1;
            }
            else if (bhs.BtnToolOn)
            {
                bhs.swapMode(0);
                swap = 2;
            }
            axesStored[index - 1].Item1[0] = am.stop1;
            axesStored[index - 1].Item1[1] = am.stop2 * -1f;
            axesStored[index - 1].Item1[2] = am.stop3;
            axesStored[index - 1].Item1[3] = am.stop4;
            axesStored[index - 1].Item1[4] = am.stop5;
            axesStored[index - 1].Item1[5] = am.stop6;
            bhs.swapMode(swap);
            //pointDict.Add(allLines[index - 1, 1], axesStored[index - 1].Item1);
            pointDict[allLines[index - 1, 1]] = axesStored[index - 1].Item1;
            //auxO.transform.position = flange.transform.position;
        }

        public void teachEnd()
        {
            endO.transform.parent = flange.transform;
            endO.transform.localPosition = new Vector3(0, 0, 0);
            endO.transform.parent = null;
            int swap = 0;
            if (bhs.BtnWorldOn)
            {
                bhs.swapMode(0);
                swap = 1;
            }
            else if (bhs.BtnToolOn)
            {
                bhs.swapMode(0);
                swap = 2;
            }
            axesStored[index - 1].Item2[0] = am.stop1;
            axesStored[index - 1].Item2[1] = am.stop2 * -1f;
            axesStored[index - 1].Item2[2] = am.stop3;
            axesStored[index - 1].Item2[3] = am.stop4;
            axesStored[index - 1].Item2[4] = am.stop5;
            axesStored[index - 1].Item2[5] = am.stop6;
            bhs.swapMode(swap);
            //pointDict.Add(allLines[index - 1, 2], axesStored[index - 1].Item2);
            pointDict[allLines[index - 1, 2]] = axesStored[index - 1].Item2;
            //endO.transform.position = flange.transform.position;
        }

        public void cancelM()
        {
            index -= 1;
        }

        public void aux()
        {
            auxEnd = 1;
            print("AUX1");
            circPopup.SetActive(false);
            warningPopup.SetActive(true);
        }
        public void end()
        {
            auxEnd = 2;
            print("AUX2");
            circPopup.SetActive(false);
            warningPopup.SetActive(true);
        }

        public void moveAux()
        {
            
            parse();
            int swap = 0;
            if (bhs.BtnWorldOn)
            {
                bhs.swapMode(0);
                swap = 1;
            }
            else if (bhs.BtnToolOn)
            {
                bhs.swapMode(0);
                swap = 2;
            }
            axesStored[indy].Item1[0] = am.stop1;
            axesStored[indy].Item1[1] = am.stop2 * -1f;
            axesStored[indy].Item1[2] = am.stop3;
            axesStored[indy].Item1[3] = am.stop4;
            axesStored[indy].Item1[4] = am.stop5;
            axesStored[indy].Item1[5] = am.stop6;
            bhs.swapMode(swap);

            


            
            if(pointDict.ContainsKey(allLines[indy, 1]))
            {

            }
            else
            {
                auxO.transform.parent = flange.transform;
                auxO.transform.localPosition = new Vector3(0, 0, 0);
                auxO.transform.parent = null;
            }

            
            
            //ROBOT.setJoints(pointDict[allLines[indy, 1]]);
            //target = RDK.AddTarget("Test 1", FRAME, ROBOT);
            //target.setAsCartesianTarget();
            //target.setJoints(ROBOT.Joints());
        }

        public void moveEnd()
        {
            
            parse();
            int swap = 0;
            if (bhs.BtnWorldOn)
            {
                bhs.swapMode(0);
                swap = 1;
            }
            else if (bhs.BtnToolOn)
            {
                bhs.swapMode(0);
                swap = 2;
            }
            axesStored[indy].Item2[0] = am.stop1;
            axesStored[indy].Item2[1] = am.stop2 * -1f;
            axesStored[indy].Item2[2] = am.stop3;
            axesStored[indy].Item2[3] = am.stop4;
            axesStored[indy].Item2[4] = am.stop5;
            axesStored[indy].Item2[5] = am.stop6;
            bhs.swapMode(swap);
            if (pointDict.ContainsKey(allLines[indy, 2]))
            {

            }
            else
            {
                endO.transform.parent = flange.transform;
                endO.transform.localPosition = new Vector3(0, 0, 0);
                endO.transform.parent = null;
            }
            
            //ROBOT.setJoints(pointDict[allLines[indy, 2]]);
            //target = RDK.AddTarget("Test 2", FRAME, ROBOT);
            //target.setAsCartesianTarget();
            //target.setJoints(ROBOT.Joints());
        }

        public void change()
        {
            GameObject x = GameObject.FindGameObjectWithTag("Highlight");
            print("Highlight");
            if (x == null)
            {
                print("Nope");
                return;
            }
            int z = x.GetComponent<count>().counta;
            //x.transform.parent.GetChild(5).gameObject.SetActive(true);
            //x.transform.parent.GetChild(6).gameObject.SetActive(true);
            //if (x.transform.parent.GetChild(1).GetChild(2).gameObject.activeSelf)
            //{
            //    x.transform.parent.GetChild(7).gameObject.SetActive(true);
            //}
            chang = true;
            //if (t[z].Item1 != null)
            //{
            //    Destroy(t[z].Item1);
            //}
            //if (t[z].Item2 != null)
            //{
            //    Destroy(t[z].Item2);
            //}

            //t[z].Item1 = null;
            //t[z].Item2 = null;


        }

        public void OnClick(Button btn)
        {
            switch (btn.name)
            {
                case "BtnMotion":
                    if(!motioning)
                    {
                        index += 1;
                        //pointsLook.SetActive(true);
                        ghostBall.SetActive(true);
                        buttonOn = true;
                        motioning = true;
                        bhs.motioning = true;
                        pl.addPoint();
                    }
                    
                    break;
                case "BtnTouchUp":
                    GameObject x = GameObject.FindGameObjectWithTag("Highlight");
                    print("Highlight");
                    int z = x.GetComponent<count>().counta;
                    if (x == null)
                    {
                        print("Nope");
                        break;
                    }
                    if (allLines[z, 0] == "CIRC")
                    {
                        circPopup.SetActive(true);
                    }
                    else
                    {
                        warningPopup.SetActive(true);
                    }
                    //pointsLook.SetActive(true);

                    break;
                case "BtnChange":
                    x = GameObject.FindGameObjectWithTag("Highlight");
                    print("Highlight");
                    z = x.GetComponent<count>().counta;
                    if (x == null)
                    {
                        print("Nope");
                        break;
                    }
                    pointsLook.SetActive(true);
                    buttonOn = true;
                    break;
                case "BtnWorld2":
                    modePopup.SetActive(!modePopup.activeSelf);
                    break;
                case "BtnRobot2":
                    modePopup.SetActive(!modePopup.activeSelf);
                    break;
                case "circEnd":
                    moveEnd();
                    break;

                case "circAux":
                    moveAux();
                    break;
            }
        }

        private void Update()
        {
            if (!A1A.isPlaying && !frontStart)
            {
                print("We here?");
                homeDone = true;
                //stateSwappin.swapFin();
            }
            if (!A1A.isPlaying && !frontStart && homeDone)
            {
                stateSwappin.swapFin();
            }
            if(homeDone && notManual)
            {
                bhs.tStop = tempStore;
                done = false;
                homeDone = false;
                frontStart = true;
                notManual = false;
                stateSwappin.swapNorm();
            }
            if (A1A.isPlaying)
            {
                print("Play on playa");
            }
            else
            {
                print("Stop. Hammertime.");
            }
        }

        public void reseti()
        {
            Array.Clear(t,0,t.Length);
            for(int i = 0; i < axesStored.Length; i++)
            {
                Array.Clear(axesStored[i].Item1, 0, axesStored[i].Item1.Length);
                Array.Clear(axesStored[i].Item2, 0, axesStored[i].Item2.Length);
            }
            indy = 0;
            index = 0;
            ghostBall.SetActive(false);
            buttonOn = false;
            motioning = false;
            bhs.motioning = false;
            pointDict.Clear();
            Array.Clear(allLines, 0, allLines.Length);
            foreach(GameObject child in sphereDict.Values.ToList())
            {
                Destroy(child);
            }
            sphereDict.Clear();
            chang = false;
            circPopup.SetActive(false);
            warningPopup.SetActive(false);
            endO.transform.position = new Vector3(-9.044f, 1.906f, -35.306f);
            auxO.transform.position = new Vector3(-9.044f, 1.906f, -35.306f);
            //prevTarget = RDK.getItem("Target 2");
            readyUp = 0;
            stateSwappin.swapNorm();
            prog.Delete();
            RDK.AddProgram("Prog1", ROBOT);
            prog = RDK.getItem("Prog1");
            tool = RDK.getItem("Tool 1");
            prog.setPoseFrame(RDK.getItem("KUKA KR 10 R1100 sixx Base"));
            prog.setTool(tool);
            prevTarget = RDK.getItem("Target 2");
            homeTarg = RDK.getItem("Target 2");
            bhs.resetti();
            pl.resetti();
        }

        private void OnApplicationQuit()
        {
            prog.Delete();
            RDK.AddProgram("Prog1", ROBOT);
            prog = RDK.getItem("Prog1");
            tool = RDK.getItem("Tool 1");
            prog.setPoseFrame(RDK.getItem("KUKA KR 10 R1100 sixx Base"));
            prog.setTool(tool);
            double[] ts = new double[6];
            ts[0] = 0;
            ts[1] = -90f;
            ts[2] = 90f;
            ts[3] = 0;
            ts[4] = 0;
            ts[5] = 0;
            ROBOT.setJoints(ts);
        }


        private IEnumerator StartMove(TextAsset move, Transform A1, Transform A2, Transform A3, Transform A4, Transform A5, Transform A6)
        {
            parseInt = 10;
            String sJoints = move.text;
            String[] joints = Regex.Split(sJoints, "[,\r\n]+");
            print("JOINT NUM" + joints.Length);
            float[] simulMove = new float[6];
            int simulIndex = 0;

            float[] velocities = new float[allLines.Length + 2];
            int currVelo = 0;
            velocities[0] = 0.1f;
            velocities[1] = 0.1f;
            for(int i = 2; i < allLines.GetLength(0)+2; i++)
            {
                print(allLines[i - 2, 3] + "RawVel");
                if (allLines[i - 2, 0] == "PTP" || allLines[i - 2, 0] == "CIRC")
                {
                    velocities[i] = 1 / (float.Parse(allLines[i - 2, 3].TrimEnd(new char[] { '%', 'm' })) / 10);
                }
                else
                {
                    velocities[i] = 1 / (float.Parse(allLines[i - 2, 3].TrimEnd(new char[] { '%', 'm' })));
                }
                print(velocities[i] + "Velocity");
            }
            WaitForSeconds wait = new WaitForSeconds(0.05f);


            frameNum = Mathf.FloorToInt(joints.Length / 10) - 1;
            print("FM " + frameNum);
            keys1 = new Keyframe[frameNum];
            keys2 = new Keyframe[frameNum];
            keys3 = new Keyframe[frameNum];
            keys4 = new Keyframe[frameNum];
            keys5 = new Keyframe[frameNum];
            keys6 = new Keyframe[frameNum];
            keys1b = new Keyframe[frameNum];
            keys2b = new Keyframe[frameNum];
            keys3b = new Keyframe[frameNum];
            keys4b = new Keyframe[frameNum];
            keys5b = new Keyframe[frameNum];
            keys6b = new Keyframe[frameNum];

            currFrame = 0;
            while (parseInt < joints.Length - 1)
            {
                
                    //stateSwappin.swapRun();
                    print(parseInt);
                    simulMove[simulIndex] = float.Parse(joints[parseInt]);
                    if (parseInt % 10 == 0 && currMov != float.Parse(joints[parseInt + 9]))
                    {
                        currMov = float.Parse(joints[parseInt + 9]);
                        print("Swapped move" + currMov);
                        mms = velocities[currVelo];
                        currVelo += 1;
                        //mms = mms / 2;
                    }
                    if (parseInt % 10 < 5)
                    {
                        parseInt++;
                        simulIndex++;
                    }
                    else
                    {
                        print("CF " + currFrame);
                        keys1[currFrame] = new Keyframe(cmms, simulMove[0]);
                        keys2[currFrame] = new Keyframe(cmms, -simulMove[1]);
                        keys3[currFrame] = new Keyframe(cmms, simulMove[2]);
                        keys4[currFrame] = new Keyframe(cmms, -simulMove[3]);
                        keys5[currFrame] = new Keyframe(cmms, simulMove[4]);
                        keys6[currFrame] = new Keyframe(cmms, simulMove[5]);
                        keys1b[currFrame] = new Keyframe(cmms, 180f);
                        keys2b[currFrame] = new Keyframe(cmms, 180f);
                        //keys3b[currFrame] = new Keyframe(cmms, 180f);
                        keys4b[currFrame] = new Keyframe(cmms, 180f);
                        keys5b[currFrame] = new Keyframe(cmms, 180f);
                        keys6b[currFrame] = new Keyframe(cmms, 180f);
                        keys1[currFrame].inTangent = 0f;
                        keys1[currFrame].outTangent = 0f;
                        keys2[currFrame].inTangent = 0f;
                        keys2[currFrame].outTangent = 0f;
                        keys3[currFrame].inTangent = 0f;
                        keys3[currFrame].outTangent = 0f;
                        keys4[currFrame].inTangent = 0f;
                        keys4[currFrame].outTangent = 0f;
                        keys5[currFrame].inTangent = 0f;
                        keys5[currFrame].outTangent = 0f;
                        keys6[currFrame].inTangent = 0f;
                        keys6[currFrame].outTangent = 0f;

                        //A1.Rotate(0, simulMove[0], 0);
                        //A2.Rotate(-simulMove[1] - A2.rotation.eulerAngles.x, 0, 0);
                        //A3.Rotate(simulMove[2] - A3.rotation.eulerAngles.x, 0, 0);
                        //A4.Rotate(0, 0, simulMove[3] - A4.rotation.eulerAngles.z);
                        //A5.Rotate(simulMove[4] - A5.rotation.eulerAngles.x, 0, 0);
                        //A6.Rotate(0, 0, simulMove[5] - A6.rotation.eulerAngles.z);
                        parseInt = parseInt + 5;
                        simulIndex = 0;
                        cmms += mms;
                        currFrame += 1;
                        yield return null;
                    }
                    
                
            }
            //keys3 = new Keyframe[2];
            //keys3[0] = new Keyframe(0.1f, 90f);
            AnimationCurve c1 = new AnimationCurve(keys1);
            AnimationCurve c2 = new AnimationCurve(keys2);
            AnimationCurve c3 = new AnimationCurve(keys3);
            AnimationCurve c4 = new AnimationCurve(keys4);
            AnimationCurve c5 = new AnimationCurve(keys5);
            AnimationCurve c6 = new AnimationCurve(keys6);
            AnimationCurve c1b = new AnimationCurve(keys1b);
            AnimationCurve c2b = new AnimationCurve(keys2b);
            //AnimationCurve c3b = new AnimationCurve(keys3b);
            AnimationCurve c4b = new AnimationCurve(keys4b);
            AnimationCurve c5b = new AnimationCurve(keys5b);
            AnimationCurve c6b = new AnimationCurve(keys6b);

            for (int i = 0; i < frameNum; i++)
            {
                c1.SmoothTangents(i, 0);
                c2.SmoothTangents(i, 0);
                c3.SmoothTangents(i, 0);
                c4.SmoothTangents(i, 0);
                c5.SmoothTangents(i, 0);
                c6.SmoothTangents(i, 0);

            }



            animClip1.ClearCurves();
            animClip2.ClearCurves();
            animClip3.ClearCurves();
            animClip4.ClearCurves();
            animClip5.ClearCurves();
            animClip6.ClearCurves();
            animClip1.SetCurve("", typeof(Transform), "localEulerAngles.y", c1);
            animClip1.SetCurve("", typeof(Transform), "localEulerAngles.z", c1b);
            animClip2.SetCurve("", typeof(Transform), "localEulerAngles.x", c2);
            animClip2.SetCurve("", typeof(Transform), "localEulerAngles.z", c2b);
            animClip3.SetCurve("", typeof(Transform), "localEulerAngles.x", c3);
            //animClip3.SetCurve("", typeof(Transform), "localEulerAngles.x", c3b);
            animClip4.SetCurve("", typeof(Transform), "localEulerAngles.z", c4);
            animClip4.SetCurve("", typeof(Transform), "localEulerAngles.y", c4b);
            animClip5.SetCurve("", typeof(Transform), "localEulerAngles.x", c5);
            animClip5.SetCurve("", typeof(Transform), "localEulerAngles.y", c5b);
            animClip6.SetCurve("", typeof(Transform), "localEulerAngles.z", c6);
            animClip6.SetCurve("", typeof(Transform), "localEulerAngles.y", c6b);
            //A1A.AddClip(animClip1, "A1A");
            //A2A.AddClip(animClip2, "A2A");
            //A3A.AddClip(animClip3, "A3A");
            //A4A.AddClip(animClip4, "A4A");
            //A5A.AddClip(animClip5, "A5A");
            //A6A.AddClip(animClip6, "A6A");
            print("DONE");
            
            print("?????????");
            done = true;
            A1A.Play("Why");
            A2A.Play("Why2");
            A3A.Play("Why3");
            A4A.Play("Why4");
            A5A.Play("Why5");
            A6A.Play("Why6");
            if (bhs.modeState != 2)
            {
                foreach (AnimationState state in A1A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A2A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A3A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A4A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A5A)
                {
                    state.speed = 0;
                }
                foreach (AnimationState state in A6A)
                {
                    state.speed = 0;
                }
            }
            yield break;
        }
    }
}