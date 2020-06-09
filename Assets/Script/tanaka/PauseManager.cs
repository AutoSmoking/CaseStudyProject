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
    public Button[] button;
    public SceneComponent Scene;
    public bool ChangeScene = false;
    bool AxisTrg = false;
    public int NowButton = 0;

    public List<GameObject> BubbleList;
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

        PauseFlag = false;
        canvas = transform.Find("Canvas").gameObject;
        button= new Button[3];
        button[0] = transform.Find("Canvas/Reset").GetComponent<Button>();
        button[1] = transform.Find("Canvas/Back").GetComponent<Button>();
        button[2] = transform.Find("Canvas/StageSelect").GetComponent<Button>();
        canvas.SetActive(false);
        Scene = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown(Controll.HOMEボタン.ToString()) || Input.GetKeyDown(KeyCode.P)) &&
            Scene.GetSceneNow() != "Title Scene" && Scene.GetSceneNow() != "StageSelect" && !ChangeScene&&!Scene.GameFrag)  
        {
            SEManager.Instance.Play(Scene.EnterClip);

            if (PauseFlag == false)
            {
                NowButton = 0;
                PauseFlag = true;
                StopStage();
                canvas.SetActive(true);
                button[NowButton].Select();
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
                if (NowButton != 1)
                {
                    NowButton = 1;
                    SEManager.Instance.Play(Scene.EnterClip);
                }
            }
            if ((Input.GetAxis(Controll.十字キー左右.ToString()) >= 1) && AxisTrg == false)
            {
                AxisTrg = true;
                if (NowButton == 1)
                {
                    NowButton = 0;
                    SEManager.Instance.Play(Scene.EnterClip);
                }
            }
            if ((Input.GetAxis(Controll.十字キー上下.ToString()) <= -1) && AxisTrg == false)
            {
                AxisTrg = true;
                if (NowButton == 1 || NowButton == 0) 
                {
                    NowButton = 2;
                    SEManager.Instance.Play(Scene.EnterClip);
                }
            }
            if ((Input.GetAxis(Controll.十字キー上下.ToString()) >= 1) && AxisTrg == false)
            {
                AxisTrg = true;
                if (NowButton == 2 || NowButton == 1) 
                {
                    NowButton = 0;
                    SEManager.Instance.Play(Scene.EnterClip);
                }
            }

            button[NowButton].Select();
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
                break;

            case 1:
                Scene.ResetStage();
                break;

            case 2:
                Scene.SelectTransition();
                break;
        }

        PauseScreenOff();
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

        canvas.SetActive(false);
    }

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
        return true;
    }
}
