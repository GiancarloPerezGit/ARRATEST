using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ModeSwitch : MonoBehaviour
{
    public GameObject modeWindow;
    public GameObject codeWindow;
    public int state = 0;
    public GameObject modeSwitch;
    public ButtonManagerScript bhs;
    public GameObject t1;
    public GameObject t2;
    public GameObject aut;

    //private bool popStat = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        bhs = GameObject.FindGameObjectWithTag("BMS").GetComponent<ButtonManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnClick(Button btn)
    {
        switch (btn.name)
        {
            case "BtnModeT1":
                modeWindow.SetActive(true);
                codeWindow.SetActive(false);
                bhs.eStop = true;
                break;
            case "BtnModeT2":
                modeWindow.SetActive(true);
                codeWindow.SetActive(false);
                bhs.eStop = true;
                break;
            case "BtnModeAuto":
                modeWindow.SetActive(true);
                codeWindow.SetActive(false);
                bhs.eStop = true;
                break;
            case "Confirm":
                modeWindow.SetActive(false);
                codeWindow.SetActive(true);
                int i = modeSwitch.transform.childCount;
                t1.SetActive(false);
                t2.SetActive(false);
                aut.SetActive(false);
                switch (bhs.modeState)
                {
                    case 0:
                        t1.SetActive(true);
                        break;
                    case 1:
                        t2.SetActive(true);
                        break;
                    case 2:
                        aut.SetActive(true);
                        break;
                }
                bhs.eStop = false;
                break;
            

        }
    }
}
