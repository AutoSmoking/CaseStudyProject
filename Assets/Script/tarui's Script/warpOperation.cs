using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpOperation : MonoBehaviour
{
    [SerializeField, Header("ワープ先")]
    GameObject next;

    [SerializeField, Header("ワープまでの待ち時間"),Range(0.0f,5.0f)]
    float WaitTime;

    [SerializeField, Header("ワープ中のの待ち時間"), Range(0.0f, 5.0f)]
    float WarpTime;

    [SerializeField, Header("")]
    List<GameObject> objList = new List<GameObject>() { };

    // ワープ中の待ち時間用
    bool WaitFlg = false;
    // ワープ中を表すフラグ
    bool WarpFlg = false;

    GameObject Object;

    Vector3 firstPos;

    float percent;

    // Start is called before the first frame update
    void Start()
    {
        if(objList.Count == 0)
        {
            Debug.LogError("ワープする対象がないです。");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(WarpFlg)
        {
            if (WaitFlg)
            {
                if(percent >= WarpTime)
                {
                    percent = 0;
                    WarpFlg = false;

                    // ワープが終わったらオブジェクトを出現させる
                    Object.SetActive(true);
                }
                else
                {
                    percent += Time.deltaTime;
                }
            }
            else
            {
                if (percent >= WaitTime)
                {
                    Object.transform.position = next.transform.position;
                    percent = 0;
                    //WarpFlg = false;
                    WaitFlg = true;

                    // 止めていたスクリプトを再起動
                    foreach (var com in Object.GetComponent<GetComOperation>().com)
                    {
                        com.enabled = true;
                    }

                    // ワープ中はオブジェクトは消す
                    Object.SetActive(false);
                }
                else
                {
                    percent += Time.deltaTime;

                    float Wait = percent / WaitTime;

                    Object.transform.position = Vector3.Lerp(firstPos, this.transform.position, Wait);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(var obj in objList)
        {
            if(obj == other.gameObject && WarpFlg == false)
            {
                //obj.transform.position = next.transform.position;
                WarpFlg = true;

                Object = obj;

                firstPos = obj.transform.position;

                foreach(var com in obj.GetComponent<GetComOperation>().com)
                {
                    com.enabled = false;
                }
            }
        }
    }
}
