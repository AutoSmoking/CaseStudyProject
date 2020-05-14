using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultComponent : MonoBehaviour
{
    static public ResultComponent instance;
    SceneComponent scene;
    public Button button;
    public GameObject canvas;
    public bool buttonflag = false; 

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
        button = GameObject.Find("Canvas/Button").GetComponent<Button>();
        //canvas = GameObject.GetComponentInChildren<GameObject>();
        button.Select();
        canvas.SetActive(false);
        scene = GameObject.Find("SceneManager").GetComponent<SceneComponent>();

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
            button.Select();
            buttonflag = true;
        }
    }

    public void NextStage()
    {
        buttonflag = false;
        scene.SceneFlag = true;
        scene.GameFrag = false;
        scene.SceneName = scene.StageNameInstance.NextSceneName;
        canvas.SetActive(false);
    }

    public void TitleMenu()
    {
        buttonflag = false;
        scene.SceneFlag = true;
        scene.GameFrag = false;
        scene.SelectTransition();
        canvas.SetActive(false);

    }
}
