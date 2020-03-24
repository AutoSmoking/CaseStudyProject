using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleOperation : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField, Header("泡の浮上速度"), Range(0, 0.1f)]
    float FloatAcceleration;

    int floatflag = 0;

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {   //ゴールに接触した時にログを出す
        if (other.CompareTag("Finish"))
        {
            Debug.Log("HitHit");
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            floatflag++;
        }




        if (floatflag != 0)
        {
            //ワールドの軸に合わせて移動
            transform.Translate(new Vector3(0, FloatAcceleration, 0), Space.World);
        }
    }
}
