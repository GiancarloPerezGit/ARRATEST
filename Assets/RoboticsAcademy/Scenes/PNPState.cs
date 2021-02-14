using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PNPState : MonoBehaviour, StateChecker
{
    private GameObject PNP;
    private bool start = false;
    public float time = 3;
    private int order;
    private StateMachine stateMachine;
    private GameObject lightWall;
    // Start is called before the first frame update
    void Start()
    {
        PNP = GameObject.Find("PickNPlace");
        PNP.SetActive(false);
        stateMachine = GameObject.FindGameObjectWithTag("SM").GetComponent<StateMachine>();
    }
    public bool returnState()
    {
        return start;
    }
    public void execute()
    {
        if(stateMachine.changeState(order))
        {
            start = true;
            PNP.SetActive(true);
            stateMachine.updateFlag(order, true);
        }

    }
    public void setOrder(int ord)
    {
        order = ord;
    }
    public void exitState()
    {
        stateMachine.PNPtoAID();
    }
    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            time -= Time.deltaTime;
            if(time <= 0)
            {
                start = false;
                PNP.SetActive(false);
                exitState();
            }
        }
    }
}
