using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;
public class StartOff : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<BaseMixedRealityLineRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
