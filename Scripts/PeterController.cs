using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class PeterController : MonoBehaviour
{
    // Start is called before the first frame update
    // public variables
    public float speed;
    public static int score;
    public static float time;
    public float jumpPower;
    
    // logic variables
    private bool started;
    private bool jumping = false;
    public static bool isGrounded = true;
    private int t = 0;  // for tracking time during gravity simulations
    public float GravAccel = 9.8f;  // the acceleration due to gravity
    public float groundLockDistance = 5;  // length of ground raycast
    public float FloatingDistance = 1.5f;  // the space between the character and ground
    public float minGroundY = -100; // if the player goes below this, automatically bring them back to fox's position
    private float fallingGroundSearch = 100;
    private bool isFalling = false;

    
    // attached components
    private Rigidbody rb;
    private Animator anim;
    SoundManager soundManager;
    public Animator ski_anim;
    public Animator scoreanim;
    private AudioSource audiosource;
    
    // textMeshPro
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI addScoreText;
    
    // fox object
    public GameObject FoxObject;
    public GameObject SkiObject;
    public GameObject camera;
    
    // end screen
    public CanvasGroup canvasGroup;
    float foxz;
    float peterz;
    
    // debug
    private bool debug = true;
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        anim = GetComponent<Animator>();
        //ski_anim = GetComponent<Animator>();
        audiosource = GetComponent<AudioSource>();
        score = 0;
        time = 0;
        started = false;
        SetTimeText();
        scoreText.text = "0";
        SetScoreText();
        rb = GetComponent<Rigidbody>();
        //addScoreText.enabled = false;
    }

    void SetScoreText() {
        scoreText.text = score.ToString();
    }

    void SetTimeText() {
        if (started) {
            timeText.text = (time).ToString("n2") + "s";
        }
        if (!started) {
            timeText.text = "0.00s";
        }
            
        
        
    }

    public Vector3 GetMovementVector(bool jumpCommand)
    {
        RaycastHit hit;
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        float y = 0f;
        
        // limit how fast the player can move forward
        if (z > 0)
        {
            z = z / 2;
        }
        
        // check if player is near the ground ground
        if (Physics.Raycast(transform.position, Vector3.down, out hit, groundLockDistance) && 
            hit.transform.CompareTag("Ground"))
        {
            // place character on ground
            isGrounded = true;
            isFalling = false;
            if (debug)
                print("isGrounded");
        }
        else
        {
            isGrounded = false;
            // check if player is above the ground
            if (Physics.Raycast(transform.position, Vector3.down, out hit, fallingGroundSearch) &&
                hit.transform.CompareTag("Ground"))
            {
                isFalling = true;
            }
        }
        
        // initiate jump
        if (jumpCommand && isGrounded && !jumping)
        {
            t = 0;
            jumping = true;
            if (debug)
                print("jump initiated");
        }
        
        // FAILSAFE STATE: if player falls beneath the ground
        if (transform.position.y < minGroundY)
        {
            // set to fox's y position
            y = FoxObject.transform.position.y - transform.position.y + FloatingDistance;
            if (debug)
                print("player position reset!!!");
        }
        
        // jumping physics
        else if (jumping) {
            y = jumpPower - GravAccel * t * Time.deltaTime;
            t++;
            if (debug)
            {
                print("jumppower=" + jumpPower + ", time=" + t + ", gravity=" + GravAccel + ", height=" +
                      transform.position.y);
                print("jump.y=" + y);
            }
            // landing after a jump
            if (y < 0 && isGrounded)
            {
                y = 0;
                jumping = false;
                if (debug)
                    print("landed");
            }
        }
        // ground lock mechanics
        else if (isGrounded)
        {
            t = 0;  // reset gravity acceleration
            
            // calculate distance from ground
            float newY = hit.point.y + FloatingDistance - transform.position.y;
            
            // if the change is significant
            if (math.abs(newY) > 0.1)
                // move player
                y = newY;
            else
                y = 0;
            if (debug)
            {
                print("locked to ground. hit.y=" + hit.point.y + " player.y=" + transform.position.y);
                print("y.transform=" + y);  
            }
        } 
        // falling mechanics
        else if (isFalling)
        {
            y = -GravAccel * t * Time.deltaTime;
            t++;
        }


        // create final vector
        Vector3 movement = new Vector3(x, y, z) * speed;
        return movement;
        
        /*
        foxz = FoxObject.transform.position.z;
        peterz = transform.position.z;
        if (peterz > (foxz+20f)) 
        {
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(x, 0, z) * speed;
            return movement;
        }
    return new Vector3();*/
    }

    private bool checkPeterFoxPos() {
        float foxz = FoxObject.transform.position.z;
        float peterz = transform.position.z;
        if (peterz < foxz) {
            Debug.Log("Peter is further than Fox!");
        }
        return false;
    }

    // Update is called once per frame
    void Update()
    {

        //this.transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,0) * speed * Time.deltaTime);
        //this.transform.rotation = FoxObject.transform.rotation;
        //SkiObject.transform.rotation = FoxObject.transform.rotation;
        anim.SetFloat("Lean", Input.GetAxis("Horizontal"));
        ski_anim.SetFloat("Lean", Input.GetAxis("Horizontal"));
        if(started) {
            time += Time.deltaTime;
        }
        if (Input.GetKey("r")) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Time.timeScale == 0f) {
            timeText.enabled = false;
            scoreText.enabled = false;
        }
        float foxdist = Vector3.Distance(this.transform.position, FoxObject.transform.position);
        if (foxdist > 18) {
            Debug.Log("Fox is too far!!");
        }

    }
    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void FixedUpdate() {
        if(started) {
            SetTimeText();
        }
    }

    // void ShowFloatingText()
    // {
    //     Vector3 xyz = new Vector3(0, 180, 0);
    //     Quaternion newRotation = Quaternion.Euler(xyz);
    //     Vector3 new_position = new Vector3(transform.position.x, transform.position.y+20, transform.position.z);

    //     var text = Instantiate(FloatingText, new_position, newRotation, transform);
    //     text.GetComponent<TextMeshPro>().text = "+100";

    // }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("PickUp")) {
            other.gameObject.SetActive(false);
            score += 100;
            addScoreText.text = "+100";
            audiosource.Play();
            scoreanim.Play("plusScore");
            SetScoreText();
        }
        if (other.gameObject.CompareTag("GoldPickUp")) {
            other.gameObject.SetActive(false);
            score += 300;
            addScoreText.text = "+300";
            audiosource.Play();
            scoreanim.Play("plusScore");
            SetScoreText();
        }
        if (other.gameObject.CompareTag("Crate")) {
            //other.gameObject.SetActive(false);
            score -= 200;
            addScoreText.text = "-200";
            //audiosource.Play();
            scoreanim.Play("plusScore");
            SetScoreText();
        }
        if (other.gameObject.CompareTag("ThroughGate")) {
            
            other.gameObject.SetActive(false);
            score += 150;
            addScoreText.text = "+150";
            soundManager.PlaySound("Gates");
            scoreanim.Play("plusScore");
            SetScoreText();
        }
        if (other.gameObject.CompareTag("StartGate")) {
            other.gameObject.SetActive(false);
            started = true;
            Debug.Log("TRUE");
        }
        if (other.gameObject.CompareTag("EndZone"))
        {
            canvasGroup.interactable = true; 
            canvasGroup.blocksRaycasts = true; 
            canvasGroup.alpha = 1f; 
            Time.timeScale = 0f;
        }
    }
}
