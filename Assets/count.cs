using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class count : MonoBehaviour
{
    public int counta;
    // Start is called before the first frame update
    
    void Awake()
    {
        counta = GameObject.FindGameObjectWithTag("UIManager").GetComponent<ProjectRoboDK.CodeWindow>().index;
    }

    private void OnEnable()
    {
        GameObject[] hL = GameObject.FindGameObjectsWithTag("Highlight");
        for(int i = 0; i < hL.Length; i++)
        {
            if (hL[i] == gameObject)
            {

            }
            else
            {
                hL[i].SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
