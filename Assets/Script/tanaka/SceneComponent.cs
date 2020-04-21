using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneComponent : MonoBehaviour
{
    [SerializeField, Header("今のシーン名")]
    public string SceneName;

    public bool SceneFlag;
    public bool GameFrag;
    static public SceneComponent instance;
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
        SceneFlag = false;
        GameFrag = false;
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.Space)) && SceneFlag == false && (SceneManager.GetActiveScene().name == "Title Scene" || (SceneManager.GetActiveScene().name == "StageSelect" && SceneName != "StageSelect"))) 
        {

            SceneFlag = true;
        }
        if (SceneFlag == true)
        {
            if (SceneManager.GetActiveScene().name == "Title Scene") 
            {
                SceneName = "StageSelect";
            }
            else if(SceneManager.GetActiveScene().name == "StageSelect")
            {
                if(SceneName!="Title Scene")
                {
                    GameFrag = false;
                }
            }
            SceneManager.LoadScene(SceneName);
            SceneFlag = false;
        }

        if ((SceneManager.GetActiveScene().name != "Title Scene" && SceneManager.GetActiveScene().name != "StageSelect") && GameFrag == true) 
        {
            SceneComponent instance = GameObject.Find("SceneManager").GetComponent<SceneComponent>();

            instance.SceneName = "StageSelect";
            SceneFlag = true;
        }
    }
}
