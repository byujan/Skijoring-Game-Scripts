using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStarter : MonoBehaviour
{
    // Start is called before the first frame update
    public void StartGame()
    {
        print("Game Started");
        SceneManager.LoadScene("PeterCity");
        Time.timeScale = 1f;
    }
    
}
