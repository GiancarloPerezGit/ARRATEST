using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerScript : MonoBehaviour
{   
    public bool clearTimer = false;
    public bool timerOn = false;

    //current H:M:S meant to be displayed
    public float currentSeconds;
    public float currentMinutes;
    public float currentHours;

    // UI timer
    public TMPro.TextMeshProUGUI timer;

    //NOTE: used FixedUpdate to decouple the timer from the processor, these things can ultimately be
    //moved to a co-routine if need be
    private void FixedUpdate()
    {
        if (clearTimer == true)
        {
            clearCurrentTime();
            clearTimer = false;
        }

        if (timerOn == true)
        {
            currentSeconds += Time.deltaTime;
            if (currentSeconds > 59.0f)
            {
                currentMinutes++;
                currentSeconds = 0.0f;
            }

            if (currentMinutes > 59.0f)
            {
                currentHours++;
                currentMinutes = 0.0f;
            }

            timer.text = currentHours.ToString("00") + ":" + currentMinutes.ToString("00") + ":" + currentSeconds.ToString("0#.");
        }

    }

    //setters for different functions in the timer
    public void clearCurrentTime()
    {
        currentSeconds = 0.0f;
        currentMinutes = 0.0f;
        currentHours = 0.0f;
    }

    //NOTE: when we have SQL intergration set up, we can basically use these methods to push/pull 
    //time data from the server. until then it just stores into a 2D float array.

    //turns out save/load wont be needed so I saved them for later
    public void saveLessonTime()
    {
    }
 
    public void LoadLessonTime()
    {
    }
}
