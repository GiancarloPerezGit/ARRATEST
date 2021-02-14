using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfState : MonoBehaviour
{
    public bool state = false;
    // Start is called before the first frame update

    private void OnEnable()
    {
        state = true;   
    }
    private void OnDisable()
    {
        state = false;
    }

}
