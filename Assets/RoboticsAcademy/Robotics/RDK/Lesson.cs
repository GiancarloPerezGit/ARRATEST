using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Lesson : MonoBehaviour
{
    public AxisMode am;
    public Material armColor;

    public MeshRenderer axisMaterial;

    public Transform[] Axes;

    private GameObject ghost;
    private GameObject complete;

    private Color Default = new Color(1.0f, 1.0f, 1.0f, 0.5f);
    private Color Completed = new Color(25f/255f, 1.0f, 0f, 0.5f);

    public bool lesson1Done = false;
    // Start is called before the first frame update

    void Awake()
    {

        Axes = new Transform[6];
        Axes[0] = GameObject.Find("[environment]/AxisBot/AxisBot/LessonBot/robot_GRP/robot_GEOM/Base/axis1").transform;
        Axes[1] = GameObject.Find("[environment]/AxisBot/AxisBot/LessonBot/robot_GRP/robot_GEOM/Base/axis1/axis2").transform;
        Axes[2] = GameObject.Find("[environment]/AxisBot/AxisBot/LessonBot/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3").transform;
        Axes[3] = GameObject.Find("[environment]/AxisBot/AxisBot/LessonBot/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3/axis4").transform;
        Axes[4] = GameObject.Find("[environment]/AxisBot/AxisBot/LessonBot/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3/axis4/axis5").transform;
        Axes[5] = GameObject.Find("[environment]/AxisBot/AxisBot/LessonBot/robot_GRP/robot_GEOM/Base/axis1/axis2/axis3/axis4/axis5/axis6").transform;

        ghost = GameObject.Find("[environment]/AxisBot/AxisBot/LessonBot");
        complete = GameObject.Find("[environment]/AxisBot/AxisBot/LessonBot Complete");


    }
    private bool checkAxis(Transform axis, int index)
    {
        if(index == 0)
        {
            if (Mathf.Round(axis.rotation.eulerAngles.y) == Mathf.Round(Axes[index].rotation.eulerAngles.y))
            {
                return true;
            }
        }
        else if(index == 1 || index == 2 || index == 4)
        {
            if (Mathf.Round(axis.rotation.eulerAngles.x) == Mathf.Round(Axes[index].rotation.eulerAngles.x))
            {
                return true;
            }
        }
        else if(index == 3 || index == 5)
        {
            if (Mathf.Round(axis.rotation.eulerAngles.z) == Mathf.Round(Axes[index].rotation.eulerAngles.z))
            {
                return true;
            }
        }
        
        return false;
    }
    // Update is called once per frame
    void Update()
    {
        //Compares every axis to the associated axis on the lesson robot
        //First if statement checks if the lesson has finished already or if the lesson robot is inactive
        if(ghost.activeSelf && !lesson1Done)
        {
            
            if (checkAxis(am.getAxis1(), 0))
            {
                //print("A1 Good");
                if (checkAxis(am.getAxis2(), 1))
                {
                    //print("A2 Good");
                    if (checkAxis(am.getAxis3(), 2))
                    {
                        //print("A3 Good");
                        if (checkAxis(am.getAxis4(), 3))
                        {
                            //print("A4 Good");
                            if (checkAxis(am.getAxis5(), 4))
                            {
                                //print("A5 Good");
                                if (checkAxis(am.getAxis6(), 5))
                                {
                                    lesson1Done = true;
                                    //This area is called only once immediately after the lesson is completed
                                }
                            }
                        }
                    }
                }
            }
        }
        if(lesson1Done)
        {
            ghost.SetActive(false);
            complete.SetActive(true);            
        }
        //else
        //{
        //    ghost.SetActive(true);
        //    complete.SetActive(false);
        //}

    }

}
