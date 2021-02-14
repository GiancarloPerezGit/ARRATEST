using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faceObject : MonoBehaviour
{
    GameObject flange;
    // Start is called before the first frame update
    void Start()
    {
        flange = GameObject.FindGameObjectWithTag("Flange");
        
    }

    // Update is called once per frame
    void Update()
    {
        //print(flange.transform.rotation.eulerAngles.y);
        Vector3 lookPos = (flange.transform.position - transform.position) * -1f;
        transform.localRotation = Quaternion.LookRotation(lookPos);
    }
}
