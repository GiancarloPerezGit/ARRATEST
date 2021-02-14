using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportTurnAround : MonoBehaviour
{
    public GameObject playSpace;
    public float yRotation;

    private bool alreadyTurned = false;


    // Update is called once per frame
    void Update()
    {         
        if(this.GetComponent<TargetCheck>().targetTeleported == true)
        {
            if(alreadyTurned == false)
            {
                playSpace.transform.Rotate(0.0f, yRotation, 0.0f);
                alreadyTurned = true;
            }

        }
    }
}
