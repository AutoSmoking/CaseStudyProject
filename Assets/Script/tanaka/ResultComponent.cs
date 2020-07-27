using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

public class ResultComponent : MonoBehaviour
{
    static public ResultComponent instance;
    SceneComponent scene;
    public Button[] button;
    public GameObject canvas;
    public bool buttonflag = false;
    public PauseManager PauseManager;
    public bool AxisTrg = false;
    public int NowButton;
    public Animator[] buttonanimation;

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
        NowButton = 0;
        button = new Button[2];
        button[0] = GameObject.Find("Canvas/NextStage").GetComponent<Button>();
        button[1] = GameObject.Find("Canvas/StageSelect").GetComponent<Button>();
        buttonanimation = new Animator[2];
        buttonanimation[0] = button[0].GetComponent<Animator>();
        buttonanimation[1] = button[1].GetComponent<Animator>();

        //canvas = GameObject.GetComponentInChildren<GameObject>();
        canvas.SetActive(false);
        scene = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
        PauseManager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canvas.activeSelf == true)
        {

        }

        if (scene.GameFrag == true && buttonflag == false)
        {
            Debug.Log("リザルト出現");
            button[0].interactable = true;
            button[1].interactable = true;
            canvas.SetActive(true);
            buttonflag = true;
            PauseManager.ChangeScene = true;
            PauseManager.StopStage();
            buttonanimation[NowButton].SetBool("ScaleChange", true);

            List<string> a = SEManager.Instance.GetCurrentAudioNames();
            for (int i = 0; i < a.Count; i++)
            {
                if (a[i] == "SE_GoalKirakira")
                {
                    Debug.Log("OK");
                    SEManager.Instance.FadeOut(a[i]);
                }
                else
                {
                    Debug.Log("NO");
                }
            }

        }

        if (scene.GameFrag == true && buttonflag == true)
        {
            //移動
            //if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= -1) && AxisTrg == false)
            if ((Input.GetAxis(Controll.十字キー上下.ToString()) >= 1) && AxisTrg == false)
                {
                    AxisTrg = true;
                NowButton--;
                if (NowButton < 0)
                {
                    NowButton = 0;
                }
                else
                {
                    SEManager.Instance.Play(PauseManager.Scene.EnterClip);
                }
            }
            //if ((Input.GetAxis(Controll.十字キー左右.ToString()) >= 1) && AxisTrg == false)
            if ((Input.GetAxis(Controll.十字キー上下.ToString()) <= -1) && AxisTrg == false)
            {
                AxisTrg = true;
                NowButton++;
                if (NowButton > 1)
                {
                    NowButton = 1;
                }
                else
                {
                    SEManager.Instance.Play(PauseManager.Scene.EnterClip);
                }
            }
            //if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= 0.5 &&
            //    Input.GetAxis(Controll.十字キー左右.ToString()) >= -0.5) &&
            //    AxisTrg)
            if ((Input.GetAxis(Controll.十字キー上下.ToString()) <= 0.5 &&
                Input.GetAxis(Controll.十字キー上下.ToString()) >= -0.5) &&
                AxisTrg)
            {
                AxisTrg = false;
            }

            //ボタンセット
            button[NowButton].Select();

            for (int i = 0; i < buttonanimation.Length; i++)
            {
                buttonanimation[i].SetBool("ScaleChange", false);
            }
            buttonanimation[NowButton].SetBool("ScaleChange", true);

        }
    }

    public void NextStage()
    {
        button[0].interactable = false;
        button[1].interactable = false;
        Debug.Log("ボタンOFF");

        SEManager.Instance.Play(PauseManager.Scene.EnterClip);
        scene.SceneFlag = true;
        scene.GameFrag = false;
        scene.SceneName = scene.StageNameInstance.NextSceneName;
    }

    public void TitleMenu()
    {
        button[0].interactable = false;
        button[1].interactable = false;
        Debug.Log("ボタンOFF");

        SEManager.Instance.Play(PauseManager.Scene.EnterClip);
        scene.SceneFlag = true;
        scene.GameFrag = false;
        scene.SelectTransition();

    }

    public void ResultScreenOff()
    {
        NowButton = 0;
        scene.GameFrag = false;
        buttonflag = false;
        canvas.SetActive(false);
    }
}
