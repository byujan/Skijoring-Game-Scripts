using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraRotator : MonoBehaviour
{
    // Start is called before the first frame update


    public GameObject player;
    public Transform target;

    private Vector3 offset;
    private float cameraDistance = 40.0f;
    private int yadd = 15;
    private int xrot = 0;
    void Start()
    {
        //offset = transform.position - player.transform.position;
        if (SceneManager.GetActiveScene().name == "Level_3_Downhill") {
            yadd = 20;
            xrot = 10;
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {

        transform.position = player.transform.position - player.transform.forward * cameraDistance;
        transform.LookAt (player.transform.position);
        transform.position = new Vector3 (transform.position.x, transform.position.y + yadd, transform.position.z);
        Vector3 rotationToAdd = new Vector3(20+xrot, 0, 0);
        transform.Rotate(rotationToAdd);
    }
}
