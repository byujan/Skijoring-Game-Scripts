using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float speed = 2;
    public float jumpAmount = 1;
    private Rigidbody rb;
    private bool jump;
    private int jump_loop_count = 0;
    private bool down = false;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        jump_loop_count = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
    }

    public Vector3 GetMovementVector()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        Vector3 movement = new Vector3(x, 0, z) * speed;
        
        // jump mechanic
        if (jump)
        {
            // loop mechanic to ensure jump lasts longer than one frame
            if (jump_loop_count >= 0)
            {
                print("jump" + jump_loop_count);
                
                if (jump_loop_count == 5)
                {
                    down = true;
                }
                
                // falling
                if (down)
                {
                    movement += (Vector3.up * (jumpAmount*5));
                    jump_loop_count--;
                }
                // rising
                else
                {
                    movement += (Vector3.up * (jumpAmount * jump_loop_count));
                    jump_loop_count++;
                }
            }
            // when loop counter ends
            else
            {
              jump = false;
              down = false;
              jump_loop_count = 0; // reset loop counter
            }
        }
        return movement;
        //transform.Translate(movement * speed * Time.deltaTime);
    }
}
