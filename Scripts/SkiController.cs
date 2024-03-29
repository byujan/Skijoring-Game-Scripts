using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkiController : MonoBehaviour
{

    public CanvasGroup canvasGroup;
    private void Start()
    {

    }

    void Update()
    {
        //this.transform.Translate(new Vector3(Input.GetAxis("Horizontal"),0,0) * 55 * Time.deltaTime);
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    void OnJointBreak(float breakForce)
    {
        canvasGroup.interactable = true; 
        canvasGroup.blocksRaycasts = true; 
        canvasGroup.alpha = 1f; 
        Time.timeScale = 0f;
        Debug.Log("A joint has just been broken!, force: " + breakForce);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
