using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class destruction : MonoBehaviour
{


    public GameObject breakVersion;
    private float bForce = 1f;
    protected Rigidbody rb;
    private int active = 0;
    public static int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI addScoreText;
    private Animator scoreanim;

  
    // Start is called before the first frame update
    void Start()
    {
        rb =  GetComponent<Rigidbody>();
        scoreanim = addScoreText.GetComponent<Animator>();

    }
    // void SetScoreText()
    // {
    //     scoreText.text = (Convert.ToInt32(scoreText.text)-200).ToString();
    // }

    private void OnCollisionEnter(Collision collision)
    {
        if(rb.velocity.magnitude > bForce && active == 0)
        {
            active++;
            Instantiate(breakVersion, transform.position, transform.rotation);
            rb.AddExplosionForce(10f, Vector3.zero, 0f);
            Destroy(gameObject); 
            //scoreanim.Play("plusScore");
            //addScoreText.text = "-200";
            //SetScoreText();

        }
    
    }

    void Update()
    {
        print(scoreText.text);
    }


}
