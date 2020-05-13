using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    public bool a = false;
    public SpinOperation SpinOp;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {

            instance = GetComponent<PauseManager>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {

            foreach (GameObject obj in TargetObj)
            {
                if (a == false && obj.GetComponent<PauseComponent>() != null)
                {
                    obj.GetComponent<PauseComponent>().OnPause();
                    Debug.Log("on");
                }
                else if (a == true && obj.GetComponent<PauseComponent>() != null)
                {
                    obj.GetComponent<PauseComponent>().OnResume();
                    Debug.Log("off");
                }
            }

            if (a == true)
            {
                a = false;
            }
            else
            {
                a = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            GameObject[] go = GameObject.FindObjectsOfType<GameObject>();
            foreach (GameObject obj in go)
            {
                if (obj.layer == 9 || obj.layer == 8)
                {
                    TargetObj.Add(obj);
                }
            }
        }
    }

    public void GetPauseObject()
    {
        Debug.Log("GetPauseObject");
        StageLayerObjList.Clear();
        TargetObj.Clear();
        a = false;
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

        //Stageのレイヤーの中にあるStageObjの取得
        foreach(GameObject obj in StageLayerObjList)
        {
            if (obj.GetComponent<SpinOperation>() != null)
            {
                foreach(GameObject targetobj in obj.GetComponent<SpinOperation>().StageObj)
                {
                    TargetObj.Add(targetobj);
                }
            }
        }

        //Pause対象のオブジェクトにPauseComponentの付与
        foreach (GameObject obj in TargetObj)
        {
            if (obj.GetComponent<PauseComponent>() == null)
            {
                obj.AddComponent<PauseComponent>();
            }
        }
    }
}
