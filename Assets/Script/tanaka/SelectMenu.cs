using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using KanKikuchi.AudioManager;

using UnityEngine.EventSystems;

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

    // Start is called before the first frame update
    void Start()
    {
        AllNameScript = GameObject.Find("SceneManager").GetComponent<SceneName>();
        Scene = GameObject.Find("SceneManager").GetComponent<SceneComponent>();

        button = GameObject.Find("Canvas/1~10").GetComponent<Button>();
        Eve = GameObject.Find("EventSystem").GetComponent<EventSystem>();
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
    }

    // Update is called once per frame
    void Update()
    {
        if (SelectOn == true && gameObject.name != "戻る")
        {
            foreach (Transform trans in transform)
            {
                if (trans.gameObject.name != "Text")
                {
                    if (trans.gameObject == Eve.currentSelectedGameObject)
                    {
                        var go = trans.gameObject.GetComponent<Animator>();
                        go.enabled = true;
                        go.SetBool("Select", true);
                        trans.gameObject.GetComponent<Button>().Select();
                        //var info = go.GetAnimatorTransitionInfo(0);
                        //go.Play(info.nameHash, 0, 0.0f);
                        //Debug.Log(trans.gameObject.name);
                    }
                    else
                    {
                        var go = trans.gameObject.GetComponent<Animator>();
                        go.enabled = true;
                        go.SetBool("Select", false);
                    }
                }
            }
        }
    }

    //ステージ名をセット
    void SetStageName(int startNumber)
    {
        for (int i = 0; i < scenenames.Length; i++) 
        {
            scenenames[i] = AllNameScript.StageSceneNama[i + startNumber];
        }
    }

    public void OnClick(int name)
    {
        SEManager.Instance.Play(Scene.EnterClip);
        if (SelectOn == true)
        {
            SelectOn = false;
            for(int i = 0; i < 3; i++)
            {
                stage[i].GetComponent<SelectMenu>().SelectOn = false;
            }
            SetButton();
        }
        else
        {
            SceneComponent instance = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
            instance.SceneName = scenenames[name];
            instance.SceneFlag = true;
        }
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
        SceneComponent instance = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
        instance.SceneName = scenenames[num];
        instance.SceneFlag = true;
    }

    public void MoveSelect(int num)
    {
        if (GameObject.Find("Animation").GetComponent<AnimationComponent>().AllAnimation == false)
        {
            SEManager.Instance.Play(Scene.EnterClip);

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
    }

    void AllOffButton()
    {
        //ボタン全非表示
        for (int t = 0; t < 3; t++)
        {
            foreach (Transform obj in stage[t].transform)
            {
                obj.gameObject.SetActive(false);
            }
            stage[t].gameObject.GetComponent<Button>().interactable = false;
            //stage[t].gameObject.GetComponent<Image>().enabled = false;
        }
    }

    void PushButton()
    {
        //押したボタンのみ表示
        //int i = 0;
        Debug.Log("a");
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
