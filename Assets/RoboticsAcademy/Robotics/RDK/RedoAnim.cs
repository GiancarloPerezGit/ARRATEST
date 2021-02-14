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
using UnityEditor;
public class RedoAnim : MonoBehaviour
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
    //public Animation anim;
    public AnimationClip animClip1;
    public AnimationClip animClip2;
    public AnimationClip animClip3;
    public AnimationClip animClip4;
    public AnimationClip animClip5;
    public AnimationClip animClip6;
    Keyframe[] keys1;
    Keyframe[] keys2;
    Keyframe[] keys3;
    Keyframe[] keys4;
    Keyframe[] keys5;
    Keyframe[] keys6;
    Keyframe[] keys1a;
    Keyframe[] keys2a;
    Keyframe[] keys3a;
    Keyframe[] keys4a;
    Keyframe[] keys5a;
    Keyframe[] keys6a;
    Keyframe[] keys1b;
    Keyframe[] keys2b;
    Keyframe[] keys3b;
    Keyframe[] keys4b;
    Keyframe[] keys5b;
    Keyframe[] keys6b;
    public Animation A1A;
    public Animation A2A;
    public Animation A3A;
    public Animation A4A;
    public Animation A5A;
    public Animation A6A;

    public int frameNum;
    public int currFrame;
    public float mms = 1.0f;
    private float cmms = 0f;
    private float currMov = 1;
    void Start()
    {
        if (useDefaultRobot)
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
        StartCoroutine(StartMove(animationFile, A1, A2,A3,A4,A5,A6));

    }

    private void LateUpdate()
    {
        Vector3 store1 = A1.localEulerAngles;
        store1.x = 0f;
        store1.z = 180f;
        A1.localEulerAngles = store1;
        Vector3 store2 = A2.localEulerAngles;
        store2.y = 0f;
        store2.z = 180f;
        A2.localEulerAngles = store2;
        Vector3 store3 = A3.localEulerAngles;
        store3.y = 0f;
        store3.z = 0f;
        A3.localEulerAngles = store3;
        Vector3 store4 = A4.localEulerAngles;
        store4.x = 0f;
        store4.y = 180f;
        A4.localEulerAngles = store4;
        Vector3 store5 = A5.localEulerAngles;
        store5.y = 180f;
        store5.z = 0f;
        A5.localEulerAngles = store5;
        Vector3 store6 = A6.localEulerAngles;
        store6.x = 0f;
        store6.y = 180f;
        A6.localEulerAngles = store6;
    }

    private IEnumerator StartMove(TextAsset move, Transform A1, Transform A2, Transform A3, Transform A4, Transform A5, Transform A6)
    {
        String sJoints = move.text;
        String[] joints = Regex.Split(sJoints, "[,\r\n]+");
        print("JOINT NUM" + joints.Length);
        float[] simulMove = new float[6];
        int simulIndex = 0;
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
        while (parseInt < joints.Length)
        {
            print(parseInt);
            simulMove[simulIndex] = float.Parse(joints[parseInt]);
            if(parseInt % 10 == 0 && currMov != float.Parse(joints[parseInt + 9]))
            {
                currMov = float.Parse(joints[parseInt + 9]);
                print("Swapped move" + currMov);
                mms = mms / 2;
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
                yield return wait;
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

        for(int i = 0; i < frameNum; i++)
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
        A1A.AddClip(animClip1, "A1A");
        A2A.AddClip(animClip2, "A2A");
        A3A.AddClip(animClip3, "A3A");
        A4A.AddClip(animClip4, "A4A");
        A5A.AddClip(animClip5, "A5A");
        A6A.AddClip(animClip6, "A6A");
        print("DONE");
    }

}
