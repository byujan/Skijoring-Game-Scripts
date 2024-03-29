using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public static bool isPaused = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void Pause()
    {
        // pause game
        Time.timeScale = 0;  
        
        // update global variable
        isPaused = true;
    }

    public static void UnPause()
    {
        // pause game
        Time.timeScale = 1;  
        
        // update global variable
        isPaused = false;
    }
}
