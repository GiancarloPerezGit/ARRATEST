using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCheck : MonoBehaviour
{
    public bool targetTeleported;

    private Vector2 mainCamera;
    private Vector2 currentPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Vector2.Distance(mainCamera, currentPos));
        mainCamera = new Vector2(Camera.main.transform.position.x, Camera.main.transform.position.z) ;
        currentPos = new Vector2(transform.position.x, transform.position.z);
        if (Vector2.Distance(mainCamera, currentPos) < 1.0f) 
        {
            targetTeleported = true;
        }
        else
        {
            targetTeleported = false;
        }
    }
}
