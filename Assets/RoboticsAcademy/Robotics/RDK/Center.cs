using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    void Start()
    {
        camera.transform.position = transform.position;
        camera.transform.rotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
