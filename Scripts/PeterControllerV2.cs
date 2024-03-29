using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(AudioSource))]

public class PeterControllerV2 : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public static int score;
    public static float time;
    private bool started;
    SoundManager soundManager;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    public TextMeshProUGUI addScoreText;
    private Animator anim;
    public Animator ski_anim;
    public Animator scoreanim;
    private AudioSource audiosource;
    public GameObject FoxObject;
    public GameObject SkiObject;

    // end screen
    public CanvasGroup canvasGroup;
    float foxz;
    float peterz;
    
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
        SetScoreText();
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

    public Vector3 GetMovementVector()
    {
        foxz = FoxObject.transform.position.z;
        peterz = transform.position.z;
        // if (peterz > (foxz+20f)) {
        //     float x = Input.GetAxis("Horizontal");
        //     float z = Input.GetAxis("Vertical");
        //     Vector3 movement = new Vector3(x, 0, z) * speed;
        //     return movement;
        // }

            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            Debug.Log(z);
            Vector3 movement = new Vector3(x, 0, z) * speed;
            this.transform.rotation = Quaternion.LookRotation(movement);
            return movement;

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

        //float z = Input.GetAxis("Vertical");
        
        this.transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical")) * speed * Time.deltaTime);
        this.transform.rotation = FoxObject.transform.rotation;
        SkiObject.transform.rotation = FoxObject.transform.rotation;
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
            //ShowFloatingText();
            score += 100;
            addScoreText.text = "+100";
            //soundManager.PlaySound("Coins");
            audiosource.Play();
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
            //Debug.Log("TRUE");
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
