using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalClearProductionOperater : MonoBehaviour
{
    [SerializeField, Header("ポーズ表示ボタン")]
    KeyCode Key;

    [SerializeField, Header("何秒後にポーズ表示するか")]
    int second = 0;

    [SerializeField, Header("財宝")]
    GameObject Zaihou = null;

    [SerializeField, Header("エフェクト")]
    GameObject Effect = null;

    public PauseManager pause = null;

    public GameObject Goal = null;
    public GameObject SceneManager = null;
    public GameObject Bubble = null;
    bool CreateFlag = false;
    
    bool GameFlg = false;

    public Animation ani = null;

    // Start is called before the first frame update
    void Start()
    {
        //PauseManagerを取得し、ステージを止める
        pause = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        pause.ChangeScene = true;
        pause.StopStage();

        //ゴール位置の取得
        Vector3 pos = Goal.transform.position;
        Quaternion quaternion = Goal.transform.rotation;

        //財宝とエフェクトの作成
        Zaihou = (GameObject)Instantiate(Zaihou, pos, quaternion);
        Zaihou.transform.parent = Goal.transform;
        Goal.GetComponent<Rigidbody>().isKinematic = true;
        Effect = (GameObject)Instantiate(Effect, pos, quaternion);
        Effect.transform.parent = Zaihou.transform;

        ani.Play("takarabako_open");

        Debug.Log("CreateGealClear");
    }

    // Update is called once per frame63
    void Update()
    {
        pause.ChangeScene = true;
        pause.StopStage();
        //second秒経過後実行
        Invoke("ChangeGameFlagTrue", second);
    }

    void ChangeGameFlagTrue()
    {
        SceneManager.GetComponent<SceneComponent>().GameFrag = true;
    }

    //Initを作る側で呼び出して値を入れる
    public void Init(Animation a,GameObject b,GameObject g,GameObject s)
    {
        ani = a;
        Bubble = b;
        Goal = g;
        SceneManager = s;
        Debug.Log("SetObjects");
    }
}
