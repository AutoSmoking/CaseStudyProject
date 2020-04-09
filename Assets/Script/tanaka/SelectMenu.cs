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
    // Start is called before the first frame update
    void Start()
    {
        button = GameObject.Find("Canvas/1~10").GetComponent<Button>();

        //ボタンが選択された状態になる

        button.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
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
        foreach(Transform trans in gameObject.transform)
        {
            var go = transform.gameObject;
        }
        
        switch (num)
        {
            case 0:

                break;

        }
    }
}
