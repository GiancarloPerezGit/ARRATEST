using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;
//using Leap.Unity;
public class pStop : MonoBehaviour
{
    private bool casting = false;
    private ButtonManagerScript bhs;
    // Start is called before the first frame update
    void Awake()
    {
        bhs = GameObject.FindGameObjectWithTag("BMS").GetComponent<ButtonManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void stopOff()
    {
        bhs.pStop = true;
    }
    public void stopOn()
    {
        bhs.pStop = false;
    }
}
