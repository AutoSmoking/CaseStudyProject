using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleOperaton : MonoBehaviour
{
    
    GameObject Bubble;

    // Start is called before the first frame update
    void Start()
    {
        Bubble = GameObject.FindGameObjectWithTag("Player");
    }

    void OnCollisionEnter(Collision collision)
    {   
        if (collision.CompareTag("Player"))
        {
            Destroy(Bubble);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
