using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField, Header("フェードアウト処理したいので追加")]
    public Image Panel;
    public bool FadeTrgIn;
    public bool FadeTrgOut;
    // Start is called before the first frame update
    void Start()
    {
        FadeTrgIn = false;
        FadeTrgOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FadeTrgIn)
        {
            Panel.color += new Color(0.0f, 0.0f, 0.0f, 0.005f);
        }

        if (Panel.color.a >= 1.0f) 
        {
            FadeTrgIn = true;
        }
        if (!FadeTrgOut)
        {
            Panel.color -= new Color(0.0f, 0.0f, 0.0f, 0.005f);
        }

        if (Panel.color.a <= 0.0f) 
        {
            FadeTrgOut = true;
        }
    }

    public void ReSetFade()
    {
        Panel.color = new Color(0.0f, 0.0f, 0.0f, 0.0f);
        FadeTrgIn = false;
        FadeTrgOut = false;
    }
}
