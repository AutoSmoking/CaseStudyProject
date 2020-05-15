using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PauseComponent))]
public class SceneComponent : MonoBehaviour
{
    [SerializeField, Header("今のシーン名")]
    public string SceneName;

    public bool SceneFlag;

    [SerializeField, Header("ゴール時にtrueにする")]
    public bool GameFrag;
    static public SceneComponent instance;
    public SceneName StageNameInstance;
    //public bool PauseFlag = false;
    public PauseManager PauseManager;
    void Awake()
    {
        if (instance == null)
        {

            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        SceneFlag = false;
        GameFrag = false;
        StageNameInstance = instance.GetComponent<SceneName>();
        PauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (SceneName != "Title Scene" && SceneName != "StageSelect")
        //{

        //}

        ////リセットキー入力
        //if (Input.GetKeyDown(KeyCode.R) && SceneName != "Title Scene" && SceneName != "StageSelect" && GameFrag == false) 
        //{
        //    ResetStage();
        //}

        ////ステージセレクト移動
        //if (Input.GetKeyDown(KeyCode.T) && SceneName != "Title Scene" && SceneName != "StageSelect" && GameFrag == false) 
        //{
        //    SelectTransition();
        //}

            //キー入力(タイトル、ステージセレクト時)
            if ((Input.GetKeyDown(KeyCode.Z) ||
            Input.GetKeyDown(KeyCode.Space)) && SceneFlag == false && 
            (SceneManager.GetActiveScene().name == "Title Scene" || 
            (SceneManager.GetActiveScene().name == "StageSelect" && 
            SceneName != "StageSelect")))
        {

            SceneFlag = true;
        }

        //シーン名の読み込み
        if (SceneFlag == true)
        {
            if (SceneManager.GetActiveScene().name == "Title Scene")
            {
                SceneName = "StageSelect";
            }
            else if (SceneManager.GetActiveScene().name == "StageSelect")
            {
                if (SceneName != "Title Scene")
                {
                    GameFrag = false;
                }
            }

            // イベントにイベントハンドラーを追加
            SceneManager.sceneLoaded += SceneLoaded;
            //シーン読み込み
            LoadScene(SceneName);
        }

        //ゲームクリア時
        //if ((SceneManager.GetActiveScene().name != "Title Scene" && SceneManager.GetActiveScene().name != "StageSelect") && GameFrag == true) 
        //{
        //    SceneName = StageNameInstance.NextSceneName;
        //    SceneFlag = true;
        //    GameFrag = false;
        //}
    }

    // イベントハンドラー（イベント発生時に動かしたい処理）
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {
        if (SceneName != "Title Scene" && SceneName != "StageSelect")
        {
            //シーン読み込み完了後
            PauseManager.GetPauseObject();

            //foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
            //{
            //    // シーン上に存在するオブジェクトならば処理.
            //    if (obj.activeInHierarchy && obj.name != "SceneManager") 
            //    {
            //       // obj.AddComponent<PauseComponent>();
            //    }
            //}
        }
    }
    //シーン読み込み
    void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        SceneFlag = false;
    }
    //ステージセレクトへ
    public void SelectTransition()
    {
        GameFrag = false;
        SceneName = "StageSelect";
        LoadScene("StageSelect");
    }

    //ステージのリセット
    public void ResetStage()
    {
        SceneManager.LoadScene(SceneName);
    }
}
