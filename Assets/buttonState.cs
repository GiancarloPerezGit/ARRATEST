using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class buttonState : MonoBehaviour
{
    private GameObject uiMan;
    void Start()
    {
        uiMan = GameObject.FindGameObjectWithTag("UIManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(uiMan.GetComponent<ProjectRoboDK.CodeWindow>().buttonOn)
        {
            transform.gameObject.GetComponent<Button>().enabled = true;
        }
        else
        {
            transform.gameObject.GetComponent<Button>().enabled = false;
        }
    }
}
