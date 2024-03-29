using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

//Simulate a rope with verlet integration and no springs
public class RopeSimulator : MonoBehaviour
{
    private float gravity = 9.8f;
    public int numIterations;
    public int numRopeSegments = 10;
    public float ropeWidth = 0.2f;
    public float pullStrength = 1;

    // game objects to attach rope to
    public GameObject attachedObject; 
    public GameObject pullingObject;
    private Rigidbody attachedRigidbody;
    private Point[] points;
    private Stick[] sticks;
    private PeterController controller;  // so we can account for attached object's movement
    private Point firstPoint;
    private Point lastPoint;
    
    // used to display rope
    private LineRenderer line;
    
    // added jump mechanic
    private bool jump = false;
    private int t = 0;
    private bool debug = false;
    
    public class Point
    {
        public Vector3 position, prevPosition;
        public bool hasGravity = false;

        public Point(Vector3 position)
        {
            this.position = position;
            this.prevPosition = position;
            this.hasGravity = false;
        }
    }

    public class Stick
    {
        public Point pointA, pointB;
        public float length = 0;

        public Stick(Point pointA, Point pointB)
        {
            this.pointA = pointA;
            this.pointB = pointB;
            this.length = (pointA.position - pointB.position).magnitude ;
        }
    }
    
    // set up initial rope points
    private void Start()
    {
        //Init the line renderer we use to display the rope
        line = GetComponent<LineRenderer>();
        attachedRigidbody = attachedObject.GetComponent<Rigidbody>();
        
        // initialize point and stick arrays
        points = new Point[numRopeSegments];
        sticks = new Stick[numRopeSegments - 1];
        
        // get the initial start and end position of the rope
        Vector3 ropeVector = attachedObject.transform.position - pullingObject.transform.position;
        
        // normalize rope vector to individual segments
        ropeVector = ropeVector / numRopeSegments;
        
        // get the movement controller for the item being "pulled"
        controller = attachedObject.GetComponent<PeterController>();

        // create all the points
        for (int i = 0; i < numRopeSegments; i++)
        {
            points[i] = new Point(pullingObject.transform.position + i * ropeVector);
        }
        
        // lock the first and last points
        points[0].hasGravity = false;
        points[points.Length - 1].hasGravity = false;
        
        // create all the sticks
        for (int i = 0; i < sticks.Length; i++)
        {
            sticks[i] = new Stick(points[i], points[i + 1]);
        }
        
        // store first and last points for easy access
        firstPoint = sticks[0].pointA;
        lastPoint = sticks[sticks.Length - 1].pointB;
    }

    // Rope simulation logic
    void Update()
    {
        // allow first point to move with object
        firstPoint.position = pullingObject.transform.position;

        Vector3 direction = Vector3.zero;
        foreach (Stick stick in sticks)
        {
            // add gravity's pull
            if (stick.pointB.hasGravity)
                stick.pointB.position += Physics.gravity * Time.deltaTime;
            
            // apply pull of leading point
            stick.pointB.position = stick.pointA.position + (stick.pointB.position - stick.pointA.position).normalized * stick.length;
        }

        // get rope translate vector
        var ropeForce = (lastPoint.position - attachedObject.transform.position);
        ropeForce.y = 0;  // x and z are inversed but y is normal
        if (debug)
        {
            print("rope.force=" + ropeForce);
            print("lastpoint=" + lastPoint.position + ", object=" + attachedObject.transform.position);  
        }
        
        // convert rope vector from world space to the attached object's space
        ropeForce = attachedObject.transform.InverseTransformVector(ropeForce);
        if (debug)
            print("newRope.force=" + ropeForce);

        // get controller translate vector
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (debug)
                print("jump");
            jump = true;
            t = -1;
            //attachedRigidbody.useGravity = false;
        }
        var controllerForce = controller.GetMovementVector(jump);

        // stop the jump calculation once the positive force has been diminished over time
        if (jump && controllerForce.y == 0)
        {
            if (debug)
                print("jump ended");
            jump = false;
        }

        // Move the attached object
        Vector3 translateVector = (pullStrength * ropeForce + controllerForce) * Time.deltaTime;
        attachedRigidbody.transform.Translate(translateVector);  // translate
        if (debug)
            print("translateVector=" + translateVector);
        //attachedRigidbody.transform.LookAt(new Vector3(pullingObject.transform.position.x, attachedRigidbody.transform.position.y, pullingObject.transform.position.z));

        // move the last stick's point to its new position
        lastPoint.position = attachedRigidbody.position;
        
        Point pointA;
        Point pointB;
        // reverse loop through the sticks
        for (int i = sticks.Length - 1; i >= 0; i--)
        {
            pointA = sticks[i].pointA;
            pointB = sticks[i].pointB;
            pointA.position = pointB.position + (pointA.position - pointB.position).normalized * sticks[i].length;
            pointB.prevPosition = pointB.position;
            pointA.prevPosition = pointA.position;
        }
        
        // place first rope point at pulling object's location
        firstPoint.position = pullingObject.transform.position;
        
        // rotate attached object
        //attachedRigidbody.transform.LookAt(new Vector3(pullingObject.transform.position.x, attachedRigidbody.transform.position.y, pullingObject.transform.position.z));
        attachedObject.transform.Rotate(Vector3.up, pullingObject.transform.eulerAngles.y - attachedObject.transform.eulerAngles.y);

        DisplayRope();  // draw rope
    }

    private void DisplayRope()
    {
        line.startWidth = ropeWidth;
        line.endWidth = ropeWidth;
        
        // array to store line positions
        Vector3[] positions = new Vector3[points.Length];
        
        for (int i = 0; i < positions.Length; i++)
        {
            positions[i] = points[i].position;
        }
        // draw line
        line.positionCount = points.Length;
        line.SetPositions(positions);
    }
}