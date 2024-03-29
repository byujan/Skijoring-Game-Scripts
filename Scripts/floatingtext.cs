using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingtext : MonoBehaviour
{

    private float DestroyTime = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
        Destroy(gameObject, DestroyTime);
    }

}
