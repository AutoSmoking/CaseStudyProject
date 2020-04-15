using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleOperation : MonoBehaviour
{
    GameObject Bubble;
    GameObject Bubble2;
    GameObject Bubble3;

    // Start is called before the first frame update
    void Start()
    {
        Bubble = GameObject.FindGameObjectWithTag("1");
        Bubble2 = GameObject.FindGameObjectWithTag("2");
        Bubble3 = GameObject.FindGameObjectWithTag("3");
    }

    void OnCollisionEnter(Collision other)
    { 
        if (other.gameObject.tag == "1")
        {
            Destroy(Bubble);
        }

        if (other.gameObject.tag == "2")
        {
            Destroy(Bubble2);
        }

        if (other.gameObject.tag == "3")
        {
            Destroy(Bubble3);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
