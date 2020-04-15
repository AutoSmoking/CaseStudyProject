using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleOperation2 : MonoBehaviour
{
    // Start is called before the first frame update

    //////////////////////////////////////////////////
    //readme
    //Bubble「2」にアタッチしてください
    //////////////////////////////////////////////////

    GameObject Bubble;
    GameObject Bubble2;

    [SerializeField, Header("泡の浮上速度"), Range(0, 0.1f)]
    float FloatAcceleration;

    int floatflag = 0;
    float BubbleSize = 0.06f;

    void Start()
    {
        Bubble = GameObject.FindGameObjectWithTag("1");
    }

    void OnTriggerEnter(Collider other)
    {   //ゴールに接触した時にログを出す
        if (other.CompareTag("Finish"))
        {
            FloatAcceleration = 0.001f;
        }


    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "3")
        {
            Destroy(Bubble);
            BubbleSize = BubbleSize + 0.06f;
            this.gameObject.transform.localScale = new Vector3(BubbleSize, BubbleSize, 0.6f);
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
