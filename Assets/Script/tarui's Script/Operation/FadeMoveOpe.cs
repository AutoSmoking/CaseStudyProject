using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeMoveOpe : MonoBehaviour
{
    BubbleFadeOpe fadeOpe;

    Vector3 firstPos = Vector3.zero;
    Vector3 endPos = Vector3.zero;

    Transform parent = null;

    [System.NonSerialized]
    public bool isMove = false;

    [System.NonSerialized]
    public bool isStart = false;

    // Start is called before the first frame update
    void Start()
    {
        fadeOpe = this.GetComponentInParent<BubbleFadeOpe>();
        parent = this.transform.parent;

        firstPos = this.transform.localPosition;

        if (fadeOpe.isFadeOut)
        {
            this.transform.localPosition -= new Vector3(0, 1080.0f * 1.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // フェード開始時に実行される初期化
        // BubbleFadeOpe に依存した処理なので注意！
        if(isStart)
        {
            if(fadeOpe.isFadeOut)
            {
                endPos = firstPos; 

                this.transform.localPosition = firstPos - new Vector3(0, 1080.0f * 1.5f);
            }
            else if(fadeOpe.isFadeIn)
            {
                endPos = firstPos + new Vector3(0, 1080.0f * 1.5f);

                this.transform.localPosition = firstPos;
            }

            isStart = !isStart;
        }

        if (isMove)
        {
            if (fadeOpe.isFadeOut || fadeOpe.isFadeIn)
            {
                AddMove(this.transform, new Vector3(0, fadeOpe.spd));

                if (this.transform.localPosition.y >= endPos.y) 
                {
                    this.transform.localPosition = endPos;

                    isMove = !isMove;
                }
            }
        }
    }

    bool SimilarValue(float a, float b)
    {
        if (a <= b + 0.1f && a >= b - 0.1f)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool SimilarValue(float a, float b, float range)
    {
        if (a <= b + range && a >= b - range)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void AddMove(Transform trans, Vector3 add)
    {
        trans.localPosition += add;
    }
}
