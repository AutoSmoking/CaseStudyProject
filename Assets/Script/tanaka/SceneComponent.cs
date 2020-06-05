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
    public AudioClip CursorClip;
    public AudioClip EnterClip;

    public bool fade = false;

    public GameObject Fadeobj = null;
    public GameObject WhiteFadeobj = null;
    bool WhiteFadeTrg = false;

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
        WhiteFadeTrg = false;

        //フェード初期処理
        if (Fadeobj != null)
        {
            Fadeobj.GetComponent<Canvas>().enabled = false;
        }else
        {
            Debug.Log("Error");
        }
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
        if ((Input.GetButtonDown(Controll.Aボタン.ToString())||
                Input.GetKeyDown(KeyCode.Z) ||
                Input.GetKeyDown(KeyCode.Space)) &&
            SceneFlag == false && 
            (SceneManager.GetActiveScene().name == "Title Scene" || 
                (SceneManager.GetActiveScene().name == "StageSelect" && 
                    SceneName != "StageSelect")))
        {

            if (SceneManager.GetActiveScene().name == "Title Scene" &&
                GameObject.Find("Fade-in Canvas").GetComponent<FadeOut>().FadeTrgOut == false)
            {
                SceneFlag = false;
            }
            else
            {
                SceneFlag = true;
            }
            

        }

        if (PauseManager.ChangeScene == true &&
            Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn == false &&
            Fadeobj.GetComponent<Canvas>().enabled == false)    
        {
            PauseManager.StartStage();
            PauseManager.ChangeScene = false;
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
                }else
                {
                    WhiteFadeobj.GetComponent<FadeOut>().FadeTrgIn = false;
                }
            }

            if (Fadeobj != null && fade == false)
            {
                Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn = false;
                Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = false;

                fade = true;
                //Fadeobj.SetActive(true);
                //Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = true;

                if (WhiteFadeobj.GetComponent<FadeOut>().FadeTrgIn != false)
                {
                    FadeIn();
                }
                
                Debug.Log("a");
            }
            else if (fade == true)
            {
                if (GameObject.Find("FadeManager").GetComponent<BubbleFadeOpe>().isFadeOut == false)
                {
                    LoadScene(SceneName);
                }
            }

            // イベントにイベントハンドラーを追加
            //if (SceneManager.GetActiveScene().name == "Title Scene")
            //{
            //    SceneManager.sceneLoaded += SceneUnLoaded;
            //    if (Fadeobj != null && fade == false)
            //    {
            //        Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn = false;
            //        Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = false;

            //        fade = true;
            //        Fadeobj.SetActive(true);
            //        Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = true;
            //        Debug.Log("a");
            //    }
            //}
            //else
            //{
            //    SceneManager.sceneLoaded += SceneLoaded;
            //    //シーン読み込み
            //    LoadScene(SceneName);
            //}

        }
        //ゲームクリア時
        //if ((SceneManager.GetActiveScene().name != "Title Scene" && SceneManager.GetActiveScene().name != "StageSelect") && GameFrag == true) 
        //{
        //    SceneName = StageNameInstance.NextSceneName;
        //    SceneFlag = true;
        //    GameFrag = false;
        //}
    }

    // シーンの読み込み完了後
    //void SceneUnLoaded(Scene nextScene, LoadSceneMode mode)
    //{
    //    FadeOut();
    //    //GameObject Fade;
    //    //if (GameObject.Find("FadeManager") != null) 
    //    //{
    //    //    Fade = GameObject.Find("FadeManager");
    //    //    Fade.SetActive(true);
    //    //    Fade.GetComponent<BubbleFadeOpe>().isFadeIn = true;
    //    //}
    //}
    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {

        if (SceneName == "Title Scene")
        {
            WhiteFadeobj.GetComponent<FadeOut>().FadeTrgOut = false;
        }
        //if (SceneName != "Title Scene" && SceneName != "StageSelect")
        else
        {
            //シーン読み込み完了後
            FadeOut();
            PauseManager.GetPauseObject();
            PauseManager.StopStage();
            PauseManager.ChangeScene = true;
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
        fade = false;
        SceneManager.sceneLoaded += SceneLoaded;
        SceneFlag = false;
        SceneManager.LoadScene(name);
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

    void FadeIn()
    {
        if (Fadeobj != null)
        {
            Fadeobj.SetActive(true);
            Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = true;
        }
    }
    void FadeOut()
    {
        if (Fadeobj != null)
        {
            Fadeobj.SetActive(true);
            Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn = true;
        }
    }
}
