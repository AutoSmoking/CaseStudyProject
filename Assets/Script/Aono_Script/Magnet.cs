using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet : MonoBehaviour
{
    public bool playerClosing;
    private SphereCollider sphereCol;

    // Start is called before the first frame update
    void Start()
    {
        sphereCol = GetComponent<SphereCollider>();
    }

    public Transform other;
    float dist;


    void Chase()
    {
        Debug.Log("Clear");
    }

    void OnTriggerStay(Collider col)
    {
        if (col.gameObject.tag == "1")
        {
            playerClosing = true;
            sphereCol.radius = 10f;
        }

    }
    void OnTriggerExit(Collider col)
    {
        playerClosing = false;
        sphereCol.radius = 5f;
    }

    // Update is called once per frame
    void Update()
    {

        void Example()
        {
            if (other)
            {
                dist = Vector3.Distance(other.position, transform.position);
                //print("Distance to other: " + dist);
            }
        }
        Debug.Log(dist);
        
    }
}
