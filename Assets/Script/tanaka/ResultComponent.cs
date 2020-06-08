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
            canvas.SetActive(true);
            buttonflag = true;
            PauseManager.ChangeScene = true;
            PauseManager.StopStage();
        }

        if (scene.GameFrag == true && buttonflag == true)
        {
            //移動
            if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= -1) && AxisTrg == false)
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
            if ((Input.GetAxis(Controll.十字キー左右.ToString()) >= 1) && AxisTrg == false)
            {
                AxisTrg = true;
                NowButton++;
                if (NowButton > 1)
                {
                    NowButton = 1;
                }else
                {
                    SEManager.Instance.Play(PauseManager.Scene.EnterClip);
                }
            }
            if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= 0.5 &&
                Input.GetAxis(Controll.十字キー左右.ToString()) >= -0.5 )&&
                AxisTrg)
            {
                AxisTrg = false;
            }

            //ボタンセット
            button[NowButton].Select();
        }
    }

    public void NextStage()
    {
        if (buttonflag)
        {
            SEManager.Instance.Play(PauseManager.Scene.EnterClip);
            buttonflag = false;
            scene.SceneFlag = true;
            scene.GameFrag = false;
            scene.SceneName = scene.StageNameInstance.NextSceneName;
        }
    }

    public void TitleMenu()
    {
        if (buttonflag)
        {
            SEManager.Instance.Play(PauseManager.Scene.EnterClip);
            buttonflag = false;
            scene.SceneFlag = true;
            scene.GameFrag = false;
            scene.SelectTransition();
        }
    }

    public void ResultScreenOff()
    {
        NowButton = 0;
        scene.GameFrag = false;
        canvas.SetActive(false);
    }
}
