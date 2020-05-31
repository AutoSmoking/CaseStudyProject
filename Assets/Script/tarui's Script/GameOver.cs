using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    List<GameObject> bubble = new List<GameObject>() { };

    bool isFade = false;
    bool isOver = false;

    BubbleFadeOpe fadeOpe = null;

    // Start is called before the first frame update
    void Start()
    {
        bubble.Add(GameObject.FindGameObjectWithTag("1"));
        bubble.Add(GameObject.FindGameObjectWithTag("2"));
        bubble.Add(GameObject.FindGameObjectWithTag("3"));

        fadeOpe = this.GetComponent<BubbleFadeOpe>();
    }

    // Update is called once per frame
    void Update()
    {
        if (fadeOpe.isFadeOut == false)
        {
            if(isOver)
            {
                Reload();
            }

            foreach (var obj in bubble)
            {
                if (obj != null)
                {
                    isFade = false;
                    break;
                }

                isFade = true;
            }

            if (isFade)
            {
                fadeOpe.isFadeOut = true;
                isFade = false;
            }
        }
        else
        {
            isOver = true;
        }
    }
    void Reload()
    {
        // 現在読み込んでいるシーンを再読込み
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
