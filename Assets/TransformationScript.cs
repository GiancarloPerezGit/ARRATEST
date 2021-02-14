using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TransformationScript : MonoBehaviour
{
    private GameObject node;
    private Transform nodeTransform;
    
    public Vector3 targetPositions;

    public TextMeshPro posX;
    public TextMeshPro posY;
    public TextMeshPro posZ;

    // Start is called before the first frame update
    void Start()
    {
        node = gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        nodeTransform = node.transform;
        targetPositions = nodeTransform.position;

        posX.text = "pos X ( " + targetPositions.x + " )";
        posY.text = "pos y ( " + targetPositions.y + " )";
        posZ.text = "pos z ( " + targetPositions.z + " )";
    }
}
