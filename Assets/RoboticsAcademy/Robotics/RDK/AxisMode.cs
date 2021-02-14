using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;
using UnityEngine.UI;
using System.Diagnostics;
using TMPro;

public class AxisMode : MonoBehaviour
{
    public Transform A1;
    public Transform A2;
    public Transform A3;
    public Transform A4;
    public Transform A5;
    public Transform A6;

    //public GameObject arr1;
    //public GameObject arr2;
    //public GameObject arr3;
    //public GameObject arr4;
    //public GameObject arr5;
    //public GameObject arr6;
    //public GameObject arr7;
    //public GameObject arr8;
    //public GameObject arr9;
    //public GameObject arr10;
    //public GameObject arr11;
    //public GameObject arr12;
    //public GameObject arr13;
    //public GameObject arr14;
    //public GameObject arr15;
    //public GameObject arr16;
    //public GameObject arr17;
    //public GameObject arr18;

    private GameObject[] arrow;

    public Material selected;
    public Material defaul;

    //public GameObject bar1;
    //public GameObject bar2;
    //public GameObject bar3;
    //public GameObject bar4;
    //public GameObject bar5;
    //public GameObject bar6;

    private ButtonManagerScript bhs;

    //public TextMeshProUGUI a1Text;
    //public TextMeshProUGUI a2Text;
    //public TextMeshProUGUI a3Text;
    //public TextMeshProUGUI a4Text;
    //public TextMeshProUGUI a5Text;
    //public TextMeshProUGUI a6Text;

    public bool online;
    public bool startOff;
    private Animator a;
    
    private bool start = true;
    private bool start2 = true;
    private bool start3 = true;
    public bool t1 = true;
    public float stop1 = 0f;
    public float stop2 = 90f;
    public float stop3 = 90f;
    public float stop4 = 0f;
    public float stop5 = 0f;
    public float stop6 = 0f;
    public float degMov;
    public float movMod = 1;
    public float startMov = 15;
    public float maxT1 = 10;
    public float jogSpeed = 50f;
    private float buttonDelay = 0.1f;
    private GameObject axis;
    RoboDK RDK = null;
    RoboDK.Item ROBOT = null;
    RoboDK.Item robFrame;
    private bool successConnect = false;
    private GameObject[] axes;
    private bool locK = false;
    private float f = 90f;
    // Start is called before the first frame update
    void Awake()
    {
        try
        {
            
            RDK = new RoboDK();
            successConnect = true;
            RDK.Render(false);
            ROBOT = RDK.getItem("KUKA KR 10 R1100 sixx");
            if (online)
            {

                robFrame = RDK.getItem("Tool 1");
                ROBOT.setPoseTool(robFrame);
                RDK.setRunMode(6);
            }
            axes = GameObject.FindGameObjectsWithTag("LessonAxis");

            A1 = axes[0].transform;
            A2 = axes[1].transform;
            A3 = axes[2].transform;
            A4 = axes[3].transform;
            A5 = axes[4].transform;
            A6 = axes[5].transform;
            //A2.transform.rotation = Quaternion.Euler(90, 0, 180);
            arrow = new GameObject[18];
            arrow = GameObject.FindGameObjectsWithTag("Arrow");
            bhs = GameObject.FindGameObjectWithTag("BMS").GetComponent<ButtonManagerScript>();
        }
        catch (Win32Exception e)
        {
            UnityEngine.Debug.Log("RoboDK is not installed");
        }
        //GameObject.Find("AxisBot").SetActive(false);
    }
    public void setT1(bool set)
    {
        t1 = set;
    }
    private void OnEnable()
    {
        //axis = GameObject.FindGameObjectWithTag("AxisRobot").transform.GetChild(0).gameObject;
        //axis.SetActive(true);
        double[] a = new double[6];
        a = ROBOT.Joints();
        stop1 = (float)a[0];
        stop2 = -1f * (float)a[1];
        stop3 = (float)a[2];
        stop4 = (float)a[3];
        stop5 = (float)a[4];
        stop6 = (float)a[5];
    }
    private void OnDisable()
    {
        double[] a = new double[6];
        a[0] = stop1;
        a[1] = -1f * stop2;
        a[2] = stop3;
        a[3] = stop4;
        a[4] = stop5;
        a[5] = stop6;
        ROBOT.setJoints(a);
    }
    public Transform getAxis1()
    {
        return A1;
    }
    public Transform getAxis2()
    {
        return A2;
    }
    public Transform getAxis3()
    {
        return A3;
    }
    public Transform getAxis4()
    {
        return A4;
    }
    public Transform getAxis5()
    {
        return A5;
    }
    public Transform getAxis6()
    {
        return A6;
    }

   

    private void resetScaleAndMaterial()
    {
        arrow[0].transform.localScale = new Vector3(1f, 1f, 1f);
        arrow[1].transform.localScale = new Vector3(1f, 1f, 1f);
        arrow[2].transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
        arrow[3].transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
        arrow[4].transform.localScale = new Vector3(0.32f, 0.32f, 0.32f);
        arrow[5].transform.localScale = new Vector3(0.30f, 0.30f, 0.3f);
        arrow[6].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[7].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[8].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[9].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[10].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        arrow[11].transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        arrow[12].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[13].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[14].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[15].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[16].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[17].transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        arrow[0].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[1].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[2].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[3].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[4].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[5].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[6].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[7].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[8].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[9].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[10].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[11].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[12].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[13].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[14].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[15].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[16].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        arrow[17].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = defaul;
        start = true;
        
    }

    private void setJoint()
    {
        if(online)
        {
            double[] newJoints = new double[6] { stop1, stop2, stop3, stop4, stop5, stop6 };
            ROBOT.MoveJ(newJoints, false);
        }
        
    }
    IEnumerator RotateMe(Transform A, Vector3 byAngles, float inTime, bool started)
    {
        locK = true;
        if (started && !bhs.eStop && bhs.pStop)
        {
            //var fromAngle = A.localRotation;
            var fromAngle = A.rotation;

            //print("FROM: " + fromAngle.eulerAngles.x);
            //var toAngle = Quaternion.Euler(A.eulerAngles + byAngles);

            //print("TO: by" + byAngles.x);
            //var toAngle = Quaternion.Euler(A.localEulerAngles + new Vector3(-10,0,0));
            var toAngle = A.GetChild(2).rotation;

            

            // define an axis, usually just up
            


            // mock rotate the axis with each quaternion
            








            if (byAngles.x < 0)
            {
                toAngle = A.GetChild(2).rotation;
            }
            else
            {
                toAngle = A.GetChild(3).rotation;
            }
            //float g = jogSpeed / 100f;
            //Quaternion.FromToRotation(fromAngle, byAngles);
            //print(Quaternion.Angle(fromAngle, Quaternion.Euler(new Vector3(360,0,180))));
            //print("TO: EULER " + toAngle.eulerAngles.x);
            for (var t = 0f; t < 1; t += Time.deltaTime)
            {
                A.rotation = Quaternion.Lerp(fromAngle, toAngle, t);
                
                //A.localRotation = Quaternion.Lerp(fromAngle, toAngle, t);
            }
            
            //           float tim = Time.deltaTime;
            //if(byAngles.x < 0)
            //{
            //    if()
            //    stop3 -= 10f * tim;
            //}

            //            A.localRotation = Quaternion.RotateTowards(fromAngle, toAngle, 10f * tim);
            yield return null;
            //}
            //A.localRotation = toAngle;
        }
        locK = false;
    }
    // Update is called once per frame
    void Update()
    {
        
        degMov = startMov * movMod;
        if(t1 && degMov > maxT1)
        {
            degMov = maxT1;
        }
        degMov = degMov * (jogSpeed/50.0f);

        if (successConnect)
        {
            if(buttonDelay < 0.1)
            {
                buttonDelay += Time.deltaTime;
            }
            if (!ROBOT.Busy() && buttonDelay >= 0.1)
            {
                
                var forwardA = A1.localRotation * Vector3.right;
                var forwardB = Quaternion.Euler(0, 0, -180) * Vector3.right;


                // now we need to compute the actual 2D rotation projections on the base plane
                var angleA = Mathf.Atan2(forwardA.x, forwardA.z) * Mathf.Rad2Deg;
                var angleB = Mathf.Atan2(forwardB.x, forwardB.z) * Mathf.Rad2Deg;


                // get the signed difference in these angles
                stop1 = Mathf.DeltaAngle(angleA, angleB) * -1f;
                //print(angleDiff);
                stop2 = Quaternion.Angle(A2.localRotation, Quaternion.Euler(new Vector3(360, 0, 180)));
                forwardA = A3.localRotation * Vector3.forward;
                forwardB = Quaternion.Euler(90, 0, 0) * Vector3.forward;


                // now we need to compute the actual 2D rotation projections on the base plane
                angleA = Mathf.Atan2(forwardA.y, forwardA.z) * Mathf.Rad2Deg;
                angleB = Mathf.Atan2(forwardB.y, forwardB.z) * Mathf.Rad2Deg;


                // get the signed difference in these angles
                stop3 = Mathf.DeltaAngle(angleA, angleB) + 90f;
                if (stop3 > 200)
                {
                    stop3 -= 360;
                }

                forwardA = A4.localRotation * Vector3.up;
                forwardB = Quaternion.Euler(0, -180, 0) * Vector3.up;
                angleA = Mathf.Atan2(forwardA.x, forwardA.y) * Mathf.Rad2Deg;
                angleB = Mathf.Atan2(forwardB.x, forwardB.y) * Mathf.Rad2Deg;
                stop4 = Mathf.DeltaAngle(angleA, angleB);
                //stop4 = Quaternion.Angle(A4.localRotation, Quaternion.Euler(new Vector3(0, 180, 360)));
                forwardA = A5.localRotation * Vector3.forward;
                forwardB = Quaternion.Euler(0, -180, 0) * Vector3.forward;
                angleA = Mathf.Atan2(forwardA.y, forwardA.z) * Mathf.Rad2Deg;
                angleB = Mathf.Atan2(forwardB.y, forwardB.z) * Mathf.Rad2Deg;
                stop5 = Mathf.DeltaAngle(angleA, angleB) * -1f;
                //stop5 = Quaternion.Angle(A5.localRotation, Quaternion.Euler(new Vector3(360, -180, 0)));
                //stop6 = Quaternion.Angle(A6.localRotation, Quaternion.Euler(new Vector3(0, 180, 360)));
                if (A3.transform.position.y < 1.429199f && stop2 < 50f)
                {
                    stop2 = stop2 * -1;
                }
                if(A3.transform.position.y < 1.429199f && stop2 > 160f)
                {
                    stop2 += 2 * Quaternion.Angle(A2.localRotation, Quaternion.Euler(new Vector3(180, 0, 180)));
                }
                //print(Quaternion.Angle(A2.localRotation, Quaternion.Euler(new Vector3(180, 0, 180))));
                //if (stop1 > 180)
                //{
                //    stop1 = stop1 - 360;
                //}
                //if(stop1 < -180)
                //{
                //    stop1 = stop1 + 360;
                //}
                //if (stop2 > 0)
                //{
                //    stop2 = stop2 - 180;
                //}
                //if (stop2 < 0)
                //{
                //    stop2 = stop2 + 360;
                //}
                if (bhs.BtnA1PlusOn)
                {
                    buttonDelay = 0;
                    if (arrow[1].activeSelf != true || start)
                    {
                        arrow[1].SetActive(true);
                        arrow[0].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[1].transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                        arrow[1].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                        start = false;
                    }
                    if (stop1 < 170f)
                    {
                        //stop1 = A1.localEulerAngles.y;
                        if (degMov + stop1 >= 170)
                        {
                            degMov = 170 - stop1;
                        }
                        StopAllCoroutines();
                        StartCoroutine(RotateMe(A1, new Vector3(1, degMov, 0), 1, bhs.BtnA1PlusOn));
                        setJoint();
                    }

                }
                else if (bhs.BtnA1MinusOn)
                {
                    buttonDelay = 0;
                    if (arrow[0].activeSelf != true || start)
                    {
                        arrow[0].SetActive(true);
                        arrow[1].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[0].transform.localScale = new Vector3(1.25f, 1.25f, 1.25f);
                        arrow[0].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop1 > -170f)
                    {
                        
                        if (stop1 - degMov <= -170)
                        {
                            degMov = stop1 + 170;
                        }
                        //A1.Rotate(0, degMov, 0);
                        //bar1.transform.localScale += new Vector3(1, 0, 0);
                        StopAllCoroutines();
                        StartCoroutine(RotateMe(A1, new Vector3(-1, -degMov, 0), 1, bhs.BtnA1MinusOn));
                        setJoint();
                    }

                }
                else if (bhs.BtnA2PlusOn)
                {
                    buttonDelay = 0;
                    if (arrow[2].activeSelf != true || start)
                    {
                        arrow[2].SetActive(true);
                        arrow[3].SetActive(true);
                        arrow[4].SetActive(false);
                        arrow[5].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[2].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[3].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[2].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                        arrow[3].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    //if (stop2 - f < 0)
                    //{
                    //    stop2 = stop2 * -1f;
                    //}
                    if (stop2 <= 191f && stop2 > -45f)
                    {

                        //if (stop2 - degMov <= -45f)
                        //{
                        //    degMov = stop2 + 45f;
                        //}
                        StopAllCoroutines();
                        //if (stop2 >= 135f)
                        //{
                        //    StartCoroutine(RotateMe(A2, new Vector3(133f, 0, 180f), 1, bhs.BtnA2PlusOn));
                        //}
                        //else
                        //{
                            StartCoroutine(RotateMe(A2, new Vector3(-45f, 0, 180f), 1, bhs.BtnA2PlusOn));
                        //}
                        
                        //A2.Rotate(degMov, 0, 0);
                        //bar2.transform.localScale += new Vector3(1, 0, 0);
                        //stop2 += degMov;
                        setJoint();
                    }
                }
                else if (bhs.BtnA2MinusOn)
                {
                    buttonDelay = 0;
                    if (arrow[4].activeSelf != true || start)
                    {
                        arrow[4].SetActive(true);
                        arrow[5].SetActive(true);
                        arrow[2].SetActive(false);
                        arrow[3].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[4].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[5].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[4].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                        arrow[5].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop2 < 190f && stop2 >= -46f)
                    {
                        //if (degMov + stop2 >= 190f)
                        //{
                        //    degMov = 190f - stop2;
                        //}
                        StopAllCoroutines();
                        //if (stop2 < 10f)
                        //{
                        //    StartCoroutine(RotateMe(A2, new Vector3(11f, 0, 180f), 1, bhs.BtnA2MinusOn));
                        //}
                        //else
                        //{
                            StartCoroutine(RotateMe(A2, new Vector3(190, 0, 180), 1, bhs.BtnA2MinusOn));
                        //}
                        //A2.Rotate(-degMov, 0, 0);
                        //bar2.transform.localScale += new Vector3(-1, 0, 0);
                        //stop2 -= degMov;
                        setJoint();
                    }
                }
                else if (bhs.BtnA3PlusOn)
                {
                    buttonDelay = 0;
                    if (arrow[6].activeSelf != true || start)
                    {
                        arrow[6].SetActive(true);
                        arrow[7].SetActive(true);
                        arrow[8].SetActive(false);
                        arrow[9].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[6].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[7].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[6].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                        arrow[7].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop3 < 156f)
                    {
                        //A3.Rotate(degMov, 0, 0);
                        ////bar3.transform.localScale += new Vector3(1, 0, 0);
                        //stop3 += degMov;
                        //stop2 >= 135f
                        StopAllCoroutines();
                        //if (false)
                        //{
                        //    StartCoroutine(RotateMe(A3, new Vector3(133f, 0, 180f), 1, bhs.BtnA2PlusOn));
                        //}
                        //else
                        //{
                        StartCoroutine(RotateMe(A3, new Vector3(-156f, 0, 0), 1, bhs.BtnA3PlusOn));
                        //}
                        setJoint();
                    }

                }
                else if (bhs.BtnA3MinusOn)
                {
                    buttonDelay = 0;
                    if (arrow[8].activeSelf != true || start)
                    {
                        arrow[8].SetActive(true);
                        arrow[9].SetActive(true);
                        arrow[6].SetActive(false);
                        arrow[7].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[8].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[9].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[8].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                        arrow[9].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop3 > -120f)
                    {
                        //A3.Rotate(-degMov, 0, 0);
                        ////bar3.transform.localScale += new Vector3(-1, 0, 0);
                        //stop3 -= degMov;
                        //if (false)
                        //{
                        //    StartCoroutine(RotateMe(A3, new Vector3(133f, 0, 180f), 1, bhs.BtnA2PlusOn));
                        //}
                        //else
                        //{
                        StartCoroutine(RotateMe(A3, new Vector3(120f, 0, 0), 1, bhs.BtnA3MinusOn));
                        //}
                        setJoint();
                    }
                }
                else if (bhs.BtnA4PlusOn)
                {
                    buttonDelay = 0;
                    if (arrow[10].activeSelf != true || start)
                    {
                        arrow[10].SetActive(true);
                        arrow[11].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[10].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[10].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop4 < 179)
                    {
                        //A4.Rotate(0, 0, -degMov);
                        ////bar4.transform.localScale += new Vector3(1, 0, 0);
                        //stop4 += degMov;
                        StartCoroutine(RotateMe(A4, new Vector3(120f, 0, 0), 1, bhs.BtnA4PlusOn));
                        setJoint();
                    }

                }
                else if (bhs.BtnA4MinusOn)
                {
                    buttonDelay = 0;
                    if (arrow[11].activeSelf != true || start)
                    {
                        arrow[11].SetActive(true);
                        arrow[10].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[11].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[11].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop4 > -179)
                    {
                        StartCoroutine(RotateMe(A4, new Vector3(-120f, 0, 0), 1, bhs.BtnA4MinusOn));
                        setJoint();
                    }

                }
                else if (bhs.BtnA5PlusOn)
                {
                    buttonDelay = 0;
                    if (arrow[12].activeSelf != true || start)
                    {
                        UnityEngine.Debug.Log("Movin");
                        arrow[12].SetActive(true);
                        arrow[13].SetActive(true);
                        arrow[14].SetActive(false);
                        arrow[15].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[12].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[13].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[12].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                        arrow[13].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop5 < 120)
                    {
                        StartCoroutine(RotateMe(A5, new Vector3(-120f, 0, 0), 1, bhs.BtnA5PlusOn));
                        setJoint();
                    }

                }
                else if (bhs.BtnA5MinusOn)
                {
                    buttonDelay = 0;
                    if (arrow[14].activeSelf != true || start)
                    {
                        arrow[14].SetActive(true);
                        arrow[15].SetActive(true);
                        arrow[12].SetActive(false);
                        arrow[13].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[14].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[15].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[14].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                        arrow[15].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop5 > -120)
                    {
                        StartCoroutine(RotateMe(A5, new Vector3(120f, 0, 0), 1, bhs.BtnA5MinusOn));
                        setJoint();
                    }
                }
                else if (bhs.BtnA6PlusOn)
                {
                    buttonDelay = 0;
                    if (arrow[16].activeSelf != true || start)
                    {
                        arrow[16].SetActive(true);
                        arrow[17].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[16].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[16].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop6 < 350)
                    {
                        StartCoroutine(RotateMe(A6, new Vector3(120f, 0, 0), 1, bhs.BtnA6PlusOn));
                        setJoint();
                    }

                }
                else if (bhs.BtnA6MinusOn)
                {
                    buttonDelay = 0;
                    if (arrow[17].activeSelf != true || start)
                    {
                        arrow[17].SetActive(true);
                        arrow[16].SetActive(false);
                        resetScaleAndMaterial();
                        arrow[17].transform.localScale = new Vector3(.5f, .5f, .5f);
                        arrow[17].transform.GetChild(0).gameObject.GetComponent<MeshRenderer>().material = selected;
                    }
                    if (stop6 > -350)
                    {
                        StartCoroutine(RotateMe(A6, new Vector3(-120f, 0, 0), 1, bhs.BtnA6MinusOn));
                        setJoint();
                    }
                }
                else
                {
                    StopAllCoroutines();
                }
                //a1Text.SetText(stop1.ToString());
                //a2Text.SetText(stop2.ToString());
                //a3Text.SetText(stop3.ToString());
                //a4Text.SetText(stop4.ToString());
                //a5Text.SetText(stop5.ToString());
                //a6Text.SetText(stop6.ToString());
            }

        }
    }
}
