using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class deduct : MonoBehaviour
{

    public TextMeshProUGUI scoreText;
    public static int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void SetScoreText()
    {
        scoreText.text = score.ToString();
    }
}
