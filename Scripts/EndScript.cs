 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndScript : MonoBehaviour
{
    // Start is called before the first frame update
    private int score;
    private float time;
    public TextMeshProUGUI scoreText;
    // public TextMeshProUGUI timeText;
    // public TextMeshProUGUI totalText;
    public TextMeshProUGUI medal1;
    public TextMeshProUGUI medal2;
    public GameObject nextButton;

    public RawImage img1;
    public RawImage img2;
    public Texture bronze;
    public Texture silver;
    public Texture gold;
    public Texture unplaced;
    private CanvasGroup canvasGroup;

    public enum Medal {
        Bronze=1800,
        Silver=2100,
        Gold=2400
    };

    public static int Bronze;
    public static int Silver;
    public static int Gold;

    void Start()
    {
        
        //score = PeterController.score;
        //int time = ps.time;
        Bronze = 1800;
        Silver = 2100;
        Gold = 2400;
        canvasGroup = GetComponent<CanvasGroup>();
        if (SceneManager.GetActiveScene().name == "Level_2") {
            Bronze = 2000;
            Silver = 2500;
            Gold = 3000;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (canvasGroup.alpha == 1f) {
        score = PeterController.score;
        time = PeterController.time;
        // Debug.Log(score);
        // Debug.Log(time);
        scoreText.text = "Score: " + score.ToString() + " POINTS";
        //timeText.text = "Time: " + (time).ToString("n2") + "s" + " = " + (((int)(100000/time)/100) * 100).ToString() + " POINTS";
        int total = calculateScore(score, time);
        //Debug.Log(total);
        //totalText.text = "Total Score: " + total.ToString() + " POINTS";
        bool won = true;
        if (total < Bronze) {
            medal1.text = "UNPLACED";
            medal2.text = "UNPLACED";
            img1.texture = unplaced;
            img2.texture = unplaced;
            img1.rectTransform.sizeDelta = new Vector2(290, 240);
            img2.rectTransform.sizeDelta = new Vector2(290, 240);
            nextButton.SetActive(false);
            won = false;
        }
        if (total >= Bronze && total < Silver) {
            medal1.text = "BRONZE";
            medal2.text = "BRONZE";
            img1.texture = bronze;
            img2.texture = bronze;
            img1.rectTransform.sizeDelta = new Vector2(200, 240);
            img2.rectTransform.sizeDelta = new Vector2(200, 240);
            nextButton.SetActive(true);
            
        }
        if (total >= Silver && total < Gold) {
            medal1.text = "SILVER";
            medal2.text = "SILVER";
            img1.texture = silver;
            img2.texture = silver;
            img1.rectTransform.sizeDelta = new Vector2(200, 240);
            img2.rectTransform.sizeDelta = new Vector2(200, 240);
            nextButton.SetActive(true);

        }
        if (total >= Gold) {
            medal1.text = "GOLD";
            medal2.text = "GOLD";
            img1.texture = gold;
            img2.texture = gold;
            img1.rectTransform.sizeDelta = new Vector2(200, 240);
            img2.rectTransform.sizeDelta = new Vector2(200, 240);
            nextButton.SetActive(true);

        }
        FindObjectOfType<ProgressBar>().OnProgressComplete(won);
        }
    }

    public int calculateScore(int score, float time) {
        if(time == 0) {
            return score;
        } else {
            return score;
        }
    }

    public void NextLevel() {
        if (SceneManager.GetActiveScene().name == "PeterCity") {
            SceneManager.LoadScene("Level_2");
        }
        if (SceneManager.GetActiveScene().name == "Level_2") {
            SceneManager.LoadScene("Level_3_Downhill");
        }
        if (SceneManager.GetActiveScene().name == "Level_3_Downhill") {
            SceneManager.LoadScene("End_Scene");
        }
    }
}
