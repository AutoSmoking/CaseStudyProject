using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class StageBGM : MonoBehaviour
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
        if (play)
        {
            play = false;
            BGMManager.Instance.Play(clip);
        }
    }
}
