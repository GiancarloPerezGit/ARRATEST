using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamFollow : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    public float rotSpeed = 20f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.SmoothDamp(transform.position, Camera.main.transform.position, ref velocity, 0.3f);
        if (Mathf.Abs(transform.eulerAngles.x - Camera.main.transform.eulerAngles.x) > 1)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Camera.main.transform.rotation, rotSpeed * Time.deltaTime);
        }
        
        
    }
}
