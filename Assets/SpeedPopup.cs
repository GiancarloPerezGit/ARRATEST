using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SpeedPopup : MonoBehaviour
{
    public GameObject speedPopup;
    public TextMeshProUGUI progTex;
    public TextMeshProUGUI jogTex;
    public int progSp = 50;
    public int jogSp = 50;
    public AxisMode am;
    public ProjectRoboDK.KR10IKW tool;
    private bool mP = false;
    private bool pP = false;
    private bool mJ = false;
    private bool pJ = false;
    private bool isRunning = false;
    public GameObject[] axesSpeeds;
    // Start is called before the first frame update
    void Start()
    {
        am.jogSpeed = 50;
        tool.jogSpeed = 50;
        //axesSpeeds = GameObject.FindGameObjectsWithTag("AxSpeed");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isRunning)
        {
            if (mP)
            {
                if (progSp < 100)
                {
                    StartCoroutine(progChange(-1));
                }


            }
            else if (pP)
            {
                if (progSp > 0)
                {
                    StartCoroutine(progChange(1));
                }

            }
            else if (mJ)
            {
                if(jogSp > 0)
                {
                    StartCoroutine(jogChange(-1));
                }
                

            }
            else if (pJ)
            {
                if (jogSp < 100)
                {
                    StartCoroutine(jogChange(1));
                }

            }
        }
        
    }
    public void OnClick(Button btn)
    {
        switch (btn.name)
        {
            case "BtnJogSpeed":
                speedPopup.SetActive(!speedPopup.activeSelf);
                break;

        }
    }
    public void OnPress(Button btn)
    {
        switch (btn.name)
        {
            case "MinusProg":
                mP = true;
                break;
            case "PlusProg":
                pP = true;
                break;
            case "MinusJog":
                mJ = true;
                break;
            case "PlusJog":
                pJ = true;
                break;
        }
    }
    public void OnRelease()
    {
        mP = false;
        pP = false;
        mJ = false;
        pJ = false;
        axesSpeeds[0].transform.localRotation = Quaternion.Euler(0, jogSp / 100f, 0);
        axesSpeeds[1].transform.localRotation = Quaternion.Euler(0, jogSp / -100f, 0);
        axesSpeeds[2].transform.localRotation = Quaternion.Euler(jogSp / 100f, 0, 0);
        axesSpeeds[3].transform.localRotation = Quaternion.Euler(jogSp / -100f, 0, 0);
        axesSpeeds[4].transform.localRotation = Quaternion.Euler(jogSp / 100f, 0, 0);
        axesSpeeds[5].transform.localRotation = Quaternion.Euler(jogSp / -100f, 0, 0);
        axesSpeeds[6].transform.localRotation = Quaternion.Euler(0, 0, jogSp / 100f);
        axesSpeeds[7].transform.localRotation = Quaternion.Euler(0, 0, jogSp / -100f);
        axesSpeeds[8].transform.localRotation = Quaternion.Euler(jogSp / 100f, 0, 0);
        axesSpeeds[9].transform.localRotation = Quaternion.Euler(jogSp / -100f, 0, 0);
        axesSpeeds[10].transform.localRotation = Quaternion.Euler(0, 0, jogSp / 100f);
        axesSpeeds[11].transform.localRotation = Quaternion.Euler(0, 0, jogSp / -100f);
        tool.jogSpeed = jogSp;
    }

    IEnumerator progChange(int i)
    {
        isRunning = true;
        progSp += i * 10;
        string g = progSp.ToString() + "%";
        progTex.text = g;
        yield return new WaitForSeconds(0.25f);
        isRunning = false;
    }
    IEnumerator jogChange(int i)
    {
        isRunning = true;
        jogSp += i * 10;
        string g = jogSp.ToString() + "%";
        jogTex.text = g;
        yield return new WaitForSeconds(0.25f);
        isRunning = false;
    }
}
