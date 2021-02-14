using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class highlightSwap : MonoBehaviour
{
    public GameObject h1;
    public GameObject h2;

    void Start()
    {
        
    }

    void OnEnable()
    {
        h1.SetActive(false);
        h2.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
