using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.ComponentModel;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
public class stateSwap : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite run;
    public Sprite stop;
    public Sprite norm;
    public Sprite ready;
    public Sprite fin;
    void Start()
    {
        
    }

    public void swapRun()
    {
        gameObject.GetComponent<Image>().sprite = run;
    }

    public void swapStop()
    {
        gameObject.GetComponent<Image>().sprite = stop;
    }

    public void swapNorm()
    {
        gameObject.GetComponent<Image>().sprite = norm;
    }

    public void swapReady()
    {
        gameObject.GetComponent<Image>().sprite = ready;
    }

    public void swapFin()
    {
        gameObject.GetComponent<Image>().sprite = fin;
    }
    void Update()
    {
        
    }
}
