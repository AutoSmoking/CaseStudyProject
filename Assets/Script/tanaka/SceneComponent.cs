using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using KanKikuchi.AudioManager;

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
    public AudioClip TitleAndSelectBGM;
    public AudioClip EnterClip;

    public bool fade = false;

    public GameObject Fadeobj = null;
    public GameObject WhiteFadeobj = null;
    public bool WhiteFadeTrg = false;
    public bool AllFade = false;

    public bool bgm = false;

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

        BGMManager.Instance.Play(TitleAndSelectBGM);

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
        if (GameFrag && !PauseManager.ChangeScene) 
        {
            Debug.Log("GoalSet");
            GoalFlag();
        }
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

        //キー入力
        if ((Input.GetButtonDown(Controll.Aボタン.ToString())||
                Input.GetKeyDown(KeyCode.Z) ||
                Input.GetKeyDown(KeyCode.Space)) &&
            SceneFlag == false && AllFade&&
            (GetSceneNow() == "Title Scene" || 
                (GetSceneNow() == "StageSelect" && 
                    SceneName != "StageSelect")))
        {
            Debug.Log("space");
            if (GetSceneNow() == "Title Scene" &&
                GameObject.Find("Fade-in Canvas").GetComponent<FadeOut>().FadeTrgOut == false)
            {
                SceneFlag = false;
            }
            else
            {
                if (GetSceneNow() == "Title Scene")
                {
                    SEManager.Instance.Play(EnterClip);
                }
                SceneFlag = true;
            }


        }

        if (IsFadeEnd() && WhiteFadeobj.GetComponent<FadeOut>().IsEnd())
        {
            AllFade = true;
        }else
        {
            AllFade = false;
        }

        //シーン読み込み中のロック解除
        if (PauseManager.ChangeScene == true &&
            Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn == false &&
            Fadeobj.GetComponent<Canvas>().enabled == false&&!GameFrag)   
        {
            Debug.Log("StopPausecon");
            PauseManager.ChangeScene = false;
            PauseManager.StartStage();
        }

        if (fade)
        {
            if(Fadeobj.GetComponent<Canvas>().enabled == false)
            {
                fade = false;
            }
        }
        //シーン名の読み込み
        if (SceneFlag == true)
        {
            //タイトルからセレクト
            if (GetSceneNow() == "Title Scene" && !WhiteFadeTrg)
            {
                SceneName = "StageSelect";
                WhiteFadeobj.GetComponent<FadeOut>().ReSetFadeIn();
                WhiteFadeTrg = true;
            }
            //セレクトの時
            else if (GetSceneNow() == "StageSelect")
            {
                //セレクトからステージ
                if (SceneName != "Title Scene")
                {
                    GameFrag = false;
                    if (Fadeobj != null && fade == false && !WhiteFadeTrg)
                    {
                        if (WhiteFadeobj.GetComponent<FadeOut>().FadeTrgIn)
                        {
                            Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn = false;
                            Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = false;
                            Debug.Log("FadeTrue1");

                            fade = true;
                            FadeIn();
                        }
                    }
                }
                //セレクトからタイトル
                else if (!WhiteFadeTrg)
                {
                    Debug.Log("Select~Title");
                    SceneName = "Title Scene";
                    WhiteFadeobj.GetComponent<FadeOut>().ReSetFadeIn();
                    WhiteFadeTrg = true;
                }
            }
            //ステージ
            else
            {
                GameFrag = false;
                if (Fadeobj != null && fade == false && !WhiteFadeTrg)
                {
                    Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn = false;
                    Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = false;
                    Debug.Log("FadeTrue2");
                    fade = true;
                    FadeIn();
                }
            }


            //if (Fadeobj != null && fade == false && WhiteFadeobj.GetComponent<FadeOut>().FadeTrgIn)
            //{
            //    Debug.Log("1");
            //    if (WhiteFadeobj.GetComponent<FadeOut>().FadeTrgIn)
            //    {
            //        Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn = false;
            //        Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = false;

            //        fade = true;
            //    }
            //}
            if (fade == true)
            {
                Debug.Log("2");

                if (GameObject.Find("FadeManager").GetComponent<BubbleFadeOpe>().isFadeOut == false)
                {
                    LoadScene(SceneName);
                }
            } else if (WhiteFadeTrg) 
            {
                if (WhiteFadeobj.GetComponent<FadeOut>().FadeTrgIn)
                {
                    Debug.Log("3");
                    LoadScene(SceneName);
                }
            }

            // イベントにイベントハンドラーを追加
            //if (GetSceneNow == "Title Scene")
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
        //if ((GetSceneNow != "Title Scene" && GetSceneNow != "StageSelect") && GameFrag == true) 
        //{
        //    SceneName = StageNameInstance.NextSceneName;
        //    SceneFlag = true;
        //    GameFrag = false;
        //}
    }

    void SceneLoaded(Scene nextScene, LoadSceneMode mode)
    {

        if (SceneName == "Title Scene" || SceneName == "StageSelect")
        {
            if (WhiteFadeTrg)
            {
                WhiteFadeobj.GetComponent<FadeOut>().ReSetFadeOut();
                //WhiteFadeTrg = false;
            }else if (fade)
            {
                //fade = false;
                FadeOut();
            }

            if (bgm)
            {
                BGMManager.Instance.Play(TitleAndSelectBGM);
                bgm = false;
            }
        }
        //if (SceneName != "Title Scene" && SceneName != "StageSelect")
        else
        {
            //シーン読み込み完了後
            //fade = false;
            FadeOut();
            PauseManager.GetPauseObject();
            PauseManager.ChangeScene = true;
            PauseManager.StopStage();
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
        if (GetSceneNow() != "Title Scene" && SceneName == "StageSelect")  
        {
            bgm = true;
        }
        SceneFlag = false;
        SceneManager.sceneLoaded += SceneLoaded;
        SceneManager.LoadScene(name);
    }
    //ステージセレクトへ
    public void SelectTransition()
    {
        SceneFlag = true;
        SceneName = "StageSelect";
        //LoadScene("StageSelect");
    }
    //ステージのリセット
    public void ResetStage()
    {
        SceneFlag = true;
        //SceneManager.LoadScene(SceneName);
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
    bool IsFadeEnd()
    {
        if (Fadeobj.GetComponent<Canvas>().enabled == false)
        {
            return true;
        }

        return false;
    }
    public string GetSceneNow()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void GoalFlag()
    {
        PauseManager.ChangeScene = true;
        PauseManager.StopStage();
    }
}
