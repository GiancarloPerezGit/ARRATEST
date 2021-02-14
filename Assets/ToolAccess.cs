using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolAccess : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject toolAc;

    public void swap()
    {
        toolAc.SetActive(!toolAc.activeSelf);
    }
}
