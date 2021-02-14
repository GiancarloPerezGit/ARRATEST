using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suicideTimer : MonoBehaviour
{
    // Start is called before the first frame update\
    public float time = 0.0f;
    bool tim = false;
    void Start()
    {
        
    }

    public void resetter()
    {
        tim = true;
    }

    public void resumer()
    {
        tim = false;
    }

    private void OnEnable()
    {
        time = 0.0f;
    }

    void Update()
    {
        if(tim)
        {
            time = 0.0f;
        }
        else
        {
            time += Time.deltaTime;
        }
        
        if(time > 6.0f)
        {
            gameObject.SetActive(false);
        }
    }
}
