using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneName : MonoBehaviour
{
    public List<string> Nama = new List<string>();
    public string TitleName;
    public SceneComponent SceneScript;
    public string NowSceneName;
    // Start is called before the first frame update
    void Start()
    {
        SceneScript = gameObject.GetComponent<SceneComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneScript.SceneName != NowSceneName)
        {
            NowSceneName = SceneScript.SceneName;
        }
    }
}
