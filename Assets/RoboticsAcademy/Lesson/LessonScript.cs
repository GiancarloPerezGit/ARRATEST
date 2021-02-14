using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LessonScript : MonoBehaviour
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
            "Welcome back the Immersive Learning lab",
            "Please wait while I upload your lesson",
            "Industrial Robotic Arms have several rotating joints known as axes that can be jogged (or moved) independently or in concert with one another. ",
            "Axes can be controlled using a touch screen pendant. Please select <color=green>WORLD</color> coordinate system on the interface.",
            "Each axis is conveniently numbered: \n \n * Axis 1 rotates around the base of the robotic arm. \n * Axis 2 rotates the arm forward and backward. \n * Axis 3 rotates the arm up and down. \n * Axis 4 rotates the forearm around its own axis. \n * Axis 5 rotates the wrist back and forth \n * Axis 6 rotates the flange",
            "Practice moving the robot using the axis control buttons.",
            "Look to your left. Try to match the orientation of the example using the axis control keys.",
            "Now try to point the robotic arm straight up in the air.",
            "First, make the blue arrow face upwards, then the <color=green>green</color>, and then the <color=red>red</color> arrow.",
            "Once you feel comfortable moving the robotic arm you may proceed to the next lesson."

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
