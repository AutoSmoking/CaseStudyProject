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

    SceneComponent Scene = null;
    // Start is called before the first frame update
    void Start()
    {
        Scene = transform.parent.GetComponent<SceneComponent>();
        FadeTrgIn = false;
        FadeTrgOut = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!FadeTrgIn)
        {
            Debug.Log("In");
            Panel.color += new Color(0.0f, 0.0f, 0.0f, 0.01f);
        }

        if (Panel.color.a >= 1.0f) 
        {
            FadeTrgIn = true;
        }
        if (!FadeTrgOut)
        {
            Debug.Log("Out");
            Panel.color -= new Color(0.0f, 0.0f, 0.0f, 0.01f);
        }

        if (Panel.color.a <= 0.0f) 
        {
            FadeTrgOut = true;
            Scene.WhiteFadeTrg = false;
        }
    }

    public void ReSetFadeOut()
    {
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
