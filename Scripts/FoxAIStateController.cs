using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoxAIStateController : MonoBehaviour
{
    public AudioClip audioState0;
    public AudioClip audioState1;
    public AudioClip audioState2;
    public AudioClip dogBark;
    public AudioClip dogHowl;
    public int normState;
    public GameObject foxText;
    public Animator reaction_anim;
    public GameObject cam;
    TextMesh textObject;
    // Start is called before the first frame update
    int WaitForSeconds = 2;
    void Start()
    {
        //foxText.SetActive(false);
        foxText.GetComponent<TextMesh>().text = "";
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            foxText.GetComponent<TextMesh>().text = "~~";
            reaction_anim.Play("StatesAnim");
            GetComponent<FoxAIStates>().setState(0);
            AudioSource.PlayClipAtPoint(audioState0, cam.transform.position, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            foxText.GetComponent<TextMesh>().text = "!";
            reaction_anim.Play("StatesAnim");
            GetComponent<FoxAIStates>().setState(1);
            AudioSource.PlayClipAtPoint(audioState1, cam.transform.position, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            foxText.GetComponent<TextMesh>().text = "!!";
            reaction_anim.Play("StatesAnim");
            GetComponent<FoxAIStates>().setState(2);
            AudioSource.PlayClipAtPoint(audioState2, cam.transform.position, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            foxText.GetComponent<TextMesh>().text = "!?!";
            reaction_anim.Play("StatesAnim");
            GetComponent<FoxAIStates>().setState(3);
            AudioSource.PlayClipAtPoint(audioState2, cam.transform.position, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            foxText.GetComponent<TextMesh>().text = "!";
            reaction_anim.Play("StatesAnim");
            GetComponent<FoxAIStates>().setState(4);
            AudioSource.PlayClipAtPoint(audioState2, cam.transform.position, 1);
        }*/
        if (Input.GetKeyDown(KeyCode.LeftControl) || Input.GetKeyDown(KeyCode.RightControl))
        {

            switchState();

        }
    }

    public void foxAteRabbit()
    {
        AudioSource.PlayClipAtPoint(dogHowl, cam.transform.position, 2);
        foxText.GetComponent<TextMesh>().text = "YUM!";
        reaction_anim.Play("StatesAnim");
        GetComponent<FoxAIStates>().setState(5);
    }

    public void switchState()
    {

        
        AudioSource.PlayClipAtPoint(dogBark, cam.transform.position, 2);
        AudioSource.PlayClipAtPoint(audioState1, cam.transform.position, 1);
        

        if (GetComponent<FoxAIStates>().state != normState)
        {
            foxText.GetComponent<TextMesh>().text = "Woof!";

            GetComponent<FoxAIStates>().setState(normState); 
        }
        else
        {
            foxText.GetComponent<TextMesh>().text = "BARK!";
            GetComponent<FoxAIStates>().setState(1);
        }
        reaction_anim.Play("StatesAnim");

    }

    public void normalState()
    {

        foxText.GetComponent<TextMesh>().text = "Woof!";
        AudioSource.PlayClipAtPoint(dogBark, cam.transform.position, 2);
        reaction_anim.Play("StatesAnim");
        GetComponent<FoxAIStates>().setState(normState);

    }
}
