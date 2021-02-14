using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class LessonScriptPNP : MonoBehaviour
{
    private int currentBox = 0;
    private TextMeshPro tmpText;

    public GameObject tmpBox;

    public string[] data;

    // Start is called before the first frame update
    void Start()
    {
        //put dialogue script here
        data = new string[]
        {
            "Welcome to the Pick and place lesson.",
            "hmm, it seems like your having difficulty with this lesson.",
            "lets go back to the Axis intergration lesson you learned last week"

        };


        tmpText = tmpBox.GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        tmpText.text = data[currentBox];
        //Debug.Log(currentBox);
    }

    // iterates the text in the box foward
    public void OnClickFoward()
    {
        if (currentBox < data.Length - 1)
        {
            currentBox++;
        }

    }

    // iterates the text in the box backwards
    public void OnClickBackward()
    {
        if (currentBox > 0)
        {
            currentBox--;
        }

    }
}
