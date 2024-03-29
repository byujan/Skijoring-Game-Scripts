using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueOnKey : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            // start the game
            gameObject.GetComponent<GameStarter>().StartGame();
        }
    }
}
