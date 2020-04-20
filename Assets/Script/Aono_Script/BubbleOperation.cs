using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleOperation : MonoBehaviour
{
    // Start is called before the first frame update

    //////////////////////////////////////////////////
    //readme
    //Bubble「１」「３」にアタッチしてください
    //////////////////////////////////////////////////

    GameObject Bubble;
    GameObject Bubble2;

    [SerializeField, Header("泡の浮上速度"), Range(0, 0.1f)]
    float FloatAcceleration;

    [SerializeField, Header("結合した泡に加算されるサイズ"), Range(0, 10.0f)]
    float BubbleSize;

    int floatflag = 0;

    void Start()
    {
        Bubble = GameObject.FindGameObjectWithTag("2");
        Bubble2 = GameObject.FindGameObjectWithTag("3");
    }

    void OnTriggerEnter(Collider other)
    {   //ゴールに接触した時にログを出す
        if (other.CompareTag("Finish"))
        {
                Debug.Log("GoalHit");
  
        }

        
    }

    void OnCollisionEnter(Collision other)
    {

        Transform myTransform = this.transform;

        if (other.gameObject.tag == "2")
        {
            Destroy(Bubble);
            //BubbleSize = BubbleSize + 1.0f; ;
            //this.gameObject.transform.localScale = new Vector3(localScale.x + BubbleSize, localScale.y + BubbleSize, 0.6f);
            Vector3 localScale = myTransform.localScale;
            localScale.x = localScale.x + BubbleSize;
            localScale.y = localScale.y + BubbleSize;
            localScale.z = localScale.z; 
            myTransform.localScale = localScale;
        }

        if (other.gameObject.tag == "3")
        {
            Destroy(Bubble2);
            //BubbleSize = BubbleSize + 0.6f;
            //this.gameObject.transform.localScale = new Vector3(BubbleSize, BubbleSize, 0.6f);
            Vector3 localScale = myTransform.localScale;
            localScale.x = localScale.x + BubbleSize;
            localScale.y = localScale.y + BubbleSize;
            localScale.z = localScale.z;
            myTransform.localScale = localScale;
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
