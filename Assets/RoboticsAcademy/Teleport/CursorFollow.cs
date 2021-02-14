using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Physics;
using Microsoft.MixedReality.Toolkit.Utilities;

public class CursorFollow : MonoBehaviour
{
    public float latchRange = 0.2f;
    // Start is called before the first frame update
    GameObject pointer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectWithTag("TeleportHand") != null)
        {
            pointer = GameObject.FindGameObjectWithTag("TeleportHand");
            if (pointer.GetComponent<MixedRealityLineRenderer>().enabled)
            {
                int layMas = 1 << 9;
                Collider[] hitColliders = Physics.OverlapSphere(pointer.GetComponent<LineRenderer>().GetPosition(15), latchRange, layMas);
                if (hitColliders.Length > 0)
                {
                    if (hitColliders[0].gameObject.tag == "Teleport Circle")
                    {
                        gameObject.transform.position = hitColliders[0].gameObject.transform.position;
                    }

                }
                else
                {
                    Vector3 pos = pointer.GetComponent<LineRenderer>().GetPosition(15);
                    //pos.y = 0;
                    gameObject.transform.position = pos;
                }

            }
            else
            {
                gameObject.transform.position = new Vector3(0, -1, 0);
            }

        }
    }
}
