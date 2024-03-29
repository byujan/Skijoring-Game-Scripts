using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreScreen : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private bool windowClosed = false;
    
    // Start is called before the first frame update
    void Start()
    {
        // pause the game
        canvasGroup = GetComponent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        // press space bar to start game
        if (Input.GetKeyDown(KeyCode.Return))
        {
            canvasGroup.alpha = 0;
            
            // stop this script since we won't use it again in the scene
            this.enabled = false;
        }
    }
}
