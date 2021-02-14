using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targeting : MonoBehaviour
{
    public GameObject RightHand;
    public GameObject LeftHand;

    public GameObject TargetObj;
    public GameObject bas;

    //Vectors to determine where the controllers are in space
    private Vector3 LeftLoc = new Vector3(0, 0, 0);
    private Vector3 RightLoc = new Vector3(0, 0, 0);
    private Vector3[] RoboMoveArray = new Vector3[1];

    private bool StartPoint = true;
    private bool competeledLine = false;
    private int CurrNode = 0;

    //private TargetConverter TC;

    GameObject StarNode;
    // Start is called before the first frame update
    void Start()
    {
        //activate listeners on start

    }

    public void TriggerUp()
    {
        //created for future proofing for later
        Debug.Log("Trigger testing: up");
    }

    public void TriggerDown()
    {
        //main method for when Trigger is pressed down
        Debug.Log("Trigger testing: down");

        // if the target that is placed is a starting point, place first target node.
        if (competeledLine == false)
        {
            if (StartPoint == true)
            {
                var NewNode = (GameObject)Instantiate(TargetObj, RightLoc, Quaternion.identity);
                StarNode = NewNode;
                StarNode.name = "node" + CurrNode;
                StarNode.transform.parent = bas.transform;
                StartPoint = false;

                //fill the roboMoveArray with first point
                RoboMoveArray[CurrNode] = RightLoc;
                Debug.Log("start Point created");

            }
            //if a starting point has already been placed, place the ending node.
            else if (StartPoint == false)
            {
                var NewNode = (GameObject)Instantiate(TargetObj, RightLoc, Quaternion.identity);
                NewNode.name = "node" + CurrNode;
                NewNode.transform.parent = bas.transform;

                //fill the RoboMoveArray with second point
                RoboMoveArray[CurrNode] = RightLoc;
                Debug.Log("end point created");

                //set the line is completed
                competeledLine = true;
                //TC.convert(StarNode.transform, NewNode.transform);  
            }
        }
        else
        {
            Debug.Log("Error: please delete or confirm current target line");
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        LeftLoc = LeftHand.transform.position;
        RightLoc = RightHand.transform.position;

    }


}

