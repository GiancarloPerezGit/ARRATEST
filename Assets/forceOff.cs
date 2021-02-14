using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class forceOff : MonoBehaviour
{
    public ButtonManagerScript bhs;
    // Start is called before the first frame update
    void Start()
    {
        bhs = GameObject.FindGameObjectWithTag("BMS").GetComponent<ButtonManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        bhs.eStop = true;
    }
}
