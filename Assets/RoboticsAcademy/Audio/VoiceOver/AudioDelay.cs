using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioDelay : MonoBehaviour
{
    // Start is called before the first frame update
    AudioSource myAudio;
    void Start()
    {
        myAudio = GetComponent<AudioSource>();
        myAudio.PlayDelayed(7.0f);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
