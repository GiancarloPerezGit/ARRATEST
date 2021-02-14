using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class TeleText : MonoBehaviour
{
    private float timer = 4f;
    public TMP_InputField telText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            telText.text = "Move towards the red circle using the \n teleport button";
        }
    }
}
