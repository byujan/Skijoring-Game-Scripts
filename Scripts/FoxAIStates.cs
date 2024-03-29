using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class FoxAIStates : MonoBehaviour
{
    
    protected Animator anim;
    protected NavMeshAgent nav;
    public GameObject tracker;
    public GameObject finishPoint;
    public GameObject[] waypoints;
    public int currWaypoint;
    public int state;
    public int state2_mag;
    public int state1_mag;
    protected float offsetStart;
    private bool begin = false;
    public GameObject player;
    public int dogSpeed;
    public int trackerZdistanceAhead;

    private Vector3 oldPlayerPosition;
    //create a delay
    private float timeSinceUpdate = 0.0f;
    public float trackingLagTime;
    public float speedUpStateDuration;
    private float timeRemaining = 0.0f;


    // Start is called before the first frame update
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //anim.SetTrigger("run");
        nav = GetComponent<NavMeshAgent>();
        oldPlayerPosition = player.transform.position;
        setState(state);
        offsetStart = Random.Range(0f, Mathf.PI * 2);
        
        if(waypoints!=null)
        {
            currWaypoint = 0;
        }
        setNextWaypoint();
        begin = true;
    }

    void OnTriggerEnter(Collider c)
    {
         if (c.CompareTag("Waypoint"))
        {
            currWaypoint++;
            c.GetComponent<Collider>().enabled = false; //can't hit twice
        }
        
    }

    private void setNextWaypoint()
    {

        Vector3 targetWaypoint = getTargetStateWaypoint();
        nav.SetDestination(targetWaypoint);
        tracker.transform.position = targetWaypoint;
    }

    public void setState(int input)
    {
        state = input;
        if(state==0)
        {
            nav.speed = 0;
            anim.SetFloat("Run", -1);
        }
        else if(state==1)
        {
            nav.speed = dogSpeed;
            //nav.speed = 15;
            anim.SetFloat("Run", 1);
        }
        else if (state == 2)
        {
            nav.speed = dogSpeed;
            anim.SetFloat("Run", 2);
        }
        else if (state == 3)
        {
            nav.speed = dogSpeed;
            anim.SetFloat("Run", 2);
        }
        else if (state == 4)
        {
            nav.speed = dogSpeed;
            anim.SetFloat("Run", 2);
        }
        else if (state == 5) //special state
        {
            nav.speed = dogSpeed*2;
            anim.SetFloat("Run", 2);
            if (timeRemaining > 0)
            {
                timeRemaining += speedUpStateDuration;
            }
            else
            {
                timeRemaining = speedUpStateDuration;
            }

        }
        else //should never be hit
        {
            Debug.Log("Tried to set Fox AI State to: " + input.ToString());
        }
    }

    private Vector3 getTargetStateWaypoint()
    {
        
        Vector3 noisyState = new Vector3(0, 0, 0);

        if (waypoints.Length > 0) //special handling for NavmeshLink jump locations
        {
            if (waypoints[currWaypoint].CompareTag("Jump"))
            {
                return waypoints[currWaypoint].transform.position;
            }
        }


        if (state == 0)
        {
            noisyState = -1 * finishWaypoint(); //Not moving

        }
        else if (state == 1)
        {
            noisyState = new Vector3(Mathf.Cos(offsetStart + Time.time) * state1_mag, 0, 0);
        }
        else if (state == 2)
        {
            noisyState = new Vector3(Mathf.Sin(offsetStart + Time.time) * state2_mag, 0, 0);
        }
        else if (state == 3)
        {

            noisyState = new Vector3((oldPlayerPosition.x - anim.transform.position.x), 0, 0);
        }
        else if (state == 4)
        {
            float noisyX = 0;
            float noisyZ = 0;
            if (Mathf.Abs(directionTowardsDest().normalized.x) > 0.8)
            {
                noisyZ = (oldPlayerPosition.z - anim.transform.position.z) / 2;// + Mathf.Sin(offsetStart + Time.time) * state2_mag;
            }
            else if (Mathf.Abs(directionTowardsDest().normalized.z) > 0.8)
            {

                noisyX = (oldPlayerPosition.x - anim.transform.position.x) / 2;//+ Mathf.Sin(offsetStart + Time.time) * state2_mag;
            }
            
            //noisyZ = (oldPlayerPosition.x - anim.transform.position.x) / 2 * Mathf.Abs(directionTowardsDest().normalized.x);
            //noisyX = (oldPlayerPosition.x - anim.transform.position.x) / 2 * Mathf.Abs(directionTowardsDest().normalized.z);
            noisyState = new Vector3(noisyX, 0, noisyZ);
            //noisyState = noisyState + new Vector3(Mathf.Sin(offsetStart + Time.time) * state2_mag, 0, 0);
            //noisyState = new Vector3((oldPlayerPosition.x - anim.transform.position.x)/2, 0, 0);
            //noisyState = noisyState + new Vector3(Mathf.Sin(offsetStart + Time.time) * state2_mag, 0, 0);
            Vector3 tryTarget = anim.transform.position + 10 * directionTowardsDest() + noisyState;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(tryTarget, out hit, 10f, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        else if(state==5)
        {
            timeRemaining -= Time.deltaTime;
            noisyState = new Vector3(player.transform.position.x - anim.transform.position.x, 0, 0);
            if (timeRemaining<0)
            {
                this.GetComponent<FoxAIStateController>().normalState();
            }
        }


        return anim.transform.position + 10 * directionTowardsDest() + noisyState;
        //return noisyState + finishWaypoint();
    }



    private Vector3 directionTowardsDest()
    {
        return (finishWaypoint() - anim.transform.position).normalized;
    }    
    private Vector3 finishWaypoint()
    {
        if (waypoints.Length>0)
        {
            return waypoints[currWaypoint].transform.position +new Vector3(0, 0, -trackerZdistanceAhead);
        }
        return finishPoint.transform.position + new Vector3(0,0,-10); //move waypoint back
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceUpdate += Time.deltaTime;
        
        setPlayerPositionOneSecondAgo();
        NavMeshAgent nav = GetComponent<NavMeshAgent>();
        if (state == 0)
        {
            return;
        }
        //anim.SetFloat("vely", nav.velocity.magnitude / nav.speed);
        setNextWaypoint();
        if (reachedFinishWaypoint())
        {
            if(waypoints!=null)
            {
                if(currWaypoint<waypoints.Length-1)
                {
                    //currWaypoint++; changing to collisions?
                }
                else
                {
                    //Finished
                    setState(0);
                }
            }
            else
            {
                //Finished
                setState(0);
            }
            
            
        }

    }
    private void setPlayerPositionOneSecondAgo()
    {
        if (timeSinceUpdate > trackingLagTime)
        {
            timeSinceUpdate = 0.0f;
            oldPlayerPosition = player.transform.position;
        }
    }

    private bool reachedFinishWaypoint()
    {
        //if ((finishWaypoint() - anim.transform.position).magnitude < 5)
        if(nav.remainingDistance - nav.stoppingDistance < 2 && !nav.pathPending && begin)
        {
            return true;
        }
        return false;
    }
}
