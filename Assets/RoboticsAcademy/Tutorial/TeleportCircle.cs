using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportCircle : MonoBehaviour
{
    public float searchRadius;
    private Transform player;
    // Start is called before the first frame update
    void Start()
    {
        player = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(player.position, this.transform.position) < searchRadius)
        {
            gameObject.SetActive(false);
        }
    }
}
