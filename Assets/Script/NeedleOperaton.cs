using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineOperaton : MonoBehaviour
{
    
    GameObject Bubble;

    // Start is called before the first frame update
    void Start()
    {
        Bubble = GameObject.Find("Bubble");
    }

    void OnCollisionEnter(Collision collision)
    {//泡と接触した時の処理
        Destroy(Bubble);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
