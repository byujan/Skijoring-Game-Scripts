using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class demoscenescript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Walking");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
