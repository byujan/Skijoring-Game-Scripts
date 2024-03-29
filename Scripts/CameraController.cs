using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        offset = transform.position - player.transform.position;
        FindObjectOfType<SoundManager>().PlaySound("Claps");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //transform.position = player.transform.position + offset;
        //transform.LookAt(player.transform);
        transform.position = player.transform.position  + offset;
        // Vector3 forward = player.transform.forward;
        // Quaternion quat = Quaternion.Euler(forward.x, forward.y, forward.z);
        // transform.rotation = transform.rotation * quat;
        Debug.Log(player.transform.forward);
        //Debug.Log(transform.position);
        //transform.position = player.transform.position + (-player.transform.forward * offset.magnitude);

    }
  
}
