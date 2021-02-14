using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManagerScript : MonoBehaviour
{
    public Color RobotColor;

    public Material RoboMaterial;

    // Start is called before the first frame update
    void Start()
    {
        RobotColor.r = 1f;
        RobotColor.g = 0.50f;
        RobotColor.b = 0f;
    }

    // Update is called once per frame
    void Update()
    {

        RoboMaterial.color = RobotColor;

    }
}
