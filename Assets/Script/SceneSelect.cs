using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSelect : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
 
    }

    void Quit()
    {
#if UNITY_EDITOR
      UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
    

    // Update is called once per frame
    void Update()
    {
       //if(Input.GetKey(KeyCode.R))
       // {
       //     // Sceneの読み直し
       //     SceneManager.LoadScene(loadScene.name);
       // }
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("Tutorial_1");
        }
        if (Input.GetKey(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("Tutorial_2");
        }
        if (Input.GetKey(KeyCode.Alpha3))
        {
            SceneManager.LoadScene("Tutorial_3");
        }
        if (Input.GetKey(KeyCode.Escape))
        {
            Quit();
        }
    }
    
}
