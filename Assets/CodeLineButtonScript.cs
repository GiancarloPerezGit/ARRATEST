using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CodeLineButtonScript : MonoBehaviour
{
    public GameObject codeLine;
    public GameObject warningPopUp;
    public GameObject codeParent;

    private GameObject[] circObjects;

    public TextMeshProUGUI velValue;

    public TextMeshProUGUI pointStart;
    public TextMeshProUGUI pointEnd;

    public List<Transform> lineList;

    public pointList pl;

    public bool movTypePTP;
    public bool movTypeLIN;
    public bool movTypeCIRC;

    public bool end = false;
    public int velocity;
    private static int currentPoint = 0;
    private bool circPrev = false;
    private string ending;
    public int added;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(movTypePTP || movTypeCIRC)
        {
            ending = "%";
            added = velocity + 100;
        }
        else
        {
            ending = "m/s";
            added = velocity + 0;
        }
        velValue.text = (added).ToString() + ending;
        //pointStart.text = "p" + (currentPoint - 1).ToString();
        //pointEnd.text = "p" + currentPoint.ToString();
    }

    private void OnEnable()
    {
        movTypePTP = true;

        codeLine.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
        codeLine.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
        codeLine.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
        //pointStart.text = "P" + codeParent.GetComponent<PointCount>().preview;
        codeParent.GetComponent<PointCount>().pointUpdate(false);
        if (codeParent.GetComponent<PointCount>().preview == -1000)
        {
            //pointEnd.text = "P" + codeParent.GetComponent<PointCount>().after;
        }
        else
        {
            pointStart.text = "P" + (codeParent.GetComponent<PointCount>().preview);
            //pointEnd.text = "P" + codeParent.GetComponent<PointCount>().after;
        }

        codeLine.transform.GetChild(3).gameObject.SetActive(false);
        codeLine.transform.GetChild(7).gameObject.SetActive(false);
    }

    public void OnClick(Button btn)
    {
        switch (btn.name)
        {
            case "PTP":
                codeLine.transform.GetChild(5).gameObject.SetActive(!codeLine.transform.GetChild(5).gameObject.activeSelf);
                
                break;

            case "LIN":
                codeLine.transform.GetChild(5).gameObject.SetActive(!codeLine.transform.GetChild(5).gameObject.activeSelf);
                
                
                break;

            case "CIRC":
                codeLine.transform.GetChild(5).gameObject.SetActive(!codeLine.transform.GetChild(5).gameObject.activeSelf);
                
                break;
            case "velocity":
                codeLine.transform.GetChild(6).gameObject.SetActive(!codeLine.transform.GetChild(6).gameObject.activeSelf);
                break;
            case "pointStart":
                codeLine.transform.parent.parent.GetChild(2).gameObject.SetActive(!codeLine.transform.parent.parent.GetChild(2).gameObject.activeSelf);
                end = false;
                break;
            case "pointEnd":
                codeLine.transform.parent.parent.GetChild(2).gameObject.SetActive(!codeLine.transform.parent.parent.GetChild(2).gameObject.activeSelf);
                end = true;
                break;
            //menu choices. but changes here. 

            case "MenuPTP":
                movTypePTP = true;
                movTypeLIN = false;
                movTypeCIRC = false;
                velocity = 0;
                codeLine.transform.GetChild(1).GetChild(0).gameObject.SetActive(true);
                codeLine.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                codeLine.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                //pointStart.text = "P" + codeParent.GetComponent<PointCount>().preview;
                codeParent.GetComponent<PointCount>().pointUpdate(false);
                if (codeParent.GetComponent<PointCount>().preview == -1000)
                {
                    //pointEnd.text = "P" + codeParent.GetComponent<PointCount>().after;
                }
                else
                {
                    pointStart.text = "P" + codeParent.GetComponent<PointCount>().preview;
                    //pointEnd.text = "P" + codeParent.GetComponent<PointCount>().after;
                }
                if (circPrev == true)
                {
                    circPrev = false;
                    pl.removePoint();
                }
                codeLine.transform.GetChild(3).gameObject.SetActive(false);
                codeLine.transform.GetChild(7).gameObject.SetActive(false);
                break;

            case "MenuLIN":
                movTypePTP = false;
                movTypeLIN = true;
                movTypeCIRC = false;
                velocity = 0;
                codeLine.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                codeLine.transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                codeLine.transform.GetChild(1).GetChild(2).gameObject.SetActive(false);
                pointStart.text = "P" + codeParent.GetComponent<PointCount>().preview;

                if (codeParent.GetComponent<PointCount>().preview == -1000)
                {
                    //pointEnd.text = "P" + codeParent.GetComponent<PointCount>().after;
                }
                else
                {
                    pointStart.text = "P" + codeParent.GetComponent<PointCount>().preview;
                    //pointEnd.text = "P" + codeParent.GetComponent<PointCount>().after;
                }
                if (circPrev == true)
                {
                    circPrev = false;
                    pl.removePoint();
                }
                codeLine.transform.GetChild(3).gameObject.SetActive(false);
                codeLine.transform.GetChild(7).gameObject.SetActive(false);
                break;

            case "MenuCIRC":
                movTypePTP = false;
                movTypeLIN = false;
                movTypeCIRC = true;
                velocity = 0;
                codeLine.transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                codeLine.transform.GetChild(1).GetChild(1).gameObject.SetActive(false);
                codeLine.transform.GetChild(1).GetChild(2).gameObject.SetActive(true);

                if(codeParent.GetComponent<PointCount>().preview == -1000)
                {
                    pointEnd.text = "P" + codeParent.GetComponent<PointCount>().after;
                }
                else
                {
                    pointStart.text = "P" + codeParent.GetComponent<PointCount>().preview;
                    pointEnd.text = "P" + codeParent.GetComponent<PointCount>().after;
                }

                if (circPrev == true)
                {

                }
                else
                {
                    circPrev = true;
                    pl.addPoint();
                }

                codeLine.transform.GetChild(3).gameObject.SetActive(true);
                codeLine.transform.GetChild(7).gameObject.SetActive(true);
                break;

            //velocity. increases and decreases on button press
            //case "velocity":
            //    codeLine.transform.GetChild(6).gameObject.SetActive(true);
            //    break;

            case "BtnVelPlus":
                velocity++;
                break;

            case "BtnVelMinus":
                velocity--;
                break;

            case "BtnClose":
                codeLine.transform.GetChild(6).gameObject.SetActive(false);
                break;

            //Window for creating Circs movements
            case "circEnd":

                break;

            case "circAux":
                codeLine.transform.GetChild(3).gameObject.SetActive(true);
                break;

            case "circClose":
                codeLine.transform.GetChild(7).gameObject.SetActive(false);
                break;

        }
    }
    //private int PointCount()
    //{
    //    foreach (Transform c in codeParent.GetComponentInChildren<Transform>())
    //    {
    //        currentPoint++;
    //    }

    //    circObjects = GameObject.FindGameObjectsWithTag("CIRC tag");
    //    return currentPoint = currentPoint + circObjects.Length - 1;
    //}
}
