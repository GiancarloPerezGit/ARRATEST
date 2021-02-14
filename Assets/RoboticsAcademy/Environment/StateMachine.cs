using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public bool[] flags;
    private GameObject[] states;
    public WayPoint PNPtoAI;
    // Start is called before the first frame update
    void Start()
    {
        states = GameObject.FindGameObjectsWithTag("State");
        flags = new bool[states.Length + 1];
        flags[0] = true;
        for(int i = 1; i < flags.Length; i++)
        {
            flags[i] = states[i - 1].GetComponent<StateChecker>().returnState();
            states[i - 1].GetComponent<StateChecker>().setOrder(i);
        }

    }
    public void updateFlag(int index, bool state)
    {
        flags[index] = state;
    }
    public bool changeState(int index)
    {
        if(flags[index - 1] != true)
        {
            return false;
        }
        else
        {
            flags[index - 1] = false;
            return true;
        }
    }
    public void PNPtoAID()
    {
        PNPtoAI.teleportToPoint();
    }
}
