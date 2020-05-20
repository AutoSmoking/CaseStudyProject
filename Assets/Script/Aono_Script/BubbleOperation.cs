using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class BubbleOperation : MonoBehaviour
{
    // Start is called before the first frame update

    GameObject Bubble;
    GameObject Bubble2;
    GameObject Bubble3;

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

    public static bool DeathBubble3 = false;

    bool DB3;

    void Start()
    {
        Bubble = GameObject.FindGameObjectWithTag("1");
        Bubble2 = GameObject.FindGameObjectWithTag("2");
        Bubble3 = GameObject.FindGameObjectWithTag("3");

    }

    void OnCollisionEnter(Collision other)
    {

        Transform myTransform = this.transform;

        //1の処理
        if (this.gameObject.CompareTag("1"))
        {
            //2と衝突した場合
            if (other.gameObject.tag == "2")
            {
                Destroy(Bubble2);

                DB3 = BubbleOperation.GetDB3Flag();

                if (DB3 == true)
                {
                    BubbleSize += 0.5f;
                }

                transform.Translate(new Vector3(0, -BubbleSize, 0), Space.World);

                gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x + BubbleSize,
                gameObject.transform.localScale.y + BubbleSize,
                gameObject.transform.localScale.z
                );
            }

            //3と衝突した場合
            if (other.gameObject.tag == "3")
            {
                Destroy(Bubble3);

                DB3 = BubbleOperation.GetDB3Flag();

                if (DeathBubble3 == true)
                {
                    BubbleSize += 0.5f;
                }

                transform.Translate(new Vector3(0, -BubbleSize, 0), Space.World);

                gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x + BubbleSize,
                gameObject.transform.localScale.y + BubbleSize,
                gameObject.transform.localScale.z
                );
            }
        }

        //2の処理
        if (this.gameObject.CompareTag("2"))
        {
            //3と衝突した場合
            if (other.gameObject.tag == "3")
            {
                Destroy(Bubble3);
                transform.Translate(new Vector3(0, -BubbleSize, 0), Space.World);

                gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x + BubbleSize,
                gameObject.transform.localScale.y + BubbleSize,
                gameObject.transform.localScale.z
                );

                DeathBubble3 = true;
            }
        }
    }

    //ゲッター
    public static bool GetDB3Flag()
    {
        return DeathBubble3;
    }


    // Update is called once per frame
    void Update()
    {
        //stopFlgを取得
        BubbleStopFlg=SpinOperation.GetstopFlg();

        //スペースを押したときの処理（１回きり）
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
        //泡の上昇部分の処理
        if (floatflag != 0)
        {
            Rigidbody rb = this.transform.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, FloatAcceleration, 0);
        }
    }
}
