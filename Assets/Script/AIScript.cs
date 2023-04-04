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

    public float maxAngle = 45;
    public float maxDistance = 10;
    public float timer = 0.0f;
    public float visionCheckRate = 1.0f;
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
        timer += Time.deltaTime;
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
        {
            
            if (state != State.LookingAround && !IsInvoking("GotoNextPoint") && timer­>3.0f)
            {
                state = State.LookingAround;
                Debug.Log("Invoke");
                agent.isStopped = true;
                Invoke("GotoNextPoint", 5.0f);
            }
        }
    }

    void UpdateAnimator()
    {
        if(agent.velocity.magnitude< 1) 
        {
            animator.SetFloat("CharacterSpeed",agent.velocity.magnitude);
        }
        else 
        {
            animator.SetFloat("CharacterSpeed",1.0f);
        }
        animator.SetBool("IsLookingAround",state == State.LookingAround);
    }
    void GotoNextPoint() {
        state = State.Patrol;
        agent.isStopped = false;
        timer = 0.0f;
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
