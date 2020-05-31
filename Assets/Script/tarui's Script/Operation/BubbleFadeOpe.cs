using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleFadeOpe : MonoBehaviour
{
    [SerializeField, Header("速さ")]
    public float spd = 1f;

    [SerializeField, Header("次の泡が動くまでの待ち時間"), Range(0.01f, 0.5f)]
    float delayTime = 0.1f;

    // 初期位置格納
    Vector3 firstPos = Vector3.zero;

    public bool isFadeIn = false;
    public bool isFadeOut = false;

    bool isFadeInStart = true;
    bool isFadeOutStart = true;

    bool isEndChildMove = false;

    Canvas canvas = null;
    
    List<Transform> child = new List<Transform>() { };
    
    // Start is called before the first frame update
    void Start()
    {
        // 初期位置を格納
        firstPos = this.transform.position;

        canvas = this.GetComponent<Canvas>();

        // すべての子オブジェクトを取得
        foreach(var trans in this.GetComponentsInChildren<Transform>())
        {
            if(trans == this.transform)
            {
                continue;
            }

            child.Add(trans);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isFadeIn)
        {
            FadeIn();
        }
        else if(!isFadeInStart)
        {
            isFadeInStart = true;
        }
        
        if(isFadeOut)
        {
            FadeOut();
        }
        else if(!isFadeOutStart)
        {
            isFadeOutStart = true;
        }
    }

    void FadeOut()
    {
        if (isFadeOutStart)
        {
            // フェード用の画像の位置を初期化
            foreach (var child in child)
            {
                child.GetComponent<FadeMoveOpe>().isStart = true;
            }

            canvas.enabled = true;

            StartCoroutine(DelayMethod(delayTime));

            isFadeOutStart = false;
        }

        if (!isEndChildMove && child[child.Count - 1].GetComponent<FadeMoveOpe>().isMove) 
        {
            isEndChildMove = true;
        }

        if (isEndChildMove && !child[child.Count - 1].GetComponent<FadeMoveOpe>().isMove)
        {
            isFadeOut = false;
            isEndChildMove = false;
        }
    }

    void FadeIn()
    {
        if (isFadeInStart)
        {
            foreach (var child in child)
            {
                child.GetComponent<FadeMoveOpe>().isStart = true;
            }

            canvas.enabled = true;

            StartCoroutine(DelayMethod(delayTime));

            isFadeInStart = false;
        }

        if (!isEndChildMove && child[child.Count - 1].GetComponent<FadeMoveOpe>().isMove)
        {
            isEndChildMove = true;
        }

        if (isEndChildMove && !child[child.Count - 1].GetComponent<FadeMoveOpe>().isMove) 
        {
            isFadeIn = false;
            isEndChildMove = false;

            canvas.enabled = false;
        }
    }

    IEnumerator DelayMethod(float WaitTime)
    {
        foreach (var child in child)
        {
            yield return new WaitForSeconds(WaitTime);

            child.GetComponent<FadeMoveOpe>().isMove = true;
        }
    }
}
