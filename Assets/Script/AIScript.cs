using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
enum State {
    Patrol,
    LookingAround
}
public class AIScript : MonoBehaviour
{
    State state = State.Patrol;
    public Transform[] points;
    public Animator animator;
    private int destPoint = 0;

    NavMeshAgent agent; 
    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //animator = GetComponent<Animator>();
        agent.autoBraking = false;
    }

    
    // Update is called once per frame
    void Update()
    { 
        UpdateAnimator();
        
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            
            if (state != State.LookingAround && !IsInvoking("GotoNextPoint"))
            {
                state = State.LookingAround;
                agent.isStopped = true;

                Invoke("GotoNextPoint", 5.0f);
            }
        }
    }

    void UpdateAnimator()
    {

        animator.SetFloat("CharacterSpeed",agent.velocity.magnitude);
        animator.SetBool("IsLookingAround",state == State.LookingAround);
    }
    void GotoNextPoint() {

        state = State.Patrol;
        agent.isStopped = false;
        
        // Returns if no points have been set up
        if (points.Length == 0)
            return;

        // Set the agent to go to the currently selected destination.
        agent.destination = points[destPoint].position;

        // Choose the next point in the array as the destination,
        // cycling to the start if necessary.
        destPoint = (destPoint + 1) % points.Length;
    }

}
