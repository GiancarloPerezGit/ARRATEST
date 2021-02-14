using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoCursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.GetChild(1).GetChild(0).GetChild(0).gameObject.SetActive(false);
    }
}
