using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CanvasController : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI timeText;
    public float timeRemaining;
    void Start()
    {
        timeRemaining = 0;
        SetTimeText();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeRemaining >= 0) {
            timeRemaining += Time.deltaTime;
        }
    }

    void SetTimeText() {
        timeText.text = "Time: " + (timeRemaining).ToString("n2") + "s";
    }

    void FixedUpdate() {
        SetTimeText();
    }
}
