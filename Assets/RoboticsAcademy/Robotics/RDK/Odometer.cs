using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Odometer : MonoBehaviour
{
    // Start is called before the first frame update
    public float angChange = 0;
    public float prevAng = 90f;
    public float currAng = 0f;
    // Update is called once per frame
    void Update()
    {
        currAng = transform.localEulerAngles.x;
        print(currAng);
        angChange += currAng - prevAng;
        prevAng = currAng;
        print(prevAng);
    }
}
