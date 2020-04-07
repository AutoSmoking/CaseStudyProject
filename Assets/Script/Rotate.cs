using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{
    //円運動の中心となるGameObject
    [SerializeField] GameObject center;
    //円運動の速度
    [SerializeField, Header("速度"), Range(0, 100.0f)]
    float speed = 0.0f;

    //// Use this for initialization
    void Start()
    {
  
    }

    // Update is called once per frame
    void Update()
    {

        //RotateAround(円運動の中心,進行方向,速度)
        transform.RotateAround(center.transform.position,
        transform.forward, speed * Time.deltaTime);


    }
}