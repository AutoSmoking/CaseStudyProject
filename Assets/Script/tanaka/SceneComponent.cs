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

    [SerializeField, Header("ゲームオーバー時にtrueにする")]
    public bool GameOverFlag;

    static public SceneComponent instance;
    public SceneName StageNameInstance;
    //public bool PauseFlag = false;
    public PauseManager PauseManager;
    public AudioClip TitleAndSelectBGM;
    public AudioClip EnterClip;

    public bool fade = false;

    [SerializeField, Header("フェードアニメーション")]
    public GameObject Fadeobj_In = null;
    public GameObject Fadeobj_Out = null;
    public FadeAnim FadeAni_In;
    //public FadeAnim FadeAni_Out;
    public BubbleFadeOpe FadeAni_Out;
    public GameObject WhiteFadeobj = null;
    public bool WhiteFadeTrg = false;
    public bool AllFade = false;

    public bool bgm = false;

    ResultComponent Result;

    public bool TitleFlag = false;
    public bool FadeObjTrg = false;
    //public bool FadeInTrg = false;

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
        GameOverFlag = false;
        StageNameInstance = instance.GetComponent<SceneName>();
        PauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        WhiteFadeTrg = false;
        Result = GameObject.Find("ResultManager").GetComponent<ResultComponent>();

        BGMManager.Instance.Play(TitleAndSelectBGM);

        //フェード初期処理
        if (Fadeobj_In != null&& Fadeobj_Out != null)
        {
            //Fadeobj.GetComponent<Canvas>().enabled = false;
            Fadeobj_Out.GetComponent<Canvas>().enabled = false;
            FadeAni_In = Fadeobj_In.GetComponent<FadeAnim>();
            //FadeAni_Out = Fadeobj_Out.GetComponent<FadeAnim>();
            FadeAni_Out = Fadeobj_Out.GetComponent<BubbleFadeOpe>();
        }
        else
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

        if(GetSceneNow() != "Title Scene" && GetSceneNow() != "StageSelect")
        {
            //if (PauseManager.BubbleChack())
            if(GameOverFlag)
            {
                ResetStage();
            }
        }

        //if (FadeInTrg && FadeObjTrg)
        //{
        //    FadeOut();
        //}
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
            TitleFlag&&
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
        //if (PauseManager.ChangeScene == true &&
        //    Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn == false &&
        //    Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut == false &&
        //    Fadeobj.GetComponent<Canvas>().enabled == false && !GameFrag)
        if (PauseManager.ChangeScene == true &&
            FadeAni_In.isFadeIn == false &&
            FadeAni_In.isFadeOut == false &&
            FadeAni_Out.isFadeIn == false &&
            FadeAni_Out.isFadeOut == false &&
            !GameFrag)   
        {
            Debug.Log("StopPausecon");
            PauseManager.ChangeScene = false;
            PauseManager.ResetNow = false;
            PauseManager.StartStage();
        }

        if (fade)
        {
            //if (Fadeobj.GetComponent<Canvas>().enabled == false)
            if (FadeAni_In.isFadeIn == true && FadeAni_Out.isFadeOut == false) 
                {
                Debug.Log("false");
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
                    //if (Fadeobj != null && fade == false && !WhiteFadeTrg)
                    if (fade == false && !WhiteFadeTrg)
                    {
                        if (WhiteFadeobj.GetComponent<FadeOut>().FadeTrgIn)
                        {
                            //Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn = false;
                            //Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = false;
                            FadeAni_In.isFadeIn = false;
                            FadeAni_Out.isFadeOut = false;
                            fade = true;
                            FadeIn();
                            Debug.Log("1");
                        }
                    }
                }
                //セレクトからタイトル
                else if (!WhiteFadeTrg)
                {
                    SceneName = "Title Scene";
                    WhiteFadeobj.GetComponent<FadeOut>().ReSetFadeIn();
                    WhiteFadeTrg = true;
                }
            }
            //ステージ
            else
            {
                GameFrag = false;
                //if (Fadeobj != null && fade == false && !WhiteFadeTrg)
                if (fade == false && !WhiteFadeTrg)
                {
                    //Fadeobj.GetComponent<BubbleFadeOpe>().isFadeIn = false;
                    //Fadeobj.GetComponent<BubbleFadeOpe>().isFadeOut = false;
                    FadeAni_In.isFadeIn = false;
                    FadeAni_Out.isFadeOut = false;
                    fade = true;
                    FadeIn();
                    Debug.Log("2");
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
                if (FadeAni_Out.isFadeOut == false)
                {
                    LoadScene(SceneName);
                }
            } else if (WhiteFadeTrg) 
            {
                if (WhiteFadeobj.GetComponent<FadeOut>().FadeTrgIn)
                {
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
        Result.ResultScreenOff();
        PauseManager.PauseScreenOff();

        List<string> a = SEManager.Instance.GetCurrentAudioNames();
        for (int i = 0; i < a.Count; i++)
        {
            if (a[i]=="SE_GoalKirakira")
            {
                Debug.Log("OK");
                SEManager.Instance.Stop(a[i]);
            }
            else
            {
                Debug.Log("NO");
            }
        }
        if (SceneName == "Title Scene" || SceneName == "StageSelect")
        {
            if (WhiteFadeTrg)
            {
                WhiteFadeobj.GetComponent<FadeOut>().ReSetFadeOut();
                //WhiteFadeTrg = false;
            }else if (fade)
            {
                //fade = false;
                Debug.Log("ai");
                //FadeInTrg = true;
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
            Debug.Log("a");
            //シーン読み込み完了後
            //fade = false;
            //FadeInTrg = true;
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
        GameOverFlag = false;
        //SceneManager.LoadScene(SceneName);
    }

    void FadeIn()
    {
        if (Fadeobj_In != null)
        {
            FadeObjTrg = true;
            Fadeobj_Out.SetActive(true);
            FadeAni_Out.isFadeOut = true;
            FadeAni_Out.isFadeIn = false;
        }
    }
    void FadeOut()
    {
        if (Fadeobj_Out != null)
        {
            Debug.Log("out");
            FadeObjTrg = false;
            //FadeInTrg = false;
            FadeAni_Out.isFadeOut = false;
            FadeAni_Out.isFadeIn = false;
            Fadeobj_Out.GetComponent<Canvas>().enabled = false;
            Fadeobj_Out.SetActive(false);

            Fadeobj_In.SetActive(true);
            FadeAni_In.isFadeIn = true;
        }
    }
    bool IsFadeEnd()
    {
        if (FadeAni_In.isFadeIn == false && FadeAni_Out.isFadeOut == false && FadeObjTrg == false)  
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
