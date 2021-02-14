using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartTick : MonoBehaviour
{
    // Start is called before the first frame update
    private bool off = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!off)
        {
            gameObject.SetActive(false);
            off = true;
        }
        
    }
}
