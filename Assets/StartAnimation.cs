using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimation : MonoBehaviour
{
    private bool startAnimation;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {

    if(startAnimation == true)
        {
            gameObject.transform.GetComponent<animationPlayer>().Run();
            startAnimation = false;
        }   
    }
    private void Awake()
    {

    }
}
