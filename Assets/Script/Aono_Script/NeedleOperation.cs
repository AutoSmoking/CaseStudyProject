using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

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
            SEManager.Instance.Play(SEPath.BUBBLE_DEATH);
            Destroy(Bubble);
        }

        if (other.gameObject.tag == "2")
        {
            SEManager.Instance.Play(SEPath.BUBBLE_DEATH);
            Destroy(Bubble2);
        }

        if (other.gameObject.tag == "3")
        {
            SEManager.Instance.Play(SEPath.BUBBLE_DEATH);
            Destroy(Bubble3);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
