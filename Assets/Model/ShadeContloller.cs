using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadeContloller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().material.SetColor("_BaseColor", Color.black);   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
