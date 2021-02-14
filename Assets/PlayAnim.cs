using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAnim : MonoBehaviour
{
    public bool useDefaultRobot = true;
    public Transform A1;
    public Transform A2;
    public Transform A3;
    public Transform A4;
    public Transform A5;
    public Transform A6;
    private GameObject[] axes;
    public Animation A1A;
    public Animation A2A;
    public Animation A3A;
    public Animation A4A;
    public Animation A5A;
    public Animation A6A;
    // Start is called before the first frame update
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
        A1A = A1.GetComponent<Animation>();
        A2A = A2.GetComponent<Animation>();
        A3A = A3.GetComponent<Animation>();
        A4A = A4.GetComponent<Animation>();
        A5A = A5.GetComponent<Animation>();
        A6A = A6.GetComponent<Animation>();
        Run();
    }

    public void Run()
    {
        A1A.Play("Why");
        A2A.Play("Why2");
        A3A.Play("Why3");
        A4A.Play("Why4");
        A5A.Play("Why5");
        A6A.Play("Why6");
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        
        //Vector3 store1 = A1.localEulerAngles;
        //store1.x = 0f;
        //store1.z = 180f;
        //A1.localEulerAngles = store1;
        //Vector3 store2 = A2.localEulerAngles;
        //store2.y = 0f;
        //store2.z = 180f;
        //A2.localEulerAngles = store2;
        ////Vector3 store3 = A3.localEulerAngles;
        ////store3.y = 0f;
        ////store3.z = 0f;
        ////A3.localEulerAngles = store3;
        //Vector3 store4 = A4.localEulerAngles;
        //store4.x = 0f;
        //store4.y = 180f;
        //A4.localEulerAngles = store4;
        //Vector3 store5 = A5.localEulerAngles;
        //store5.y = 180f;
        //store5.z = 0f;
        //A5.localEulerAngles = store5;
        //Vector3 store6 = A6.localEulerAngles;
        //store6.x = 0f;
        //store6.y = 180f;
        //A6.localEulerAngles = store6;
    }
}
