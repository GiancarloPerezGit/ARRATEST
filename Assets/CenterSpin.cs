using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterSpin : MonoBehaviour
{
    public GameObject t;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameObject.GetComponent<MeshRenderer>().bounds.center);

    }

    // Update is called once per frame
    void Update()
    {
        t.transform.position = gameObject.GetComponent<MeshRenderer>().bounds.center;
    }
}
