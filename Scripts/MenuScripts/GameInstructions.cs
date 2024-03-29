using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstructions : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        print("Game Started");
        SceneManager.LoadScene("Instructions");
        Time.timeScale = 1f;
    }
    
}
