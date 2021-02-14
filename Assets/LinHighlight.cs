using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LinHighlight : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnClick(Button btn)
    {
        btn.gameObject.transform.GetChild(0).gameObject.SetActive(!btn.gameObject.transform.GetChild(0).gameObject.activeSelf);
        //switch (btn.name)
        //{
        //    case "Lin1":
        //        print("yee");
                
        //        break;
        //    case "Lin2":
        //        btn.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(!btn.gameObject.transform.GetChild(0).GetChild(0).gameObject.activeSelf);
        //        break;
        //    case "Lin3":
        //        btn.gameObject.transform.GetChild(0).GetChild(0).gameObject.SetActive(!btn.gameObject.transform.GetChild(0).GetChild(0).gameObject.activeSelf);
        //        break;
        //}
    }
}
