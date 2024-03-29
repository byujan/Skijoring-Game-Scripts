using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(UnityEngine.AI.NavMeshAgent))] //,typeof(VelocityReporter))]
public class FoxAI : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    private UnityEngine.AI.NavMeshAgent navMeshAgent;
    //public VelocityReporter movingAgent;
    public GameObject[] waypoints;
    //public GameObject tracker;
    private int currWaypoint = -1;
    private bool isColliding;
    public CanvasGroup canvasGroup;

    public enum AIState {
        StaticWP,
        DynamicWP
    };

    public AIState aiState;
    void Start()
    {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        //movingAgent = GetComponent<VelocityReporter>();
        aiState = AIState.StaticWP;
        //predictionWayPoint();
        setNextWayPoint();
        //navMeshAgent.speed = 15f;
    }

    private void Awake()
    {
        //canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnTriggerEnter(Collider col) {
        isColliding = true;
    }

    private void setNextWayPoint() {
        int waypointLength = waypoints.Length;
        if (waypointLength == 0) {
            Debug.Log("Waypoint Length is Zero!");
        }
        currWaypoint++;

        if (currWaypoint >= waypointLength) {
            currWaypoint = 0;
            anim.SetFloat("Run", 0);
        }
        Debug.Log(currWaypoint);
        navMeshAgent.SetDestination(waypoints[currWaypoint].transform.position);
        //tracker.transform.position = waypoints[currWaypoint].transform.position;
    }

    // private void predictionWayPoint() {
    //     float dist = Vector3.Distance(movingAgent.transform.position, navMeshAgent.transform.position);
    //     float lookAheadT = dist/navMeshAgent.speed;
    //     //Debug.Log(lookAheadT);
    //     float clampedAhead = Mathf.Clamp(lookAheadT, 0, 6f);
    //     Vector3 futureTarget = movingAgent.transform.position + clampedAhead * movingAgent.velocity;
    //     UnityEngine.AI.NavMeshHit hit;
    //     if (navMeshAgent.Raycast(futureTarget, out hit)) {
    //         clampedAhead = Mathf.Clamp(lookAheadT, 0, 3f);
    //         futureTarget = movingAgent.transform.position + clampedAhead * movingAgent.velocity;
    //     }
    //     futureTarget.z = Mathf.Clamp(futureTarget.z, -13.97f, 11.026f);
    //     UnityEngine.AI.NavMeshPath path = new UnityEngine.AI.NavMeshPath();
    //     navMeshAgent.CalculatePath(futureTarget, path);
    //     navMeshAgent.SetPath(path);
    //     navMeshAgent.SetDestination(futureTarget);
    //     //navMeshAgent.SetDestination(movingAgent.transform.position);
    //     tracker.transform.position = futureTarget;
    //     //Debug.Log(futureTarget);
    // }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(Input.GetAxis("Vertical"));
        navMeshAgent.speed = navMeshAgent.speed + (Input.GetAxis("Vertical"));
        // if (navMeshAgent.remainingDistance < 1 && !navMeshAgent.pathPending) {
        //     setNextWayPoint();
        // }
        //Debug.Log(movingAgent.velocity);
        //Debug.Log(movingAgent.transform.position);
        // anim.SetFloat("vely", navMeshAgent.velocity.magnitude/navMeshAgent.speed);
        switch (aiState) {
            case AIState.StaticWP:
                // if (currWaypoint == waypoints.Length-1) {
                //     aiState = AIState.DynamicWP;
                //     //Debug.Log("moving to prediction");
                //     break;
                // }
                if (navMeshAgent.remainingDistance - navMeshAgent.stoppingDistance < 1 && !navMeshAgent.pathPending) {
                    //setNextWayPoint();
                    canvasGroup.interactable = true; 
                    canvasGroup.blocksRaycasts = true; 
                    canvasGroup.alpha = 1f; 
                    Time.timeScale = 0f;
                    
                }
                anim.SetFloat("Run", 5f);
                break;

            // case AIState.DynamicWP:
            //     //if (navMeshAgent.remainingDistance - navMeshAgent.stoppingDistance < 1 && !navMeshAgent.pathPending) {
            //     if (isColliding) {    
            //         setNextWayPoint();
            //         currWaypoint = -1;
            //         isColliding = false;
            //         aiState = AIState.StaticWP;
            //         Debug.Log("moving to static");
                   
            //     }
            //     predictionWayPoint();
            //     anim.SetFloat("vely", navMeshAgent.velocity.magnitude/navMeshAgent.speed);
            //     //Debug.Log("Moving wp");
            //     break;
        }
    }
    


}
