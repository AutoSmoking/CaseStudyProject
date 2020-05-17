using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

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
    public Button button;
    public SceneComponent Scene;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            Debug.Log("set");
            instance = GetComponent<PauseManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

        PauseFlag = false;
        canvas = transform.Find("Canvas").gameObject;
        button = transform.Find("Canvas/Back").GetComponent<Button>();
        canvas.SetActive(false);
        Scene = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (PauseFlag == false)
            {
                StopStage();
                PauseFlag = true;
                canvas.SetActive(true);
                button.Select();

            }
            else
            {
                StartStage();
                PauseFlag = false;
                canvas.SetActive(false);
            }
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
        Debug.Log("GetPauseObject");
        StageLayerObjList.Clear();
        TargetObj.Clear();
        PauseFlag = false;
        //全てのオブジェクト取得
        GameObject[] AllObj = GameObject.FindObjectsOfType<GameObject>();

        //全てのオブジェクトの中からレイヤーが「9:Staeg」の物のみ取得
        foreach(GameObject obj in AllObj)
        {
            if (obj.layer == BubbleLayerNumber)
            {
                TargetObj.Add(obj);
            }

            if (obj.layer == StageLayerNumber) 
            {
                StageLayerObjList.Add(obj);
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

        PauseFlag = false;
        canvas.SetActive(false);

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
            if (PauseFlag == false && obj.GetComponent<PauseComponent>() != null)
            {
                obj.GetComponent<PauseComponent>().OnPause();
                Debug.Log("on");
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
            if (PauseFlag == true && obj.GetComponent<PauseComponent>() != null)
            {
                obj.GetComponent<PauseComponent>().OnResume();
                Debug.Log("off");
            }
        }
    }
}
