using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpOperation : MonoBehaviour
{
    [SerializeField, Header("ワープ先")]
    GameObject next;

    [SerializeField, Header("ワープまでの待ち時間"),Range(0.0f,3.0f)]
    float WaitTime;

    //[SerializeField,Header("")]

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
