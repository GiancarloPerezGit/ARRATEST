using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoboticAcademyLessonScript : MonoBehaviour
{
    public GameObject teleTargetTutorial;
    public GameObject teleTargetPNP;
    public GameObject teleTargetAxis;
    public GameObject teleTargetPNPBehind;

    public GameObject logoObject;
    public GameObject clickTarget;

    public GameObject teleportGraphic;
    public GameObject turnGraphic;
    public GameObject clickGraphic;
    public GameObject dashboardGraphic;

    public GameObject Dashboard;

    public GameObject Teleport;

    public GameObject workEvelope;
    public GameObject coverPanel;

    public GameObject axisBot;
    public GameObject highlightBot;
    public GameObject lessonBot;

    public GameObject UIarrows;

    public GameObject lessonAnimation;

    public GameObject lesson1CompleteBox;

    public GameObject lessonAudio;

    public GameObject pnpSceneObject;

    public GameObject pnpLights;

    public GameObject clickGraphicPNP;

    private bool teleTurnPressed = false;
    private bool menuButtonPressed = false;
    private bool targetPressed = false;

    //these veriable need to be moved back into the lesson 0-3a, 0-3b, and 0-3c
    private bool completeTele = false;
    private bool completeView = false;
    private bool completeClick = false;

    //private
    private Animator pnpKR420Animation;
    private Animator pnpKR30Animation;
    private Animator pnpToolAnimation;
    private Animator pnpBrickAnimation;


    //lesson 1-5 boolean
    private bool eStopDisengaged = false;
    private bool t1varified = false;
    private bool enableingSwitchOn = false;
    private bool driverOn = false;

    //

    public void OnTargetClick()
    {
        //when the user turns using the track pad signal true
        targetPressed = true;
        Debug.Log("turning");
    }

    public void OnClick(Button btn)
    {
        switch (btn.name)
        {
            case "EmergencySwitchOff":
                eStopDisengaged = true;
                break;
        }
    }
    public void OnTurn()
    {
        //when the user turns using the track pad signal true
        teleTurnPressed = true;
        Debug.Log("turning");
    }

    public void OnOpenDashboard()
    {
        //when the dashboard button is pressed
        menuButtonPressed = true;
    }

    //on the start of the project, run the first co-routine
    private void Awake()
    {
        highlightBot.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);

        pnpKR420Animation = pnpSceneObject.transform.GetChild(0).GetComponent<Animator>();
        pnpKR30Animation = pnpSceneObject.transform.GetChild(1).GetChild(0).GetComponent<Animator>();
        pnpBrickAnimation = pnpSceneObject.transform.GetChild(6).GetComponent<Animator>();
        pnpToolAnimation = pnpSceneObject.transform.GetChild(7).GetComponent<Animator>();

        StartCoroutine("Lesson0_7");

    }

    //this is a list of enumerators that can be turned on and off and activated on and off of sequence
    //basically a sequencer...
    public void pnpAnimatorControllerSpeed(float s)
    {
        pnpKR420Animation.speed = s;
        pnpKR30Animation.speed = s;
        pnpBrickAnimation.speed = s;
        pnpToolAnimation.speed = s;
    }

    //--LESSON 0--VR TUTORIAL//
    IEnumerator Lesson0_1()
    {

        pnpAnimatorControllerSpeed(0);

        Debug.Log(lessonAudio.transform.GetChild(0).GetChild(0).GetComponent<AudioSource>().clip.length);
        //animation for the RA loop plays
        Teleport.SetActive(false);
        logoObject.SetActive(true);

        lessonAudio.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(0).GetComponent<AudioSource>().clip.length);
        lessonAudio.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        logoObject.SetActive(false);
        StartCoroutine("Lesson0_2");
    }

    IEnumerator Lesson0_2()
    {
        StopCoroutine("Lesson0_1");
        Debug.Log("lesson started 0-2");
        lessonAudio.transform.GetChild(0).GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(1).GetComponent<AudioSource>().clip.length);
        lessonAudio.transform.GetChild(0).GetChild(1).gameObject.SetActive(false);
        StartCoroutine("Lesson0_3a");
    }

    IEnumerator Lesson0_3a()
    {
        StopCoroutine("Lesson0_2");
        Debug.Log("lesson started 0-3a");
        lessonAudio.transform.GetChild(0).GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        teleportGraphic.SetActive(true);
        yield return new WaitForSeconds(12);
        teleTargetTutorial.SetActive(true);
        yield return new WaitForSeconds(7);
        Teleport.SetActive(true);

        while (completeTele!= true)
        {
            if (teleTargetTutorial.GetComponent<TargetCheck>().targetTeleported == true)
            {
                completeTele = true;
                Debug.Log("teleport tutorial completed");
                teleTargetTutorial.SetActive(false);
                teleportGraphic.SetActive(false);
                lessonAudio.transform.GetChild(0).GetChild(2).gameObject.SetActive(false);

                StartCoroutine("Lesson0_3b");
            }
            yield return null;
        }
    }

    IEnumerator Lesson0_3b()
    {
        StopCoroutine("Lesson0_3a");
        Debug.Log("lesson started 0-3b");
        lessonAudio.transform.GetChild(0).GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(7);
        turnGraphic.SetActive(true);
        teleTurnPressed = false;
        yield return new WaitForSeconds(21);

        while (completeView != true)
        {
            if (teleTurnPressed == true)
            {
                completeView = true;
                Debug.Log("turn tutorial completed");
                turnGraphic.SetActive(false);
                lessonAudio.transform.GetChild(0).GetChild(3).gameObject.SetActive(false);
                StartCoroutine("Lesson0_3c");
            }
            yield return null;
        }
    }

    IEnumerator Lesson0_3c()
    {
        StopCoroutine("Lesson0_3b");
        Debug.Log("lesson started 0-3c");
        lessonAudio.transform.GetChild(0).GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);

        clickTarget.SetActive(true);
        clickGraphic.SetActive(true);
        while (completeClick != true)
        {
            if (targetPressed == true)
            {
                completeClick = true;
                Debug.Log("click tutorial completed");
                clickTarget.SetActive(false);
                clickGraphic.SetActive(false);
                lessonAudio.transform.GetChild(0).GetChild(4).gameObject.SetActive(false);
                StartCoroutine("Lesson0_4");
            }
            yield return null;
        }
    }
    IEnumerator Lesson0_4()
    {
        Debug.Log("obnoxious code gulag complete");
        StopCoroutine("Lesson0_3c");
        Debug.Log("lesson started 0-4");
        lessonAudio.transform.GetChild(0).GetChild(5).gameObject.SetActive(true);
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(5).GetComponent<AudioSource>().clip.length);
        dashboardGraphic.SetActive(true);

        menuButtonPressed = false;

        while (menuButtonPressed != true)
        {
            lessonAudio.transform.GetChild(0).GetChild(5).gameObject.SetActive(false);
            yield return null;
        }
        dashboardGraphic.SetActive(false);
        StartCoroutine("Lesson0_5");
    }
    IEnumerator Lesson0_5()
    {
        StopCoroutine("Lesson0_4");
        Debug.Log("lesson Started 0-5");
        //bool completeDashboard = false;

        //voice over to go through the dashboard
        menuButtonPressed = false;

        lessonAudio.transform.GetChild(0).GetChild(6).gameObject.SetActive(true);
        Dashboard.transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0).GetComponent<Toggle>().isOn = true;
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(6).GetComponent<AudioSource>().clip.length);
        lessonAudio.transform.GetChild(0).GetChild(6).gameObject.SetActive(false);

        lessonAudio.transform.GetChild(0).GetChild(7).gameObject.SetActive(true);
        Dashboard.transform.GetChild(0).GetChild(0).GetChild(1).GetChild(0).GetComponent<Toggle>().isOn = true;
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(7).GetComponent<AudioSource>().clip.length);
        lessonAudio.transform.GetChild(0).GetChild(7).gameObject.SetActive(false);

        lessonAudio.transform.GetChild(0).GetChild(8).gameObject.SetActive(true);
        Dashboard.transform.GetChild(0).GetChild(0).GetChild(2).GetChild(0).GetComponent<Toggle>().isOn = true;
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(8).GetComponent<AudioSource>().clip.length);
        lessonAudio.transform.GetChild(0).GetChild(8).gameObject.SetActive(false);

        lessonAudio.transform.GetChild(0).GetChild(9).gameObject.SetActive(true);
        Dashboard.transform.GetChild(0).GetChild(0).GetChild(3).GetChild(0).GetComponent<Toggle>().isOn = true;
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(9).GetComponent<AudioSource>().clip.length);
        lessonAudio.transform.GetChild(0).GetChild(9).gameObject.SetActive(false);

        lessonAudio.transform.GetChild(0).GetChild(10).gameObject.SetActive(true);
        Dashboard.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetComponent<Toggle>().isOn = true;
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(10).GetComponent<AudioSource>().clip.length);
        lessonAudio.transform.GetChild(0).GetChild(10).gameObject.SetActive(false);

        yield return new WaitForSeconds(5);
        Dashboard.transform.GetChild(0).GetChild(0).GetChild(4).GetChild(0).GetComponent<Toggle>().isOn = false;
        //completeDashboard = true;
        
        while (menuButtonPressed != true)
        {
            yield return null;
     
        }
        StartCoroutine("Lesson0_6");
    }
    IEnumerator Lesson0_6()
    {
        pnpAnimatorControllerSpeed(0);

        Debug.Log("lesson Started 0-6");
        lessonAudio.transform.GetChild(0).GetChild(11).gameObject.SetActive(true);
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(11).GetComponent<AudioSource>().clip.length);

        teleTargetPNP.SetActive(true);
        lessonAudio.transform.GetChild(0).GetChild(11).gameObject.SetActive(false);
        completeTele = false;

        while (completeTele != true)
        {
            if (teleTargetPNP.GetComponent<TargetCheck>().targetTeleported == true)
            {
                completeTele = true;
                Debug.Log("PNP teleport completed");
                teleTargetPNP.SetActive(false);

                //end instruction.
                StartCoroutine("Lesson0_7");
            }
            yield return null;
        }
    }
    IEnumerator Lesson0_7()
    {
        pnpAnimatorControllerSpeed(0);
        StopCoroutine("Lesson0_6");
        Debug.Log("lesson Started 0-7");
        completeTele = false;
        targetPressed = false;

        lessonAudio.transform.GetChild(0).GetChild(12).gameObject.SetActive(true);
        yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(12).GetComponent<AudioSource>().clip.length);
        lessonAudio.transform.GetChild(0).GetChild(12).gameObject.SetActive(false);
        teleTargetPNPBehind.SetActive(true);

        while (completeTele != true)
        {
            if (teleTargetPNPBehind.GetComponent<TargetCheck>().targetTeleported == true)
            {

                completeTele = true;
                Debug.Log("PNP teleport completed");
                teleTargetPNPBehind.SetActive(false);

                pnpAnimatorControllerSpeed(0.25f);
               

                lessonAudio.transform.GetChild(0).GetChild(13).gameObject.SetActive(true);
                yield return new WaitForSeconds(5);
                pnpAnimatorControllerSpeed(1);

                yield return new WaitForSeconds(12.5f);
                pnpAnimatorControllerSpeed(0);
                yield return new WaitForSeconds(1.5f);
                lessonAudio.transform.GetChild(0).GetChild(13).gameObject.SetActive(false);
                clickGraphicPNP.gameObject.SetActive(true);

                while (targetPressed != true)
                {
                    yield return null;
                }
                
                clickGraphicPNP.gameObject.SetActive(false);
                pnpAnimatorControllerSpeed(1);
                lessonAudio.transform.GetChild(0).GetChild(14).gameObject.SetActive(true);
                yield return new WaitForSeconds(lessonAudio.transform.GetChild(0).GetChild(14).GetComponent<AudioSource>().clip.length);
                lessonAudio.transform.GetChild(0).GetChild(13).gameObject.SetActive(false);

            }
            yield return null;
        }

        StartCoroutine("Lesson0_8");

    }
    IEnumerator Lesson0_8()
    {
        StopCoroutine("Lesson0_7");
        Debug.Log("lesson Started 0-8");
        //instructions to move to Axis
        teleTargetAxis.SetActive(true);
        completeTele = false;


        while (completeTele != true)
        {
            if (teleTargetAxis.GetComponent<TargetCheck>().targetTeleported == true)
            {
                completeTele = true;
                Debug.Log("Axis teleport completed");
                teleTargetAxis.SetActive(false);

                //end instruction.
                StartCoroutine("Lesson0_9");
            }
            yield return null;
        }

    }
    IEnumerator Lesson0_9()
    {
        StopCoroutine("Lesson0_8");
        Debug.Log("lesson Started 0-9");
        //start talk about the next steps. "open up the dashboard and select the next Lesson

        lesson1CompleteBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        //StartCoroutine("Lesson0_8");
    }

    //--LESSON 1--AXIS TUTORIAL//
    public void startLesson1()
    {
        lesson1CompleteBox.gameObject.SetActive(false);
        StartCoroutine("Lesson1_1");
    }

    IEnumerator Lesson1_1()
    {
        //overview explination of lesson
        Debug.Log("lesson Started 1-1");
        //overview explination of lesson
        yield return new WaitForSeconds(5);
        yield return null;
        StartCoroutine("Lesson1_2");
    }

    IEnumerator Lesson1_2()
    {
        
        Debug.Log("lesson Started 1-2");
        StopCoroutine("Lesson1_1");
        //explain the KR10
        yield return new WaitForSeconds(5);

        //Explain each axis with hightlight (NOTE: this needs to be cleaned up so much...)
        axisBot.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        axisBot.transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
        highlightBot.transform.GetChild(0).GetChild(0).gameObject.SetActive(false);

        axisBot.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        axisBot.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(true);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(0).gameObject.SetActive(false);

        axisBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        axisBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);

        axisBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        axisBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);

        axisBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);
        axisBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);

        axisBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);

        //Explain the axis 6 Flange
        yield return new WaitForSeconds(5);

        //explain the work envelope
        axisBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(true);
        highlightBot.transform.GetChild(0).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(1).GetChild(0).gameObject.SetActive(false);
        workEvelope.SetActive(true);
        yield return new WaitForSeconds(5);

        yield return null;
        StartCoroutine("Lesson1_3");
    }

    IEnumerator Lesson1_3()
    {
        Debug.Log("lesson Started 1-3");
        StopCoroutine("Lesson1_2");

        //use the evelope to explain distance
        yield return new WaitForSeconds(5);

        //Industrial Vs colabortive
        workEvelope.SetActive(false);
        yield return new WaitForSeconds(5);

        //In-scene safety
        yield return new WaitForSeconds(5);

        //E-Stop exsplination on the UI
        yield return new WaitForSeconds(5);

        yield return null;
        StartCoroutine("Lesson1_4");

    }
    IEnumerator Lesson1_4()
    {
        Debug.Log("lesson Started 1-4");
        StopCoroutine("Lesson1_3");
        coverPanel.SetActive(true);
        //e-stop explination
        UIarrows.transform.GetChild(0).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);

        //T1/T2/Auto explination
        UIarrows.transform.GetChild(0).gameObject.SetActive(false);
        UIarrows.transform.GetChild(1).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);

        //difference between Axis/world
        UIarrows.transform.GetChild(1).gameObject.SetActive(false);
        UIarrows.transform.GetChild(2).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);

        //Jog button expliantion
        UIarrows.transform.GetChild(2).gameObject.SetActive(false);
        UIarrows.transform.GetChild(3).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);

        //I/O button explination
        UIarrows.transform.GetChild(3).gameObject.SetActive(false);
        UIarrows.transform.GetChild(4).gameObject.SetActive(true);
        yield return new WaitForSeconds(5);

        UIarrows.transform.GetChild(4).gameObject.SetActive(false);
        coverPanel.SetActive(false);
        yield return null;
        StartCoroutine("Lesson1_5");

    }
    IEnumerator Lesson1_5()
    {
        Debug.Log("lesson Started 1-5");
        StopCoroutine("Lesson1_4");

        //turn on highlight, discuss the goal of this lesson
        lessonBot.SetActive(true);
        lessonAnimation.gameObject.GetComponent<animationPlayer>().Run();
        yield return new WaitForSeconds(5);

        //explination of the sequence of events
        yield return new WaitForSeconds(5);

        //Disengage e-stop
        yield return new WaitForSeconds(5);
        eStopDisengaged = false;
        while (eStopDisengaged != true)
        {
            //voice acting about e-stop
            Debug.Log("currently waiting for e-stop");
            yield return null;
            
        }

        Debug.Log("1) estop disengaged");

        //Varified T1
        //voiceline goes here
        yield return new WaitForSeconds(5);

        Debug.Log("2) Varified T1");

        //enabling switch
        yield return new WaitForSeconds(5);
        enableingSwitchOn = false;
        while (enableingSwitchOn != true)
        {
            //voice acting about enabling switch
            Debug.Log("3) enabling switch on");
            yield return null;
        }

        Debug.Log("2) enabling switch pressed");

        //drivers
        //voiceline goes here
        yield return new WaitForSeconds(5);

        Debug.Log("2) Varified T1");

        yield return new WaitForSeconds(5);

        yield return null;
        StartCoroutine("Lesson1_6");

    }
    IEnumerator Lesson1_6()
    {
        Debug.Log("lesson Started 1-6");
        StopCoroutine("Lesson1_5");


        //discuss the difference of axis Six 
        yield return null;
        StartCoroutine("Lesson1_7");

    }
    
}
