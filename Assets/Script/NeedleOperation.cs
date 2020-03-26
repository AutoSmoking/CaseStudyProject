using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleOperation : MonoBehaviour
{
    GameObject Bubble;

    // Start is called before the first frame update
    void Start()
    {
        Bubble = GameObject.FindGameObjectWithTag("Player");
    }

    void OnTriggerEnter(Collider other)
    {   //ゴールに接触した時にログを出す
        if (other.gameObject.tag == "Player")
        {
            Destroy(Bubble);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
