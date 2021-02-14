using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Starting : MonoBehaviour
{
    private bool start = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            GameObject.FindGameObjectWithTag("PlayerCam").transform.position = new Vector3(gameObject.transform.position.x, 0, gameObject.transform.position.z);
            GameObject.FindGameObjectWithTag("PlayerCam").transform.rotation = gameObject.transform.rotation;
            
            start = false;
        }
        
    }
}
