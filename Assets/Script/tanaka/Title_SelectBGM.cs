using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class Title_SelectBGM : MonoBehaviour
{
    public AudioClip clip;

    bool play = false;

    GameObject SceneManager;
    SceneComponent Scene;

    private void Start()
    {
        if (clip == null) 
        {
            Debug.LogError("BGMが入ってません");
        }

        else
        {
            play = true;
        }

        SceneManager = GameObject.Find("SceneManager");
        Scene = SceneManager.GetComponent<SceneComponent>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(clip.ToString());
        //Debug.Log(BGMManager.Instance.GetCurrentAudioNames().Count);
        //for (int i = 0; i < BGMManager.Instance.GetCurrentAudioNames().Count; i++)
        //{
        //    Debug.Log(BGMManager.Instance.GetCurrentAudioNames()[i]);
        //}
        if (play) 
        {
            //1つも再生してない場合
            if (!BGMManager.Instance.IsPlaying())
            {
                Debug.Log("再生0");
                BGMManager.Instance.Play(clip);
            }
            else
            {
                //再生中のリスト取得
                List<string> a = BGMManager.Instance.GetCurrentAudioNames();

                for (int i = 0; i < BGMManager.Instance.GetCurrentAudioNames().Count; i++)
                {

                    //再生しているのが流そうとしているモノと同じ
                    if (clip.ToString().Equals(a[i].ToString()))
                    {
                        break;
                    }
                    else if (i + 1 == BGMManager.Instance.GetCurrentAudioNames().Count)
                    {
                        BGMManager.Instance.Play(clip);
                        break;
                    }
                }
            }
            
            
            play = false;
        }
    }
}
