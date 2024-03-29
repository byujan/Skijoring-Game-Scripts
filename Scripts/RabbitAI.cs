using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class RabbitAI : MonoBehaviour
{
    public float radiusToRoamInside;
    protected Animator anim;
    protected NavMeshAgent nav;
   // public GameObject tracker;
    public float waitTime;
    private float timeSinceReached;
    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        setNextWaypoint();
        anim.SetInteger("AnimIndex", 1);
    }

    // Update is called once per frame
    void Update()
    {
        

        //anim.SetInteger("AnimIndex", 1);
        if (reachedFinishWaypoint())
        {
            timeSinceReached += Time.deltaTime;
            if (timeSinceReached > waitTime)
            {

                timeSinceReached = 0f;
                anim.SetInteger("AnimIndex", 1);
                setNextWaypoint();
            }
            else
            {
                anim.SetInteger("AnimIndex", -1); //idle
            }
        }
        if (dead)
        {
            anim.SetInteger("AnimIndex", -2);
        }
    }
    void OnTriggerEnter(Collider c)
    {
        anim.SetInteger("AnimIndex", -2); //dead
        if (c.CompareTag("Fox"))
        {
            anim.SetInteger("AnimIndex", -2); //dead
            GameObject fox = GameObject.FindGameObjectWithTag("Fox");
            fox.GetComponent<FoxAIStateController>().foxAteRabbit();
           
        }
        dead = true;
    }


    private Vector3 getTargetStateWaypoint()
    {
        if (dead)
        {
            return this.transform.position;
        }
        int count = 0;
        Vector3 target;
        bool foundTarget = attemptGetValidRandomTarget(out target);
        while(!foundTarget && count<=4)
        {
            foundTarget = attemptGetValidRandomTarget(out target);
            count++;
        }
        return target;
    }

    private bool attemptGetValidRandomTarget(out Vector3 target)
    {
        Vector3 randomSpot = transform.position + Random.insideUnitSphere * radiusToRoamInside;
        NavMeshHit hit;
        target = transform.position; //fall back to self
        if (NavMesh.SamplePosition(randomSpot, out hit, radiusToRoamInside, NavMesh.AllAreas))
        {
            target = hit.position;
            return true;
        }
        return false;
        

    }

    private void setNextWaypoint()
    {

        Vector3 targetWaypoint = getTargetStateWaypoint();
        nav.SetDestination(targetWaypoint);
        //tracker.transform.position = targetWaypoint;
    }

    private bool reachedFinishWaypoint()
    {
        //if ((finishWaypoint() - anim.transform.position).magnitude < 5)
        if (nav.remainingDistance - nav.stoppingDistance < 1 && !nav.pathPending)
        {
            return true;
        }
        return false;
    }
}
