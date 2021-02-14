using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotionLesson : MonoBehaviour, StateChecker
{
    private GameObject lessonUI;
    public GameObject lessonBot;
    private GameObject player;
    private GameObject startPoint;
    private AxisMode am;
    private GameObject axisbot;
    private GameObject startCircle;
    public bool start = false;
    private StateMachine stateMachine;
    private int order;
    // Start is called before the first frame update
    void Awake()
    {
        lessonUI = GameObject.FindGameObjectWithTag("MotionUI");
        lessonBot = GameObject.FindGameObjectWithTag("LessonRobot");
        player = GameObject.FindGameObjectWithTag("MainCamera");
        //am = GameObject.FindGameObjectWithTag("RobotManager").GetComponent<AxisMode>();
        axisbot = GameObject.FindGameObjectWithTag("AxisRobot");
        lessonUI.SetActive(false);
        lessonBot.SetActive(false);
        stateMachine = GameObject.FindGameObjectWithTag("SM").GetComponent<StateMachine>();
        startCircle = GameObject.FindGameObjectWithTag("StartCircle");
    }
    public bool returnState()
    {
        return start;
    }
    public void execute()
    {
        if (stateMachine.changeState(order))
        {
            start = true;
            stateMachine.updateFlag(order, true);
        }

    }
    public void setOrder(int ord)
    {
        order = ord;
    }
    public void exitState()
    {
        lessonUI.SetActive(false);
        stateMachine.updateFlag(order, false);
    }
    // Update is called once per frame
    void Update()
    {

        if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(gameObject.transform.position.x, gameObject.transform.position.z)) < 1)
        {
            execute();
            if (start)
            {
                lessonUI.SetActive(true);
                lessonBot.SetActive(true);
                startCircle.SetActive(true);
                am.enabled = true;
            }

        }
    }
}
