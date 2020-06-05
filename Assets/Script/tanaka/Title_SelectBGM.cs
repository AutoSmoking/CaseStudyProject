using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class Title_SelectBGM : MonoBehaviour
{
    public AudioClip clip;

    bool play = false;

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

            List<string> a =  BGMManager.Instance.GetCurrentAudioNames();
            if (BGMManager.Instance.GetCurrentAudioNames().Count == 0)
            {
                Debug.Log("uuuuuu");
                BGMManager.Instance.Play(clip);
            }
            else
            {
                for (int i = 0; i < BGMManager.Instance.GetCurrentAudioNames().Count; i++)
                {
                    if (a[i].ToString() == clip.ToString())
                    {
                        break;
                    }
                    else if (i + 1 == BGMManager.Instance.GetCurrentAudioNames().Count)
                    {
                        Debug.Log("a" + a[i].ToString());
                        Debug.Log(clip.ToString());
                        BGMManager.Instance.Play(clip);
                        break;
                    }
                }
            }
            
            
            play = false;
        }
    }
}
