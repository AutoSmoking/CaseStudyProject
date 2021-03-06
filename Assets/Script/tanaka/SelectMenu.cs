﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using KanKikuchi.AudioManager;


public class SelectMenu : MonoBehaviour
{
    [SerializeField, Header("1～10のステージシーン名")]
    public string[] scenenames = new string[10];
    Button button;
    public GameObject[] stage=new GameObject[3];
    public Animator[] anim = new Animator[3];
    public SceneName AllNameScript;
    public SceneComponent Scene;
    public EventSystem Eve;
    public bool SelectOn = false;
    public Image[] SelectBig;
    Button SelectButton;
    int childnumber = 0;
    public bool AxisTrg = false;
    bool Uptrg = false;
    public Button BackButton;
    AnimationComponent AnimaObj;
    bool StageStart = false;
    Image[] StageFont;
    public RectTransform[] SelectList;
    public bool LeftMoveEnd = true;
    public bool RightMoveEnd = true;
    public Vector3[] SelectEndPos;
    public Vector3[] SelectPos;
    public float SelectMoveTime=50;
    public int set = 0;
    // Start is called before the first frame update
    void Start()
    {
        StageFont = new Image[3];
        StageFont[0] = GameObject.Find("SelectFont/StageSelect").GetComponent<Image>();
        StageFont[1] = GameObject.Find("SelectFont/Stage").GetComponent<Image>();
        StageFont[2] = GameObject.Find("SelectFont/Stage2").GetComponent<Image>();
        StageFont[2].enabled = false;

        StageStart = false;
        AllNameScript = GameObject.Find("SceneManager").GetComponent<SceneName>();
        Scene = GameObject.Find("SceneManager").GetComponent<SceneComponent>();

        button = GameObject.Find("Canvas/1~10").GetComponent<Button>();
        Eve = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        AnimaObj = GameObject.Find("Animation").GetComponent<AnimationComponent>();

        SelectPos = new Vector3[10];
        SelectEndPos = new Vector3[10];

        SelectList = new RectTransform[10];
        if (gameObject.name == "1~10"|| gameObject.name == "11~20"|| gameObject.name == "21~30")
        {
            Debug.Log(transform.Find("Select").name);
            transform.Find("Select").gameObject.SetActive(true);
            for (int i = 0; i < 10; i++)
            {
                Debug.Log(transform.Find("Select").transform.GetChild(i).name);
                SelectList[i] = transform.Find("Select").transform.GetChild(i).gameObject.GetComponent<RectTransform>();
            }
            transform.Find("Select").gameObject.SetActive(false);
            GetEndPos();
        }


        //ボタンが選択された状態になる

        button.Select();

        //ボタンを消す

        stage[0] = GameObject.Find("1~10");
        stage[1] = GameObject.Find("11~20");
        stage[2] = GameObject.Find("21~30");

        for (int t = 0; t < stage.Length; t++)
        {
            anim[t] = stage[t].GetComponent<Animator>();
            anim[t].enabled = true;
            foreach (Transform obj in stage[t].transform)
            {
                if (obj.gameObject.name != "Text")
                {
                    obj.gameObject.SetActive(false);
                }
            }
        }

        switch (gameObject.name)
        {
            case "1~10":
                SetStageName(0);
                break;
            case "11~20":
                SetStageName(10);
                break;
            case "21~30":
                SetStageName(20);
                break;

        }

        SelectBig = new Image[3];
        SelectBig[0] = GameObject.Find("Canvas/1/1").GetComponent<Image>();
        SelectBig[1] = GameObject.Find("Canvas/1/2").GetComponent<Image>();
        SelectBig[2] = GameObject.Find("Canvas/1/3").GetComponent<Image>();
        GameObject.Find("Canvas/1").GetComponent<Image>().enabled = false;
        BackButton = GameObject.Find("戻る").GetComponent<Button>();

        for (int i = 0; i < 3; i++)
        {
            SelectBig[i].enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectOn == true && gameObject.name != "戻る")
        {
            if (!StageStart)
            {
                if (LeftMoveEnd && RightMoveEnd)
                {
                    if ((Input.GetAxisRaw(Controll.十字キー上下.ToString()) >= 1) && AxisTrg == false && !Uptrg)
                    {
                        AxisTrg = true;
                        Uptrg = true;
                        SEManager.Instance.Play(Scene.EnterClip);
                    }
                    if ((Input.GetAxisRaw(Controll.十字キー上下.ToString()) <= -1) && AxisTrg == false && Uptrg)
                    {
                        AxisTrg = true;
                        Uptrg = false;
                        SEManager.Instance.Play(Scene.EnterClip);
                    }

                    if ((Input.GetAxis(Controll.十字キー左右.ToString()) >= 1) && AxisTrg == false)
                    {
                        AxisTrg = true;
                        SEManager.Instance.Play(Scene.EnterClip);
                        if (!Uptrg)
                        {
                            GetEndPos();
                            //Uipos0 = new Vector3(SelectList[9].localPosition.x, SelectList[9].localPosition.y, SelectList[9].localPosition.z);
                            childnumber++;
                            LeftMoveEnd = false;
                            GetLeftSelectPos();
                        }
                        else
                        {
                            Uptrg = false;
                        }
                        if (childnumber > 9)
                        {
                            childnumber = 0;
                        }
                    }
                    if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= -1) && AxisTrg == false)
                    {
                        AxisTrg = true;
                        SEManager.Instance.Play(Scene.EnterClip);
                        if (!Uptrg)
                        {
                            GetEndPos();
                            //Uipos0 = new Vector3(SelectList[0].localPosition.x, SelectList[0].localPosition.y, SelectList[0].localPosition.z);
                            childnumber--;
                            RightMoveEnd = false;
                            GetRightSelectPos();
                        }
                        else
                        {
                            Uptrg = false;
                        }

                        if (childnumber < 0)
                        {
                            childnumber = 9;
                        }
                    }
                }

                //if ((Input.GetAxis(Controll.十字キー左右.ToString()) < 1 && Input.GetAxis(Controll.十字キー左右.ToString()) > -1) && AxisTrg)
                if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= 0.5 &&
                    Input.GetAxis(Controll.十字キー左右.ToString()) >= -0.5 &&
                    Input.GetAxis(Controll.十字キー上下.ToString()) <= 0.5 &&
                    Input.GetAxis(Controll.十字キー上下.ToString()) >= -0.5) &&
                    AxisTrg)
                {
                    AxisTrg = false;
                }

                if (!RightMoveEnd)
                {
                    MoveRightSelect();
                }
                else if (!LeftMoveEnd)
                {
                    MoveLeftSelect();
                }
                if (Uptrg)
                {
                    Debug.Log("UPp");
                    BackButton.Select();
                    GameObject.Find("戻る").GetComponent<Animator>().SetBool("ScaleChange", true);
                }
                else
                {
                    Debug.Log("oooo");
                    transform.Find("Select").GetChild(childnumber).GetComponent<Button>().Select();
                    GameObject.Find("戻る").GetComponent<Animator>().SetBool("ScaleChange", false);
                }

                //foreach (Transform trans in transform)
                //{
                //    if (trans.gameObject.name != "Text")
                //    {
                //        if (trans.gameObject == Eve.currentSelectedGameObject)
                //        {
                //            var go = trans.gameObject.GetComponent<Animator>();
                //            go.enabled = true;
                //            go.SetBool("Select", true);
                //            // trans.gameObject.GetComponent<Button>().Select();
                //            //Debug.Log("bbb");
                //            //var info = go.GetAnimatorTransitionInfo(0);
                //            //go.Play(info.nameHash, 0, 0.0f);
                //            //Debug.Log(trans.gameObject.name);
                //        }
                //        else
                //        {
                //            var go = trans.gameObject.GetComponent<Animator>();
                //            go.enabled = true;
                //            go.SetBool("Select", false);
                //        }
                //    }
                //}

            }
        }
    }

    void GetEndPos()
    {
        for(int i = 0; i < 10; i++)
        {
            SelectEndPos[i] = SelectList[i].localPosition;
        }
    }
    void GetRightSelectPos()
    {
        for (int i = 0; i < 10; i++)
        {
            if (i == 9)
            {
                SelectPos[i] = new Vector3(SelectList[0].localPosition.x - SelectList[i].localPosition.x,
                    SelectList[0].localPosition.y - SelectList[i].localPosition.y,
                    SelectList[0].localPosition.z - SelectList[i].localPosition.z);
            }
            else
            {
                SelectPos[i] = new Vector3(SelectList[i + 1].localPosition.x - SelectList[i].localPosition.x,
                    SelectList[i + 1].localPosition.y - SelectList[i].localPosition.y,
                    SelectList[i + 1].localPosition.z - SelectList[i].localPosition.z);
            }
        }
    }
    void GetLeftSelectPos()
    {
        for (int i = 9; i >= 0; i--)
        {
            if (i == 0)
            {
                SelectPos[i] = new Vector3(SelectList[9].localPosition.x - SelectList[i].localPosition.x,
                    SelectList[9].localPosition.y - SelectList[i].localPosition.y,
                    SelectList[9].localPosition.z - SelectList[i].localPosition.z);
            }
            else
            {
                SelectPos[i] = new Vector3(SelectList[i - 1].localPosition.x - SelectList[i].localPosition.x,
                    SelectList[i - 1].localPosition.y - SelectList[i].localPosition.y,
                    SelectList[i - 1].localPosition.z - SelectList[i].localPosition.z);
            }
        }
    }
    bool MoveRightSelect()
    {
        set++;

        for (int i = 0; i < 10; i++)
        {
            //if (i == 9)
            //{
            //    SelectList[i].localPosition = Uipos0;
            //}
            //else
            //{
            //    SelectList[i].localPosition = new Vector3(SelectList[i + 1].localPosition.x, SelectList[i + 1].localPosition.y, SelectList[i + 1].localPosition.z);
            //}
            if (set >= 50)
            {
                if (i == 9)
                {
                    SelectList[i].localPosition = SelectEndPos[0];
                }
                else
                {
                    SelectList[i].localPosition = SelectEndPos[i + 1];
                }

                RightMoveEnd = true;
            }
            else
            {
                if (i == 9)
                {
                    SelectList[i].localPosition =
                        new Vector3(SelectList[i].localPosition.x + SelectPos[i].x / SelectMoveTime,
                        SelectList[i].localPosition.y + SelectPos[i].y / SelectMoveTime,
                        SelectList[i].localPosition.z + SelectPos[i].z / SelectMoveTime);
                }
                else
                {
                    SelectList[i].localPosition =
                        new Vector3(SelectList[i].localPosition.x + SelectPos[i].x / SelectMoveTime,
                        SelectList[i].localPosition.y + SelectPos[i].y / SelectMoveTime,
                        SelectList[i].localPosition.z + SelectPos[i].x / SelectMoveTime);
                }
            }
        }
        if (RightMoveEnd)
        {
            set = 0;
        }
        //for(int i = 0; i < 10; i++)
        //{
        //    if (i == 9)
        //    {

        //    }else
        //    {

        //    }
        //}
        return true;
    }
    bool MoveLeftSelect()
    {
        //for (int i = 9; i >= 0; i--)
        //{
        //    if (i == 0)
        //    {
        //        SelectList[i].localPosition = SelectEndPos[9];
        //    }
        //    else
        //    {
        //        SelectList[i].localPosition = SelectEndPos[i - 1];
        //    }
        //}
        set++;

        for (int i = 9; i >= 0; i--)
        {
            //if (i == 9)
            //{
            //    SelectList[i].localPosition = Uipos0;
            //}
            //else
            //{
            //    SelectList[i].localPosition = new Vector3(SelectList[i + 1].localPosition.x, SelectList[i + 1].localPosition.y, SelectList[i + 1].localPosition.z);
            //}
            if (set >= 50)
            {
                if (i == 0)
                {
                    SelectList[i].localPosition = SelectEndPos[9];
                }
                else
                {
                    SelectList[i].localPosition = SelectEndPos[i - 1];
                }

                LeftMoveEnd = true;
            }
            else
            {
                if (i == 9)
                {
                    SelectList[i].localPosition =
                        new Vector3(SelectList[i].localPosition.x + SelectPos[i].x / SelectMoveTime,
                        SelectList[i].localPosition.y + SelectPos[i].y / SelectMoveTime,
                        SelectList[i].localPosition.z + SelectPos[i].z / SelectMoveTime);
                }
                else
                {
                    SelectList[i].localPosition =
                        new Vector3(SelectList[i].localPosition.x + SelectPos[i].x / SelectMoveTime,
                        SelectList[i].localPosition.y + SelectPos[i].y / SelectMoveTime,
                        SelectList[i].localPosition.z + SelectPos[i].x / SelectMoveTime);
                }
            }
        }
        if (LeftMoveEnd)
        {
            set = 0;
        }


        return true;
    }
    //ステージ名をセット
    void SetStageName(int startNumber)
    {
        for (int i = 0; i < scenenames.Length; i++) 
        {
            scenenames[i] = AllNameScript.StageSceneNama[i + startNumber];
        }
    }

    //Back
    public void OnClick(int name)
    {
        SEManager.Instance.Play(Scene.EnterClip);
        if (SelectOn == true)
        {
            SelectOn = false;
            for(int i = 0; i < 3; i++)
            {
                stage[i].GetComponent<SelectMenu>().SelectOn = false;
                StageFont[i].enabled = false;
            }

            StageFont[0].enabled = true;
            StageFont[1].enabled = true;

            SetButton();
        }
        else
        {
            SceneComponent instance = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
            instance.SceneName = scenenames[name];
            instance.SceneFlag = true;
        }
        GameObject.Find("戻る").GetComponent<Animator>().SetBool("ScaleChange", false);

        //for(int i = 0; i < scenename1_10.Length; i++)
        //{
        //    if (scenename1_10[i] == i.ToString())
        //    {
        //        Debug.Log(scenename1_10[i]);
        //    }
        //}
    }

    public void StageSet(int num)
    {
        if (!StageStart)
        {
            SEManager.Instance.Play(Scene.EnterClip);
            StageStart = true;
        }

        SceneComponent instance = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
        instance.SceneName = scenenames[num];
        instance.SceneFlag = true;
    }

    public void MoveSelect(int num)
    {
        if (AnimaObj.time)
        {
            if (GameObject.Find("Animation").GetComponent<AnimationComponent>().AllAnimation == false)
            {
                SEManager.Instance.Play(Scene.EnterClip);
                //childnumber = 0;
                Uptrg = false;
                //非表示
                AllOffButton();

                //押したボタンのみ表示
                PushButton();

                SelectOn = true;
                //switch (num)
                //{
                //    case 0:

                //        break;

                //}
            }

            switch (num)
            {
                case 1:
                    SelectBig[num - 1].enabled = true;
                    break;
                case 2:
                    SelectBig[num - 1].enabled = true;
                    break;
                case 3:
                    SelectBig[num - 1].enabled = true;
                    break;
            }
            GameObject.Find("Canvas/1").GetComponent<Image>().enabled = true;
            StageFont[0].enabled = false;
            StageFont[1].enabled = false;
            StageFont[2].enabled = true;
        }
    }

    void AllOffButton()
    {
        //ボタン全非表示
        for (int t = 0; t < 3; t++)
        {
            SelectBig[t].enabled = false;

            foreach (Transform obj in stage[t].transform)
            {
                obj.gameObject.SetActive(false);
            }
            stage[t].gameObject.GetComponent<Button>().interactable = false;
            //stage[t].gameObject.GetComponent<Image>().enabled = false;
        }
        GameObject.Find("Canvas/1").GetComponent<Image>().enabled = false;

    }

    void PushButton()
    {
        //押したボタンのみ表示
        //int i = 0;
        //Debug.Log("a");
        transform.GetComponent<Button>().Select();
        foreach (Transform trans in transform)
        {
            if (trans.gameObject.name == "Text")
            {
                trans.gameObject.SetActive(false);
            }
            else
            {
                trans.gameObject.SetActive(true);
            }            

            if (trans.gameObject.name == "1")
            {
                trans.GetComponent<Button>().Select();
                SelectButton = trans.GetComponent<Button>();
            }
            //var go = trans.gameObject.GetComponent<Animator>();
            //go.enabled = true;
            //var info = go.GetAnimatorTransitionInfo(0);
            //go.Play(info.nameHash, 0, 0.0f);
        }

        GameObject.Find("戻る").GetComponent<SelectMenu>().SelectOn = true;
    }

    void SetButton()
    {
        AllOffButton();

        for(int i = 0; i < 3; i++)
        {
            stage[i].gameObject.GetComponent<Button>().interactable = true;
            foreach (Transform obj in stage[i].transform)
            {
                if (obj.gameObject.name == "Text")
                {
                    obj.gameObject.SetActive(true);
                }
            }
        }
    }
}
