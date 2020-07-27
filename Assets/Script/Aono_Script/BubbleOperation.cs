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

    bool floatflag = false;//泡の浮き沈みのフラグ（bool型にしろよ）
    // プロパティっていうやつ
    // ゲッターとセッター作るときに便利
    public bool isFloat
    {
        get
        {
            return floatflag;
        }
    }

    private int BubbleNum = 1;//泡の合計結合数（うまく言語化できない・・・）

    public bool DeathFlg = false;

    bool BubbleStopFlg;

    public static bool DeathBubble3 = false;

    bool DB3 = false;

    bool OnBubble2 = false;
    bool OnBubble3 = false;

    int num = 0;

    public Vector3 Bub1Vec = Vector3.zero;
    public Vector3 Bub2Vec = Vector3.zero;
    public Vector3 Bub3Vec = Vector3.zero;

    Controll AButton = Controll.Aボタン;

    // 20200604追加
    // 樽の子オブジェクトを取得
    Transform taru = null;

    Transform GravityField = null;

    void Start()
    {
        Bubble = GameObject.FindGameObjectWithTag("1");
        Bubble2 = GameObject.FindGameObjectWithTag("2");
        Bubble3 = GameObject.FindGameObjectWithTag("3");

        taru = this.transform.parent.Find("taru");
    }

    public void Say()
    {
        Debug.Log("当たったときの処理");
    }

    public void OnCollisionEnter(Collision other)
    {

        Transform myTransform = this.transform;

        //1の処理
        if (this.gameObject.CompareTag("1"))
        {
            //2と衝突した場合
            if (other.gameObject.tag == "2")
            {
                Destroy(Bubble2.transform.parent.gameObject);

                //DB3 = BubbleOperation.GetDB3Flag();

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

                BubbleNum = BubbleNum + 1;
            }

            //3と衝突した場合
            if (other.gameObject.tag == "3")
            {
                Destroy(Bubble3.transform.parent.gameObject);

                //DB3 = BubbleOperation.GetDB3Flag();

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

                BubbleNum = BubbleNum + 1;
            }
        }

        //2の処理
        if (this.gameObject.CompareTag("2"))
        {
            //3と衝突した場合
            if (other.gameObject.tag == "3")
            {
                Destroy(Bubble3.transform.parent.gameObject);
                transform.Translate(new Vector3(0, -BubbleSize, 0), Space.World);

                gameObject.transform.localScale = new Vector3(
                gameObject.transform.localScale.x + BubbleSize,
                gameObject.transform.localScale.y + BubbleSize,
                gameObject.transform.localScale.z
                );

                DeathBubble3 = true;

                BubbleNum = BubbleNum + 1;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (this.gameObject.CompareTag("1"))
        {
            if (other.gameObject.tag == "2")
            {
                transform.position = Vector3.Lerp(Bub1Vec, Bub2Vec, 0.03f);
            }
            if (other.gameObject.tag == "3")
            {
                transform.position = Vector3.Lerp(Bub1Vec, Bub3Vec, 0.03f);
            }
        }

        if (this.gameObject.CompareTag("2"))
        {
            //if (other.gameObject.tag == "1")
            //{
            //    transform.position = Vector3.Lerp(Bub2Vec, Bub1Vec, 0.01f);
            //}
            if (other.gameObject.tag == "3")
            {
                transform.position = Vector3.Lerp(Bub1Vec, Bub3Vec, 0.03f);
            }
        }
    }

    //フラグのゲッター
    public bool GetDB3Flag
    {
        get
        {
            return DeathBubble3;
        }
    }


    //泡の結合数のプロパティ
    public int GetBubbleNum
    {
        get {
            return BubbleNum;
        }
        private set {
            BubbleNum = value;
        }

    }

    // Update is called once per frame
    void Update()
    {
        Transform myTransform = this.transform;

        if (Bubble2 != null) { OnBubble2 = true; }
        else { OnBubble2 = false; }
        if (Bubble3 != null) { OnBubble3 = true; }
        else { OnBubble3 = false; }

        Bub1Vec = Bubble.transform.position;
        if (OnBubble2 == true) { Bub2Vec = Bubble2.transform.position; }
        if (OnBubble3 == true) { Bub3Vec = Bubble3.transform.position; }

        if (OnBubble2 == false && OnBubble3 == false && num==0)
        {
            Destroy(gameObject.transform.Find("GravityField").gameObject);
            num += 1;
        }

        //stopFlgを取得
        BubbleStopFlg = SpinOperation.GetstopFlg();

        //スペースを押したときの処理（１回きり）
        //Aボタンを押した
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(AButton.ToString())) && !floatflag)
        {
            floatflag = !floatflag;

            // ここで音を鳴らす
            SEManager.Instance.Play("SE/Bubble_Birth");

            taru.GetComponent<Animation>().Play("taru_open");
        }

        if(floatflag && taru != null && !taru.GetComponent<Animation>().isPlaying)
        {
            Destroy(taru.gameObject);
            taru = null;
        }

        if (BubbleStopFlg == true)
        {
            FloatAcceleration = StopAcceleration;
           // Debug.Log("StageStopNow");
        }
        else
        {
            FloatAcceleration = MoveAcceleration;
            //Debug.Log("StageMoveNow");
        }

    }

    void FixedUpdate()
    {
        //泡の上昇部分の処理
        if (floatflag)
        {
            Rigidbody rb = this.transform.GetComponent<Rigidbody>();
            rb.velocity = new Vector3(0, FloatAcceleration, 0);
        }
    }
}


