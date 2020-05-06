using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneName : MonoBehaviour
{
    [SerializeField, Header("ステージの順番にステージ名を入れていく")]
    public List<string> StageSceneNama = new List<string>();
    public string TitleName;
    public SceneComponent SceneScript;
    public string NowSceneName;
    public string NextSceneName;
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

            if(NowSceneName== "Title Scene")
            {
                NextSceneName = "StageSelect";
            }
            if(NowSceneName== "StageSelect")
            {
                NextSceneName = StageSceneNama[0];
            }
            for(int i = 0; i < StageSceneNama.Count; i++)
            {
                if (NowSceneName == StageSceneNama[i])
                {
                    if (i == StageSceneNama.Count)
                    {
                        NextSceneName = "StageSelect";
                    }
                    else
                    {
                        NextSceneName = StageSceneNama[i + 1];
                    }
                }
            }

        }
    }
}
