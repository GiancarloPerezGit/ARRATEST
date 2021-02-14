using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IOSwap : MonoBehaviour
{
    public GameObject popup;
    private ButtonManagerScript bhs;
    // Start is called before the first frame update
    void Start()
    {
        bhs = GameObject.FindGameObjectWithTag("BMS").GetComponent<ButtonManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!bhs.eStop && bhs.pStop && bhs.modeState == 0)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        else if(!bhs.eStop && bhs.pStop && bhs.modeState == 1)
        {
            if(!popup.activeSelf && !transform.GetChild(0).gameObject.activeSelf)
            {
                popup.SetActive(true);
            }
            
        }
        else
        {
            //popup.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
    }
    public void setOn()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(false);
        popup.SetActive(false);
        
    }
    
}
