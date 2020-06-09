using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField, Header("フェード速度(アルファ値の増減値)")]
    public float Alpha;

    [SerializeField, Header("フェードイン後に止まる時間")]
    public float StopTime = 2.0f;

    [SerializeField, Header("フェード様のパネル")]
    public Image Panel;
    public bool FadeTrgIn;
    public bool FadeTrgOut;

    SceneComponent Scene = null;


    float Delta;
    // Start is called before the first frame update
    void Start()
    {
        Scene = transform.parent.GetComponent<SceneComponent>();
        FadeTrgIn = false;
        FadeTrgOut = false;
        Delta = 2.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FadeTrgIn)
        {
            Panel.color += new Color(0.0f, 0.0f, 0.0f, Alpha);
        }

        if (Panel.color.a >= 1.0f) 
        {
            FadeTrgIn = true;
        }
        if (!FadeTrgOut)
        {
            if (Delta <= StopTime)
            {
                Delta += Time.deltaTime;
            }
            else
            {
                Panel.color -= new Color(0.0f, 0.0f, 0.0f, Alpha);
            }
        }

        if (Panel.color.a <= 0.0f) 
        {
            FadeTrgOut = true;
            Scene.WhiteFadeTrg = false;
        }
    }

    public void ReSetFadeOut()
    {
        Delta = 0.0f;
        FadeTrgOut = false;
    }
    public void ReSetFadeIn()
    {
        FadeTrgIn = false;
    }
    public bool IsEnd()
    {
        if (FadeTrgOut == true && FadeTrgIn == true)
        {
            return true;
        }
        return false;
    }                
}
