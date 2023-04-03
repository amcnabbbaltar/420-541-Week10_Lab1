using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
public class AIScript : MonoBehaviour
{

    public float maxAngle = 45;
    public float maxDistance = 10;
    public float timer = 1.0f;
    public float visionCheckRate = 1.0f;
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
            GotoNextPoint();
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
    }
    void GotoNextPoint() {
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
