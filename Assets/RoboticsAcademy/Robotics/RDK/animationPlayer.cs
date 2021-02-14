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

public class animationPlayer : MonoBehaviour
{
    // Start is called before the first frame update
    public TextAsset animationFile;
    public bool useDefaultRobot = true; 
    public Transform A1;
    public Transform A2;
    public Transform A3;
    public Transform A4;
    public Transform A5;
    public Transform A6;
    private GameObject[] axes;
    int parseInt = 10;
    void Start()
    {
        if(useDefaultRobot)
        {
            axes = GameObject.FindGameObjectsWithTag("LessonAxis");
            A1 = axes[0].transform;
            A2 = axes[1].transform;
            A3 = axes[2].transform;
            A4 = axes[3].transform;
            A5 = axes[4].transform;
            A6 = axes[5].transform;
        }

        Run();

    }

    public void Run()
    {
        StartCoroutine(StartMove(animationFile, A1, A2, A3, A4, A5, A6));

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Run();
        }

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
        
}
