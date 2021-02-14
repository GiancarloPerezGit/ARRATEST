using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WarningPopupScript : MonoBehaviour
{
    public GameObject window;

    public void OnClick(Button btn)
    {
        switch (btn.name)
        {
            //currently both of these will just close the window
            case "btnYes":
                window.SetActive(false);
                break;

            case "btnNo":
                window.SetActive(false);
                break;
        }
    }
}
