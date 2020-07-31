using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

public class PauseManager : MonoBehaviour
{
    [SerializeField, Header("「Stage」(SpinOparationのあるオブジェクト)のレイヤー番号")]
    public int StageLayerNumber;

    [SerializeField, Header("「Bubble」のレイヤー番号")]
    public int BubbleLayerNumber;

    [SerializeField, Header("レイヤーがStageのオブジェクト")]
    public List<GameObject> StageLayerObjList = new List<GameObject>();

    [SerializeField, Header("Pause対象のオブジェクト")]
    public List<GameObject> TargetObj = new List<GameObject>();   // ポーズ対象のスクリプト

    Behaviour[] pauseBehavs = null;
    GameObject[] obj = null;
    static public PauseManager instance;
    public bool PauseFlag = false;
    public GameObject canvas;
    public GameObject Explanation1;
    public GameObject Explanation2;
    public Button[] button;
    public Button[] button_Ex1;
    public Button[] button_Ex2;
    public Animator[] Ani;
    public Animator[] Ani_Ex1;
    public Animator[] Ani_Ex2;

    public SceneComponent Scene;
    public bool ChangeScene = false;
    bool AxisTrg = false;
    public int NowButton = 0;
    public bool ResetNow = false;
    public List<GameObject> BubbleList;
    private int SetNumber;
    private int pattern;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            //Debug.Log("set");
            instance = GetComponent<PauseManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

        pattern = 0;

        PauseFlag = false;
        canvas = transform.Find("Canvas").gameObject;
        Explanation1 = transform.Find("Explanation1").gameObject;
        Explanation2 = transform.Find("Explanation2").gameObject;
        Explanation1.SetActive(false);
        Explanation2.SetActive(false);

        button = new Button[5];
        button[0] = transform.Find("Canvas/Back").GetComponent<Button>();
        button[1] = transform.Find("Canvas/Reset").GetComponent<Button>();
        button[2] = transform.Find("Canvas/StageSelect").GetComponent<Button>();
        button[3] = transform.Find("Canvas/0").GetComponent<Button>();
        button[4] = transform.Find("Canvas/1").GetComponent<Button>();

        button_Ex1 = new Button[2];
        button_Ex2 = new Button[2];

        button_Ex1[0] = transform.Find("Explanation1/0").GetComponent<Button>();
        button_Ex1[1] = transform.Find("Explanation1/1").GetComponent<Button>();
        button_Ex2[0] = transform.Find("Explanation2/0").GetComponent<Button>();
        button_Ex2[1] = transform.Find("Explanation2/1").GetComponent<Button>();

        Ani = new Animator[5];
        Ani[0] = button[0].GetComponent<Animator>();
        Ani[1] = button[1].GetComponent<Animator>();
        Ani[2] = button[2].GetComponent<Animator>();
        Ani[3] = button[3].GetComponent<Animator>();
        Ani[4] = button[4].GetComponent<Animator>();

        Ani_Ex1 = new Animator[2];
        Ani_Ex1[0] = button_Ex1[0].GetComponent<Animator>();
        Ani_Ex1[1] = button_Ex1[1].GetComponent<Animator>();
        Ani_Ex2 = new Animator[2];
        Ani_Ex2[0] = button_Ex2[0].GetComponent<Animator>();
        Ani_Ex2[1] = button_Ex2[1].GetComponent<Animator>();

        canvas.SetActive(false);
        Scene = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown(Controll.HOMEボタン.ToString()) || Input.GetKeyDown(KeyCode.M)) &&
            Scene.GetSceneNow() != "Title Scene" && Scene.GetSceneNow() != "StageSelect" && !ChangeScene && !Scene.GameFrag && !ResetNow)   
        {
            SEManager.Instance.Play(Scene.EnterClip);

            if (PauseFlag == false)
            {
                NowButton = 0;
                PauseFlag = true;
                StopStage();
                canvas.SetActive(true);
                button[NowButton].Select();
                Ani[NowButton].SetBool("ScaleChange", true);
                //Debug.Log("Selected");
            }
            else
            {
                PauseFlag = false;
                StartStage();
                canvas.SetActive(false);
            }
        }

        if (PauseFlag)
        {
            //AxisTrg = false;

            //移動
            if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= -1) && AxisTrg == false)
            {
                AxisTrg = true;
                if (pattern == 0)
                {
                    if (NowButton != 3)
                    {
                        SetNumber = NowButton;
                        NowButton = 4;
                        SEManager.Instance.Play(Scene.EnterClip);
                    }
                    else
                    {
                        NowButton = SetNumber;
                        SEManager.Instance.Play(Scene.EnterClip);
                    }
                }
                else
                {
                    NowButton = 1;
                    SEManager.Instance.Play(Scene.EnterClip);
                }
            }
            if ((Input.GetAxis(Controll.十字キー左右.ToString()) >= 1) && AxisTrg == false)
            {
                AxisTrg = true;
                if (pattern == 0)
                {
                    if (NowButton != 4)
                    {
                        SetNumber = NowButton;
                        NowButton = 3;
                        SEManager.Instance.Play(Scene.EnterClip);
                    }
                    else
                    {
                        NowButton = SetNumber;
                        SEManager.Instance.Play(Scene.EnterClip);
                    }
                }
                else
                {
                    NowButton = 0;
                    SEManager.Instance.Play(Scene.EnterClip);
                }
            }
            if (pattern == 0)
            {

                if ((Input.GetAxis(Controll.十字キー上下.ToString()) <= -1) && AxisTrg == false)
                {
                    AxisTrg = true;
                    NowButton++;
                    if (NowButton >= 3)
                    {
                        NowButton = 2;
                    }
                    else
                    {
                        SEManager.Instance.Play(Scene.EnterClip);
                    }
                    //if (NowButton == 1 || NowButton == 0) 
                    //{
                    //    NowButton = 2;
                    //    SEManager.Instance.Play(Scene.EnterClip);
                    //}
                }
                if ((Input.GetAxis(Controll.十字キー上下.ToString()) >= 1) && AxisTrg == false)
                {
                    AxisTrg = true;
                    NowButton--;
                    if (NowButton < 0)
                    {
                        NowButton = 0;
                    }
                    else if (NowButton == 3)
                    {
                        NowButton = 0;
                        SEManager.Instance.Play(Scene.EnterClip);
                    }
                    else
                    {
                        SEManager.Instance.Play(Scene.EnterClip);
                    }
                    //if (NowButton == 2 || NowButton == 1) 
                    //{
                    //    NowButton = 0;
                    //    SEManager.Instance.Play(Scene.EnterClip);
                    //}
                }
            }
            if (pattern == 0)
            {
                button[NowButton].Select();

                for (int i = 0; i < 5; i++)
                {
                    Ani[i].SetBool("ScaleChange", false);
                }

                Ani[NowButton].SetBool("ScaleChange", true);
            }else if (pattern == 1)
            {
                button_Ex1[NowButton].Select();

                for (int i = 0; i < 2; i++)
                {
                    Ani_Ex1[i].SetBool("ScaleChange", false);
                }

                Ani_Ex1[NowButton].SetBool("ScaleChange", true);
            }
            else if (pattern == 2)
            {
                button_Ex2[NowButton].Select();

                for (int i = 0; i < 2; i++)
                {
                    Ani_Ex2[i].SetBool("ScaleChange", false);
                }

                Ani_Ex2[NowButton].SetBool("ScaleChange", true);
            }

        }

        if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= 0.5 &&
            Input.GetAxis(Controll.十字キー左右.ToString()) >= -0.5 &&
            Input.GetAxis(Controll.十字キー上下.ToString()) <= 0.5 &&
            Input.GetAxis(Controll.十字キー上下.ToString()) >= -0.5) &&
            AxisTrg)
        {
            AxisTrg = false;
        }

        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    GameObject[] go = GameObject.FindObjectsOfType<GameObject>();
        //    foreach (GameObject obj in go)
        //    {
        //        if (obj.layer == 9 || obj.layer == 8)
        //        {
        //            TargetObj.Add(obj);
        //        }
        //    }
        //}
    }

    public void GetPauseObject()
    {
        //Debug.Log("GetPauseObject");
        StageLayerObjList.Clear();
        TargetObj.Clear();
        BubbleList.Clear();
        PauseFlag = false;
        //全てのオブジェクト取得
        GameObject[] AllObj = GameObject.FindObjectsOfType<GameObject>();

        //全てのオブジェクトの中からレイヤーが「9:Staeg」の物のみ取得
        foreach(GameObject obj in AllObj)
        {
            if (obj.layer == BubbleLayerNumber)
            {
                TargetObj.Add(obj);
                BubbleList.Add(obj);
            }

            if (obj.layer == StageLayerNumber) 
            {
                StageLayerObjList.Add(obj);
                TargetObj.Add(obj);
            }

            if (obj.tag == "fish")
            {
                TargetObj.Add(obj);
            }
        }

        //SpinOparationのSpinSpeedを0にしてステージを止める

        //Stageのレイヤーの中にあるStageObjの取得
        //foreach(GameObject obj in StageLayerObjList)
        //{
        //    if (obj.GetComponent<SpinOperation>() != null)
        //    {
        //        foreach(GameObject targetobj in obj.GetComponent<SpinOperation>().StageObj)
        //        {
        //            TargetObj.Add(targetobj);
        //        }
        //    }
        //}

        //Pause対象のオブジェクトにPauseComponentの付与
        foreach (GameObject obj in TargetObj)
        {
            if (obj.GetComponent<PauseComponent>() == null)
            {
                obj.AddComponent<PauseComponent>();
            }
        }
    }

    public void PauseMenu(int num)
    {
        PauseFlag = false;

        switch (num)
        {
            case 0:
                StartStage();
                PauseScreenOff();
                break;

            case 1:
                Scene.ResetStage();
                PauseScreenOff();
                ResetNow = true;
                break;

            case 2:
                Scene.SelectTransition();
                PauseScreenOff();
                ResetNow = true;
                break;
        }

    }
    //ステージ上のオブジェクトを止める
    public void StopStage()
    {
        foreach (GameObject obj in TargetObj)
        {
            //if (obj.GetComponent<SpinOperation>() != null)
            //{
            //    obj.GetComponent<SpinOperation>().SpinSpeed = 0.0f;
            //}
            if (obj!=null&&(PauseFlag == true||ChangeScene==true) && 
                obj.GetComponent<PauseComponent>() != null)
            {
                obj.GetComponent<PauseComponent>().OnPause();
                //Debug.Log("on");
            }
        }
    }

    //ステージ上のオブジェクトの停止解除
    public void StartStage()
    {
        foreach (GameObject obj in TargetObj)
        {
            //if (obj.GetComponent<SpinOperation>() != null)
            //{
            //    obj.GetComponent<SpinOperation>().SpinSpeed = 0.0f;
            //}
            if (obj != null && (PauseFlag == false && ChangeScene == false) &&
                obj.GetComponent<PauseComponent>() != null)
            {
                obj.GetComponent<PauseComponent>().OnResume();
                //Debug.Log("off");
            }
        }
    }

    public void PauseScreenOff()
    {
        PauseFlag = false;
        ResetNow = false;
        canvas.SetActive(false);
    }

    //ゲームオーバー判定
    public bool BubbleChack()
    {
        for(int i = 0; i < BubbleList.Count; i++)
        {
            if (BubbleList[i] != null)
            {
                return false;
            }
        }

        Debug.Log("Bubble=0");

        ResetNow = true;
        return true;
    }

    public void PauseChange1()
    {
        if (NowButton == 0)
        {
            NowButton = 3;
        }
        else if (NowButton == 1)
        {
            NowButton = 4;
        }
        SEManager.Instance.Play(Scene.EnterClip);
        pattern = 0;
        canvas.SetActive(true);
        Explanation1.SetActive(false);
        Explanation2.SetActive(false);
    }
    public void PauseChange2()
    {
        if (NowButton == 3)
        {
            NowButton = 0;
        }else if (NowButton == 4)
        {
            NowButton = 1;
        }
        SEManager.Instance.Play(Scene.EnterClip);
        pattern = 1;
        canvas.SetActive(false);
        Explanation1.SetActive(true);
        Explanation2.SetActive(false);
    }
    public void PauseChange3()
    {
        if (NowButton == 3)
        {
            NowButton = 0;
        }
        else if (NowButton == 4)
        {
            NowButton = 1;
        }
        SEManager.Instance.Play(Scene.EnterClip);
        pattern = 2;
        canvas.SetActive(false);
        Explanation1.SetActive(false);
        Explanation2.SetActive(true);
    }
}
