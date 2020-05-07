using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneComponent : MonoBehaviour
{
    [SerializeField, Header("今のシーン名")]
    public string SceneName;

    public bool SceneFlag;
    public bool GameFrag;
    static public SceneComponent instance;
    public SceneName StageNameInstance;
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
    }

    // Update is called once per frame
    void Update()
    {
        //リセットキー入力
        if(Input.GetKeyDown(KeyCode.R)&&SceneName!= "Title Scene"&&SceneName!= "StageSelect")
        {
            ResetStage();
        }

        //ステージセレクト移動
        if (Input.GetKeyDown(KeyCode.T) && SceneName != "Title Scene" && SceneName != "StageSelect")
        {
            SelectTransition();
        }

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

            //シーン読み込み
            LoadScene(SceneName);
        }

        //ゲームクリア時
        if ((SceneManager.GetActiveScene().name != "Title Scene" && SceneManager.GetActiveScene().name != "StageSelect") && GameFrag == true) 
        {
            SceneName = StageNameInstance.NextSceneName;
            SceneFlag = true;
            GameFrag = false;
        }
    }

    //シーン読み込み
    void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        SceneFlag = false;
    }
    //ステージセレクトへ
    void SelectTransition()
    {
        GameFrag = false;
        SceneName = "StageSelect";
        LoadScene("StageSelect");
    }

    //ステージのリセット
    void ResetStage()
    {
        SceneManager.LoadScene(SceneName);
    }
}
