using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class PauseMenuToggle : MonoBehaviour
{
    // Start is called before the first frame update
    private CanvasGroup canvasGroup;
    public string backgroundSong;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timeText;
    
    void Start()
    {
        
    }
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown (KeyCode.Escape)) { 
            if (canvasGroup.interactable) {
                canvasGroup.interactable = false; 
                canvasGroup.blocksRaycasts = false; 
                canvasGroup.alpha = 0;
                Time.timeScale = 1;
                scoreText.enabled = true;
                timeText.enabled = true;
                FindObjectOfType<SoundManager>().PlaySound("Ice_skiing");
                if (!string.IsNullOrEmpty(backgroundSong)) {
                    FindObjectOfType<SoundManager>().PlaySound(backgroundSong);
                }
            } else { 
                canvasGroup.interactable = true; 
                canvasGroup.blocksRaycasts = true;
                scoreText.enabled = true;
                timeText.enabled = true;
                canvasGroup.alpha = 1; 
                Time.timeScale = 0;
                FindObjectOfType<SoundManager>().StopAllSound();
            }
        }
    }

    public void Resume() {
        canvasGroup.interactable = false; 
        canvasGroup.blocksRaycasts = false; 
        canvasGroup.alpha = 0;
        Time.timeScale = 1;
    }

    public void Restart() {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

    public void MainMenu() {
        SceneManager.LoadScene("StartScreen");
        Time.timeScale = 1;
    }
}
