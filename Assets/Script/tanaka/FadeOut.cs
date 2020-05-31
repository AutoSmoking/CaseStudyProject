using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    [SerializeField, Header("フェードアウト処理したいので追加")]
    public Image Panel;
    public bool FadeTrg;
    // Start is called before the first frame update
    void Start()
    {
        FadeTrg = false;
    }

    // Update is called once per frame
    void Update()
    {
        Panel.color -= new Color(0.0f, 0.0f, 0.0f, 0.005f);

        if (Panel.color.a <= 0)
        {
            FadeTrg = true;
        }
    }
}
