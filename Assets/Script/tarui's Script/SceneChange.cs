using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    [SerializeField, Header("シーン切り替えボタン")]
    KeyCode key = KeyCode.Alpha1;

    [SerializeField, Header("切り替え先シーン名(ここに名前を書いてね)")]
    string scene;

    // Start is called before the first frame update
    void Start()
    {
        if (scene == null)
        {
            Debug.LogError("シーンが設定されていません。");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(key))
        {
            SceneManager.LoadScene(scene);
        }
    }
}
