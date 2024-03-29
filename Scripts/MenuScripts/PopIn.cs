using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopIn : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // start at 0 scale
        transform.localScale = Vector2.zero;
        
        // pop in
        transform.LeanScale(Vector2.one, 0.8f).setEaseOutExpo();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
}
