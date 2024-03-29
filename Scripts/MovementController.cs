using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    // Start is called before the first frame update
    Vector3 moveDir = Vector3.zero;
    CharacterController controller;
    Animator anim;
    public float speed = 5f;
    public float rot = 0f;
    private float gravity = 8f;
    private float rotspeed = 80f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)) {
          anim.SetBool("Fox_Run", true);
          moveDir = new Vector3(0,0,1);
          moveDir *= speed;
          moveDir = transform.TransformDirection(moveDir);
        }
        if(Input.GetKeyUp(KeyCode.W)) {
          anim.SetBool("Fox_Run", false);
          moveDir = Vector3.zero;
        }
        rot += Input.GetAxis("Horizontal") * rotspeed * Time.deltaTime;
        transform.eulerAngles = new Vector3(0, rot, 0);
        moveDir.y -= gravity * Time.deltaTime;
        controller.Move(moveDir * Time.deltaTime);
    }
}
