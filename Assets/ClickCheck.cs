using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickCheck : MonoBehaviour
{
    public bool targetClicked = false;

    public void OnClick()
    {
        targetClicked = true;
        Debug.Log("sphere Clicked");
    }

    public void OnEnter()
    {
        Debug.Log("touched sphere");
    }
}
