using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNPCircle : MonoBehaviour
{
    private GameObject player;
    private GameObject UI;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("MainCamera");
        UI = GameObject.FindGameObjectWithTag("PNPUI");
    }

    // Update is called once per frame
    void Update()
    {
        //if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(gameObject.transform.position.x, gameObject.transform.position.z)) < 1)
        //{
        //    UI.SetActive(true);
        //    UI.transform.position = gameObject.transform.GetChild(0).transform.position;
        //    UI.transform.rotation = gameObject.transform.GetChild(0).transform.rotation;

        //}
    }
}
