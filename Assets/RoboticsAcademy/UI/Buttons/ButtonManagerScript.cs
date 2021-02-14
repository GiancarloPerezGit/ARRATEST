using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// NOTE: drop the "button manager into the scene, and drop the toolbox prefab into main camera
public class ButtonManagerScript : MonoBehaviour
{
    public GameObject robMan;
    public GameObject toolBot;
    public GameObject axisBot;
    public GameObject worldBot;

    public TimerScript timer;

    public GameObject PulldownMode;
    public GameObject PulldownAxisMode;

    public GameObject CodeWindow;
    public GameObject CodeLine;

    public GameObject WarningPopUp;

    public GameObject RoboticUI;
    public GameObject AxisObject;

    public GameObject modePopUp;

    //Mode buttons bools
    public bool BtnRobotOn = true;
    public bool BtnWorldOn = false;
    public bool BtnToolOn = false;

    //Increase Buttons bools
    public bool BtnA1PlusOn = false;
    public bool BtnA2PlusOn = false;
    public bool BtnA3PlusOn = false;
    public bool BtnA4PlusOn = false;
    public bool BtnA5PlusOn = false;
    public bool BtnA6PlusOn = false;

    //decrease Buttons bools
    public bool BtnA1MinusOn = false;
    public bool BtnA2MinusOn = false;
    public bool BtnA6MinusOn = false;
    public bool BtnA3MinusOn = false;
    public bool BtnA4MinusOn = false;
    public bool BtnA5MinusOn = false;

    //Start lesson buttons bools
    public bool startLesson = false;

    public bool worldAnim = false;
    public bool toolAnim = false;
    public bool axisAnim = false;

    public bool LinStart = false;

    //Teleop Modes

    public bool set = true;

    public GameObject eConfirm;

    //controls the radial pie sliders (now made private)
    private Image RdlAxisA1Plus;
    private Image RdlAxisA2Plus;
    private Image RdlAxisA3Plus;
    private Image RdlAxisA4Plus;
    private Image RdlAxisA5Plus;
    private Image RdlAxisA6Plus;

    private Image RdlAxisA1Minus;
    private Image RdlAxisA2Minus;
    private Image RdlAxisA3Minus;
    private Image RdlAxisA4Minus;
    private Image RdlAxisA5Minus;
    private Image RdlAxisA6Minus;

    private TextMeshProUGUI NumAxisA1;
    private TextMeshProUGUI NumAxisA2;
    private TextMeshProUGUI NumAxisA3;
    private TextMeshProUGUI NumAxisA4;
    private TextMeshProUGUI NumAxisA5;
    private TextMeshProUGUI NumAxisA6;

    public GameObject RdlAxisPanel;

    public bool eStop;
    public bool pStop;
    public bool aStop = false;
    public bool tStop = false;
    public int modeState = 0;

    private int codeLineIndex = 0;

    public SpeedPopup sp;
    public float jogSp;
    public bool motioning = false;
    // Start is called before the first frame update
    // RdlAxis higherarchy should be Canvas > panel 1 > child 6-11 (for pos) child 12-17 (for neg) > child 1
    void Start()
    {
        //RdlAxisA1Plus = RdlAxisPanel.transform.GetChild(1).GetChild(6).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA2Plus = RdlAxisPanel.transform.GetChild(1).GetChild(7).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA3Plus = RdlAxisPanel.transform.GetChild(1).GetChild(8).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA4Plus = RdlAxisPanel.transform.GetChild(1).GetChild(9).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA5Plus = RdlAxisPanel.transform.GetChild(1).GetChild(10).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA6Plus = RdlAxisPanel.transform.GetChild(1).GetChild(11).GetChild(1).GetComponentInChildren<Image>();

        //RdlAxisA1Minus = RdlAxisPanel.transform.GetChild(1).GetChild(12).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA2Minus = RdlAxisPanel.transform.GetChild(1).GetChild(13).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA3Minus = RdlAxisPanel.transform.GetChild(1).GetChild(14).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA4Minus = RdlAxisPanel.transform.GetChild(1).GetChild(15).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA5Minus = RdlAxisPanel.transform.GetChild(1).GetChild(16).GetChild(1).GetComponentInChildren<Image>();
        //RdlAxisA6Minus = RdlAxisPanel.transform.GetChild(1).GetChild(17).GetChild(1).GetComponentInChildren<Image>();

        //NumAxisA1 = RdlAxisPanel.transform.GetChild(1).GetChild(18).GetComponent<TextMeshProUGUI>();
        //NumAxisA2 = RdlAxisPanel.transform.GetChild(1).GetChild(19).GetComponent<TextMeshProUGUI>();
        //NumAxisA3 = RdlAxisPanel.transform.GetChild(1).GetChild(20).GetComponent<TextMeshProUGUI>();
        //NumAxisA4 = RdlAxisPanel.transform.GetChild(1).GetChild(21).GetComponent<TextMeshProUGUI>();
        //NumAxisA5 = RdlAxisPanel.transform.GetChild(1).GetChild(22).GetComponent<TextMeshProUGUI>();
        //NumAxisA6 = RdlAxisPanel.transform.GetChild(1).GetChild(23).GetComponent<TextMeshProUGUI>();

    }

    // Update is called once per frame
    void Update()
    {
        jogSp = sp.jogSp;
        if (!eStop && pStop && !tStop)
        {
            //0213hacky, yes. but when the eStop turn on the "active" button and turn off the "deactive" button 
            RoboticUI.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(3).gameObject.SetActive(true);
            RoboticUI.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(4).gameObject.SetActive(true);

            RoboticUI.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(5).gameObject.SetActive(false);
            RoboticUI.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(6).gameObject.SetActive(false);

            AxisObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
            AxisObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
            AxisObject.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            AxisObject.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            AxisObject.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            AxisObject.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            AxisObject.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
            AxisObject.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
            AxisObject.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
            AxisObject.transform.GetChild(5).GetChild(1).gameObject.SetActive(true);
            AxisObject.transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
            AxisObject.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);

            AxisObject.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
            AxisObject.transform.GetChild(1).GetChild(3).gameObject.SetActive(false);
            AxisObject.transform.GetChild(2).GetChild(2).gameObject.SetActive(false);
            AxisObject.transform.GetChild(2).GetChild(3).gameObject.SetActive(false);
            AxisObject.transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
            AxisObject.transform.GetChild(3).GetChild(3).gameObject.SetActive(false);
            AxisObject.transform.GetChild(4).GetChild(2).gameObject.SetActive(false);
            AxisObject.transform.GetChild(4).GetChild(3).gameObject.SetActive(false);
            AxisObject.transform.GetChild(5).GetChild(2).gameObject.SetActive(false);
            AxisObject.transform.GetChild(5).GetChild(3).gameObject.SetActive(false);
            AxisObject.transform.GetChild(6).GetChild(2).gameObject.SetActive(false);
            AxisObject.transform.GetChild(6).GetChild(3).gameObject.SetActive(false);
        }
        else
        {
            RoboticUI.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(3).gameObject.SetActive(false);
            RoboticUI.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(4).gameObject.SetActive(false);

            RoboticUI.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(5).gameObject.SetActive(true);
            RoboticUI.transform.GetChild(0).GetChild(2).GetChild(1).GetChild(6).gameObject.SetActive(true);

            AxisObject.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
            AxisObject.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
            AxisObject.transform.GetChild(2).GetChild(0).gameObject.SetActive(false);
            AxisObject.transform.GetChild(2).GetChild(1).gameObject.SetActive(false);
            AxisObject.transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
            AxisObject.transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
            AxisObject.transform.GetChild(4).GetChild(0).gameObject.SetActive(false);
            AxisObject.transform.GetChild(4).GetChild(1).gameObject.SetActive(false);
            AxisObject.transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
            AxisObject.transform.GetChild(5).GetChild(1).gameObject.SetActive(false);
            AxisObject.transform.GetChild(6).GetChild(0).gameObject.SetActive(false);
            AxisObject.transform.GetChild(6).GetChild(1).gameObject.SetActive(false);

            AxisObject.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);
            AxisObject.transform.GetChild(1).GetChild(3).gameObject.SetActive(true);
            AxisObject.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
            AxisObject.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
            AxisObject.transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
            AxisObject.transform.GetChild(3).GetChild(3).gameObject.SetActive(true);
            AxisObject.transform.GetChild(4).GetChild(2).gameObject.SetActive(true);
            AxisObject.transform.GetChild(4).GetChild(3).gameObject.SetActive(true);
            AxisObject.transform.GetChild(5).GetChild(2).gameObject.SetActive(true);
            AxisObject.transform.GetChild(5).GetChild(3).gameObject.SetActive(true);
            AxisObject.transform.GetChild(6).GetChild(2).gameObject.SetActive(true);
            AxisObject.transform.GetChild(6).GetChild(3).gameObject.SetActive(true);
            BtnA1PlusOn = false;
            BtnA2PlusOn = false;
            BtnA3PlusOn = false;
            BtnA4PlusOn = false;
            BtnA5PlusOn = false;
            BtnA6PlusOn = false;

            BtnA1MinusOn = false;
            BtnA2MinusOn = false;
            BtnA3MinusOn = false;
            BtnA4MinusOn = false;
            BtnA5MinusOn = false;
            BtnA6MinusOn = false;
        }
        // Debug.Log(robMan.GetComponent<AxisMode>().stop1);

        //RdlAxisA1Plus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop1, 0, 170) / 360);
        //RdlAxisA2Plus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop2, 0, 45) / 360);
        //RdlAxisA3Plus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop3, 0, 156) / 360);
        //RdlAxisA4Plus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop4, 0, 185) / 360);
        //RdlAxisA5Plus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop5, 0, 120) / 360);
        //RdlAxisA6Plus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop6, 0, 350) / 360);

        //RdlAxisA1Minus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop1 * (-1), 0, 170) / 360);
        //RdlAxisA2Minus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop2 * (-1), 0, 45) / 360);
        //RdlAxisA3Minus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop3 * (-1), 0, 156) / 360);
        //RdlAxisA4Minus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop4 * (-1), 0, 185) / 360);
        //RdlAxisA5Minus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop5 * (-1), 0, 120) / 360);
        //RdlAxisA6Minus.fillAmount = (Mathf.Clamp(robMan.GetComponent<AxisMode>().stop6 * (-1), 0, 350) / 360);

        //NumAxisA1.text = robMan.GetComponent<AxisMode>().stop1.ToString();
        //NumAxisA2.text = robMan.GetComponent<AxisMode>().stop2.ToString();
        //NumAxisA3.text = robMan.GetComponent<AxisMode>().stop3.ToString();
        //NumAxisA4.text = robMan.GetComponent<AxisMode>().stop4.ToString();
        //NumAxisA5.text = robMan.GetComponent<AxisMode>().stop5.ToString();
        //NumAxisA6.text = robMan.GetComponent<AxisMode>().stop6.ToString();

        //CodeLine = CodeWindow.transform.GetChild(codeLineIndex).gameObject;
    }

    public void swapMode(int i = 0)
    {
        if(i == 0)
        {
            BtnRobotOn = true;
            BtnWorldOn = false;
            BtnToolOn = false;
            Debug.Log("Robot Axis Mode: on");
            robMan.GetComponent<ProjectRoboDK.ToolMode>().enabled = false;
            robMan.GetComponent<ProjectRoboDK.KR10IKW>().enabled = false;
            robMan.GetComponent<AxisMode>().enabled = true;
            modePopUp.transform.GetChild(0).gameObject.SetActive(false);
            modePopUp.transform.GetChild(1).gameObject.SetActive(true);
        }
        else if(i == 1)
        {
            BtnRobotOn = false;
            BtnWorldOn = true;
            BtnToolOn = false;
            Debug.Log("World Axis Mode: on");
            robMan.GetComponent<ProjectRoboDK.ToolMode>().enabled = false;
            robMan.GetComponent<ProjectRoboDK.KR10IKW>().enabled = true;
            robMan.GetComponent<AxisMode>().enabled = false;
            modePopUp.transform.GetChild(0).gameObject.SetActive(true);
            modePopUp.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    public void OnClick(Button btn)
    {
        switch (btn.name)
        {
            //mode button cases (these toggle on and off)
            case "BtnT1":
                tStop = false;
                robMan.GetComponent<AxisMode>().setT1(set);
                modeState = 0;
                break;
            case "BtnT2":
                tStop = true;
                robMan.GetComponent<AxisMode>().setT1(!set);
                modeState = 1;
                break;
            case "BtnAuto":
                tStop = false;
                robMan.GetComponent<AxisMode>().setT1(!set);
                modeState = 2;
                break;
            case "BtnRobot":
                BtnRobotOn = true;
                BtnWorldOn = false;
                BtnToolOn = false;
                Debug.Log("Robot Axis Mode: on");
                robMan.GetComponent<ProjectRoboDK.ToolMode>().enabled = false;
                robMan.GetComponent<ProjectRoboDK.KR10IKW>().enabled = false;
                robMan.GetComponent<AxisMode>().enabled = true;
                modePopUp.transform.GetChild(0).gameObject.SetActive(false);
                modePopUp.transform.GetChild(1).gameObject.SetActive(true);
                break;

            case "BtnWorld":
                BtnRobotOn = false;
                BtnWorldOn = true;
                BtnToolOn = false;
                Debug.Log("World Axis Mode: on");
                robMan.GetComponent<ProjectRoboDK.ToolMode>().enabled = false;
                robMan.GetComponent<ProjectRoboDK.KR10IKW>().enabled = true;
                robMan.GetComponent<AxisMode>().enabled = false;
                modePopUp.transform.GetChild(0).gameObject.SetActive(true);
                modePopUp.transform.GetChild(1).gameObject.SetActive(false);
                break;

            case "BtnTool":
                BtnRobotOn = false;
                BtnWorldOn = false;
                BtnToolOn = true;
                robMan.GetComponent<ProjectRoboDK.ToolMode>().enabled = true;
                robMan.GetComponent<ProjectRoboDK.KR10IKW>().enabled = false;
                robMan.GetComponent<AxisMode>().enabled = false;
                Debug.Log("Tool Axis Mode: on");
                break;

            // lesson buttons meant to teleport users to clicked on lessons
            case "Robotic Axis":
                Debug.Log("Lesson accessed: Robotic Axis ");
                timer.timerOn = true;
                break;

            case "World/Tool Coordinate":
                Debug.Log("Lesson accessed: World/Tool Coordinate ");
                timer.timerOn = true;
                break;

            case "Intro to End-Effectors":
                Debug.Log("Lesson accessed: Intro to End-Effectors ");
                timer.timerOn = true;
                break;

            case "Pick & Place":
                Debug.Log("Lesson accessed: Pick & Place ");
                timer.timerOn = true;
                break;

            case "Lin":
                LinStart = !LinStart;
                break;

            case "Target":

                break;

            case "EStopConfirm":
                eStop = false;
                
                eConfirm.SetActive(false);
                break;

            case "EmergencySwitchOff":
                RoboticUI.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(true);
                RoboticUI.transform.GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(false);
                eConfirm.SetActive(true);
                break;

            case "EmergencySwitchOn":
                eStop = true;
                RoboticUI.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.SetActive(false);
                RoboticUI.transform.GetChild(0).GetChild(1).GetChild(2).gameObject.SetActive(true);
                break;
            
            // IO drive dropdown
            case "BtnODriveDisplay":
                RoboticUI.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(true);
                break;

            case "BtnIDriveDisplay":
                RoboticUI.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(true);
                break;
            
            //add drive code here
            case "BtnODrive":
                RoboticUI.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(7).GetChild(0).GetChild(4).GetChild(0).gameObject.SetActive(true);
                RoboticUI.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(7).GetChild(0).GetChild(4).GetChild(1).gameObject.SetActive(false);
                RoboticUI.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
                break;

            case "BtnIDrive":
                RoboticUI.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(7).GetChild(0).GetChild(4).GetChild(0).gameObject.SetActive(false);
                RoboticUI.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(7).GetChild(0).GetChild(4).GetChild(1).gameObject.SetActive(true);
                RoboticUI.transform.GetChild(0).GetChild(2).GetChild(2).gameObject.SetActive(false);
                break;


            // these three exist to open the pulldownMenu Menu
            //case "BtnModeAuto":
            //    if (PulldownMode.activeSelf == false)
            //    {
            //        PulldownAxisMode.SetActive(false);
            //        PulldownMode.SetActive(true);
            //    }

            //    else
            //    {
            //        PulldownMode.SetActive(false);
            //    }
            //    break;

            //case "BtnModeT1":
            //    if (PulldownMode.activeSelf == false)
            //    {
            //        PulldownAxisMode.SetActive(false);
            //        PulldownMode.SetActive(true);
            //    }

            //    else
            //    {
            //        PulldownMode.SetActive(false);
            //    }
            //    break;

            //case "BtnModeT2":
            //    if (PulldownMode.activeSelf == false)
            //    {
            //        PulldownAxisMode.SetActive(false);
            //        PulldownMode.SetActive(true);
            //    }

            //    else
            //    {
            //        PulldownMode.SetActive(false);
            //    }
            //    break;
            // these two exist to open the pulldownAxisMenu Menu

            case "BtnAxisWorld":
                if (PulldownMode.activeSelf == false)
                {
                    PulldownAxisMode.SetActive(false);
                    PulldownMode.SetActive(true);
                }

                else
                {
                    PulldownMode.SetActive(false);
                }
                break;

            //these buttons control the code line. 
            case "BtnTouchUp":
                //filler code filled with however highlighting takes place
                //if (btn.name != null)
                //{
                //    WarningPopUp.SetActive(true);
                //}
                //else
                //{
                //    WarningPopUp.SetActive(true);
                //}
                break;

            case "BtnMotion":
                if(!motioning)
                {
                    CodeLine = CodeWindow.transform.GetChild(codeLineIndex).gameObject;
                    CodeLine.SetActive(true);
                    codeLineIndex++;
                }
                break;

            case "BtnChange":
                //filler code with however highlighting takes place
                //if (btn.name != null)
                //{
                //    WarningPopUp.SetActive(true);
                //}
                //else
                //{
                //    WarningPopUp.SetActive(true);
                //}
                break;

            default:
                break;
        }
    }

    public void resetti()
    {
        codeLineIndex = 0;
        for(int ids = 0; ids < CodeWindow.transform.childCount; ids++)
        {
            CodeWindow.transform.GetChild(ids).gameObject.SetActive(false);
            CodeWindow.transform.GetChild(ids).GetChild(0).gameObject.SetActive(true);
            CodeWindow.transform.GetChild(ids).GetChild(1).GetChild(0).gameObject.SetActive(true);
            CodeWindow.transform.GetChild(ids).GetChild(1).GetChild(1).gameObject.SetActive(false);
            CodeWindow.transform.GetChild(ids).GetChild(1).GetChild(2).gameObject.SetActive(false);
        }
    }

    public void OnPress(Button btn)
    {
        if (!eStop && pStop && !tStop)
        {
            switch (btn.name)
            {
                case "BtnA1Plus": BtnA1PlusOn = true; break;
                case "BtnA2Plus": BtnA2PlusOn = true; break;
                case "BtnA3Plus": BtnA3PlusOn = true; break;
                case "BtnA4Plus": BtnA4PlusOn = true; break;
                case "BtnA5Plus": BtnA5PlusOn = true; break;
                case "BtnA6Plus": BtnA6PlusOn = true; break;

                case "BtnA1Minus": BtnA1MinusOn = true; break;
                case "BtnA2Minus": BtnA2MinusOn = true; break;
                case "BtnA3Minus": BtnA3MinusOn = true; break;
                case "BtnA4Minus": BtnA4MinusOn = true; break;
                case "BtnA5Minus": BtnA5MinusOn = true; break;
                case "BtnA6Minus": BtnA6MinusOn = true; break;

                case "BtnLesson": startLesson = true; break;
            }
        }


    }

    public void OnRelease()
    {
        BtnA1PlusOn = false;
        BtnA2PlusOn = false;
        BtnA3PlusOn = false;
        BtnA4PlusOn = false;
        BtnA5PlusOn = false;
        BtnA6PlusOn = false;

        BtnA1MinusOn = false;
        BtnA2MinusOn = false;
        BtnA3MinusOn = false;
        BtnA4MinusOn = false;
        BtnA5MinusOn = false;
        BtnA6MinusOn = false;
    }
}

