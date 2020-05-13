using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SelectMenu : MonoBehaviour
{
    [SerializeField, Header("1～10のステージシーン名")]
    public string[] scenenames = new string[10];
    Button button;
    public GameObject[] stage=new GameObject[3];
    public Animator[] anim = new Animator[3];
    public SceneName AllNameScript;
    // Start is called before the first frame update
    void Start()
    {
        AllNameScript = GameObject.Find("SceneManager").GetComponent<SceneName>();

        button = GameObject.Find("Canvas/1~10").GetComponent<Button>();

        //ボタンが選択された状態になる

        button.Select();

        //ボタンを消す

        stage[0] = GameObject.Find("1~10");
        stage[1] = GameObject.Find("11~20");
        stage[2] = GameObject.Find("21~30");

        for (int t = 0; t < stage.Length; t++)
        {
            anim[t] = stage[t].GetComponent<Animator>();
            anim[t].enabled = false;
            foreach (Transform obj in stage[t].transform)
            {
                obj.gameObject.SetActive(false);
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
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            //string name = gameObject.name;
            //Debug.Log(int.Parse(name));
            //SceneComponent instance = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
            //EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();

            //if (eventSystem.currentSelectedGameObject.gameObject == gameObject)
            //{
            //    OnClick(int.Parse(gameObject.name) + 1);
            //}

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
        SceneComponent instance = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
        instance.SceneName = scenenames[name];
        instance.SceneFlag = true;
        //for(int i = 0; i < scenename1_10.Length; i++)
        //{
        //    if (scenename1_10[i] == i.ToString())
        //    {
        //        Debug.Log(scenename1_10[i]);
        //    }
        //}
    }

    public void MoveSelect(int num)
    {
        for (int t = 0; t < 3; t++)
        {
            foreach (Transform obj in stage[t].transform)
            {
                obj.gameObject.SetActive(false);
            }
        }

        int i = 0;
        Debug.Log("a");
        foreach(Transform trans in transform)
        {
            trans.gameObject.SetActive(true);
            var go = trans.gameObject.GetComponent<Animator>();
            go.enabled = true;
            var info = go.GetAnimatorTransitionInfo(0);
            go.Play(info.nameHash, 0, 0.0f);
        }
        
        switch (num)
        {
            case 0:

                break;

        }
    }
}
