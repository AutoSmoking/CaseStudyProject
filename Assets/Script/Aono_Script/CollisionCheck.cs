using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionCheck : MonoBehaviour
{

    GameObject gameObject;

    // Start is called before the first frame update
    void Start()
    {
        //gameObject = GameObject.FindGameObjectWithTag("1"); 
        gameObject =  GameObject.Find("Sphere");
    }



    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("当たったよ");
        
        
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "2")
        {
            BubbleOperation bubble = gameObject.GetComponent<BubbleOperation>();
            //bubble.Say();
            //bubble.Union();
            //Debug.Log("当たったよ");
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
