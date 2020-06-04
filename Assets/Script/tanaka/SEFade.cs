using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class SEFade : MonoBehaviour
{
    [SerializeField, Header("フェード時のSE")]
    public AudioClip FadeSE;

    [SerializeField, Header("フェード速度")]
    public float FadeSpeed;

    AudioSource audiosource;
    BubbleFadeOpe bubblefadeope;

    bool IsInplay = false;
    bool IsOutplay = false;

    float deltaTime = 0.0f;
    // Start is called before the first frame update
    void Start()
    {
        IsInplay = false;
        IsOutplay = false;

        deltaTime = 0.0f;

        if (gameObject.GetComponent<AudioSource>() != null)
        {
            audiosource = gameObject.GetComponent<AudioSource>();
        }
        if (gameObject.GetComponent<BubbleFadeOpe>() != null)
        {
            bubblefadeope = gameObject.GetComponent<BubbleFadeOpe>();
        }

        audiosource.clip = FadeSE;
        audiosource.loop = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (bubblefadeope.isFadeOut == true &&
            gameObject.GetComponent<Canvas>().enabled == true)
        {
            if (IsInplay)
            {
                FadeIn();
            } else
            {
                IsInplay = true;
                deltaTime = 0.0f;
            }

        }
        else if (bubblefadeope.isFadeIn == true &&
            gameObject.GetComponent<Canvas>().enabled == true)
        {
            if (IsOutplay)
            {
                FadeOut();
            }
            else
            {
                IsOutplay = true;
                deltaTime = 0.0f;
            }
        }
        else
        {
            if (audiosource.isPlaying)
            {
                Reset();
            }
        }
    }

    //再生
    void SEPlay()
    {
        deltaTime += Time.deltaTime;
        if (!audiosource.isPlaying)
        {
            audiosource.Play();
        }
    }

    //大きくなる
    public void FadeIn()
    {
        SEPlay();
        audiosource.volume = (float)(deltaTime / FadeSpeed);
    }
    //小さくなる
    public void FadeOut()
    {
        SEPlay();
        audiosource.volume = 1.0f - (float)(deltaTime / FadeSpeed);
    }
    void Reset()
    {
        IsInplay = false;
        IsOutplay = false;
        deltaTime = 0.0f;
        audiosource.Stop();
    }
}
