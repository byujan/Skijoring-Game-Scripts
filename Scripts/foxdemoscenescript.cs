using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class foxdemoscenescript : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
        anim.Play("Fox_Attack_Paws");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
