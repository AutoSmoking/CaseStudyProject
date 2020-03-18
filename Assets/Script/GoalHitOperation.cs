using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHitOperation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider t)
    {   //ゴールに接触した時にログを出す
        Debug.Log("Hit");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
