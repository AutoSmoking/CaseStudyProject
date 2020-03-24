using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene : MonoBehaviour
{
    public string NextSceneName;
    public string TitleSceneName;
    private int nowscene = 0;
    public   GameObject[] a;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);
        a = GameObject.FindGameObjectsWithTag("EditorOnly");
        if (a.Length > 1)
        {
            for (int i = 1; i < a.Length; i++)
            {
                GameObject.Destroy(a[i]);
            }
        }
    }

    void awake()
    {
    }
    // Update is called once per frame
    void Update()
    {
        switch (nowscene)
        {
            case 0:
                if (Input.GetKey(KeyCode.Space))
                {
                    SceneManager.LoadScene(NextSceneName);
                    nowscene = 1;
                }
                break;

            case 1:
                if (Input.GetKey(KeyCode.Space)){
                    SceneManager.LoadScene(TitleSceneName);
                    nowscene = 0;
                }
                break;
        }
    }
}
