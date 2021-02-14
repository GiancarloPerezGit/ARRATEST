using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

public class Female01Movement : MonoBehaviour
{
    public GameObject destination;
    public NavMeshAgent agent;
    public ThirdPersonCharacter character;
    public Animator animator;

    // Update is called once per frame

    void Update()
    {
        Debug.Log(agent.remainingDistance);

        agent.SetDestination(destination.transform.position);

        if (agent.remainingDistance > agent.stoppingDistance)
        {
            character.Move(agent.desiredVelocity, false, false);
        }
        else
        {
            character.Move(Vector3.zero, false, false);
        }

        if (agent.remainingDistance < 0.15)
        {
            animator.SetBool("IsWalking", true);
        }
    }
}
