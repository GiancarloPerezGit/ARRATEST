using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AxisLogging : MonoBehaviour
{
    // Start is called before the first frame update
    private InputField A1;
    private InputField A2;
    private InputField A3;
    private InputField A4;
    private InputField A5;
    private InputField A6;
    private GameObject LessonBot;
    private GameObject[] AxisList;
    private bool toggle = false;
    private bool start = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!toggle)
        {
            if(GameObject.FindGameObjectWithTag("LessonRobot"))
            {
                LessonBot = GameObject.FindGameObjectWithTag("LessonRobot");
                toggle = true;
            }
        }
        else if(!start && toggle)
        {
            AxisList = new GameObject[6];
            AxisList = GameObject.FindGameObjectsWithTag("LessonAxis");
            start = true;
        }
        else if(start && toggle)
        {
            A1.text = AxisList[0].transform.rotation.y.ToString();
            A2.text = AxisList[1].transform.rotation.x.ToString();
            A3.text = AxisList[2].transform.rotation.x.ToString();
            A4.text = AxisList[3].transform.rotation.z.ToString();
            A5.text = AxisList[4].transform.rotation.x.ToString();
            A6.text = AxisList[5].transform.rotation.z.ToString();
        }
    }
}
