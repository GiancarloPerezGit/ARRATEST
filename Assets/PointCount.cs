using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointCount : MonoBehaviour
{
    private int pointC = -1;
    public int preview = 0;
    public int after = 1;
    //public bool change = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //GameObject[] movs = GameObject.FindGameObjectsWithTag("MovementType");
        //pointC = -1;
        //for(int i = 0; i < movs.Length - 1; i++)
        //{
        //    if(movs[i].name == "PTP" || movs[i].name == "LIN")
        //    {
        //        pointC += 1;
        //    }
        //    else
        //    {
        //        pointC += 2;
        //    }
        //}
    }

    public void pointUpdate(bool change)
    {
        if(!change)
        {
            GameObject[] movs = GameObject.FindGameObjectsWithTag("MovementType");
            pointC = 0;
            for (int i = 0; i < movs.Length - 1; i++)
            {
                if (movs[i].name == "PTP" || movs[i].name == "LIN")
                {
                    pointC += 1;
                }
                else
                {
                    pointC += 2;
                }
            }
            preview = pointC + 1;
            after = pointC + 2;
        }
        else
        {
            GameObject[] movs = GameObject.FindGameObjectsWithTag("MovementType");
            pointC = 0;
            for (int i = 0; i < movs.Length; i++)
            {
                if (movs[i].name == "PTP" || movs[i].name == "LIN")
                {
                    pointC += 1;
                }
                else
                {
                    pointC += 2;
                }
            }
            preview = -1000;
            after = pointC + 1;
        }
    }
}
