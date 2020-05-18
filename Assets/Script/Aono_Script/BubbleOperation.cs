using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class BubbleOperation : MonoBehaviour
{
    // Start is called before the first frame update

    //////////////////////////////////////////////////
    //readme
    //Bubble「１」「３」にアタッチしてください
    //////////////////////////////////////////////////

    GameObject Bubble;
    GameObject Bubble2;

    float FloatAcceleration;

    [SerializeField, Header("泡の浮上速度"), Range(0, 5.0f)]
    float MoveAcceleration;

    [SerializeField, Header("ステージが止まっている時の泡の浮上速度"), Range(0, 5.0f)]
    float StopAcceleration;

    [SerializeField, Header("結合した泡に加算されるサイズ"), Range(0, 10.0f)]
    float BubbleSize;

    [SerializeField] private Transform _parentTransform;

    int floatflag = 0;

    public bool DeathFlg = false;

    bool BubbleStopFlg;

    void Start()
    {
        Bubble = GameObject.FindGameObjectWithTag("2");
        Bubble2 = GameObject.FindGameObjectWithTag("3");

        //SFlag = SpinOperation.stopFlg;

    }

    void OnCollisionEnter(Collision other)
    {

        Transform myTransform = this.transform;

        if (other.gameObject.tag == "2")
        {
            Destroy(Bubble);
    
            transform.Translate(new Vector3(0, -BubbleSize, 0), Space.World);

            gameObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x + BubbleSize,
            gameObject.transform.localScale.y + BubbleSize,
            gameObject.transform.localScale.z
            );
        }

        if (other.gameObject.tag == "3")
        {
            Destroy(Bubble2);
            transform.Translate(new Vector3(0, -BubbleSize, 0), Space.World);

            gameObject.transform.localScale = new Vector3(
            gameObject.transform.localScale.x + BubbleSize,
            gameObject.transform.localScale.y + BubbleSize,
            gameObject.transform.localScale.z
            );
        }
    }


    // Update is called once per frame
    void Update()
    {
        BubbleStopFlg=SpinOperation.GetstopFlg();

        if (Input.GetKey(KeyCode.Space) && floatflag == 0)
        {
            floatflag++;

            // ここで音を鳴らす
            SEManager.Instance.Play("SE/Bubble_Birth");

            Destroy(gameObject.transform.Find("taru").gameObject);
        }

        if (BubbleStopFlg == true)
        {
            FloatAcceleration = StopAcceleration;
            Debug.Log("StageStopNow");
        }
        else
        {
            FloatAcceleration = MoveAcceleration;
            Debug.Log("StageMoveNow");
        }
        
    }

    void FixedUpdate()
    {
        if (floatflag != 0)
        {
            Rigidbody rb = this.transform.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, FloatAcceleration, 0);
        }
    }
}
