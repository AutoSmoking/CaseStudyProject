using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpOperation : MonoBehaviour
{
    [SerializeField, Header("ワープ先")]
    public GameObject next;

    [SerializeField, Header("ワープまでの待ち時間"),Range(0.01f,5.0f)]
    float WaitTime = 0.01f;

    [SerializeField, Header("ワープ中のの待ち時間"), Range(0.01f, 5.0f)]
    float WarpTime = 0.01f;

    [SerializeField, Header("ワープ対象のオブジェクト(入れたオブジェクトは止める対象にもなる)")]
    List<GameObject> objList = new List<GameObject>() { };

    [SerializeField, Header("ワープ中に止める対象のオブジェクト(上記以外のオブジェクト)")]
    List<GameObject> stopList = new List<GameObject>() { };

    [SerializeField, Header("ワープオブジェクトを格納")]
    List<GameObject> warpObj = new List<GameObject>() { };

    [SerializeField, Header("ゴールかどうか")]
    bool GoalFlg = false;

    [SerializeField, Header("ワープ中に停止させるか TRUE:させる")]
    bool StopFlg = false;

    // ワープ中の待ち時間用
    bool WaitFlg = false;
    // ワープ中を表すフラグ
    bool WarpFlg = false;

    // ワープしているオブジェクトを一時格納
    [System.NonSerialized]
    public GameObject Object;

    Vector3 firstPos;
    Vector3 firstScale;

    float percent;

    Animation ani;

    SkinnedMeshRenderer meshRen;

    // Start is called before the first frame update
    void Start()
    {
        if(objList.Count == 0)
        {
            Debug.LogError("ワープする対象がないです。");
        }

        if(next == null && !GoalFlg)
        {
            Debug.LogError("ワープ先がないです。");
        }

        ani = this.gameObject.GetComponentInChildren<Animation>();
        

        // ゴールなら開かない
        if (GoalFlg)
        {
            ani.Play();
        }

        if(this.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>() == null)
        {
            meshRen = this.transform.GetChild(0).GetChild(1).gameObject.AddComponent<SkinnedMeshRenderer>();
        }
        else
        {
            meshRen = this.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>();
        }

        if (GoalFlg)
        {
            SetMatColor(meshRen, Color.red);
        }
        else
        {
            SetMatColor(meshRen, Color.green);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // ワープ開始
        if(WarpFlg)
        {
            // ワープ中は待機
            if (WaitFlg)
            {
                // 待ち時間が終わったらワープ完了
                if(percent >= WarpTime)
                {
                    percent = 0;
                    WarpFlg = false;
                    WaitFlg = false;
                    // 自身のオブジェクトをワープの出口に切り替える
                    GoalFlg = true;

                    // ワープが終わったらオブジェクトを出現させる
                    if (!Object.GetComponent<MeshRenderer>())
                    {
                        Object.SetActive(true);
                    }
                    else
                    {
                        Object.GetComponent<MeshRenderer>().enabled = true;
                    }

                    // 対象オブジェクトをワープ先まで移動
                    Object.transform.position = next.transform.position;

                    // 対象のオブジェクトをもとの大きさに戻す
                    Object.transform.localScale = firstScale;

                    // 開くアニメーション(ワープ先のオブジェクト)
                    next.GetComponentInChildren<Animation>().Play("shako_open");

                    // すべてのワープオブジェクトの次のワープ先に自分のオブジェクトを設定する
                    foreach (var obj in warpObj)
                    {
                        obj.GetComponent<warpOperation>().next = this.gameObject;
                    }

                    // 止めていたスクリプトを再起動

                    if (StopFlg)
                    {
                        foreach (var stopObj in objList)
                        {
                            foreach (var com in stopObj.GetComponents<MonoBehaviour>())
                            {
                                com.enabled = true;
                            }

                            foreach (var com in stopObj.GetComponents<Collider>())
                            {
                                com.enabled = true;
                            }

                            if (stopObj.GetComponent<Rigidbody>() != null)
                            {
                                stopObj.GetComponent<Rigidbody>().isKinematic = false;
                            }
                        }
                        foreach (var stopObj in stopList)
                        {
                            foreach (var com in stopObj.GetComponents<MonoBehaviour>())
                            {
                                com.enabled = true;
                            }

                            foreach (var com in stopObj.GetComponents<Collider>())
                            {
                                com.enabled = true;
                            }

                            if (stopObj.GetComponent<Rigidbody>() != null)
                            {
                                stopObj.GetComponent<Rigidbody>().isKinematic = false;
                            }
                        }
                    }
                    else
                    {
                        foreach (var com in Object.GetComponents<MonoBehaviour>())
                        {
                            com.enabled = true;
                        }

                        foreach (var com in Object.GetComponents<Collider>())
                        {
                            com.enabled = true;
                        }

                        if (Object.GetComponent<Rigidbody>() != null)
                        {
                            Object.GetComponent<Rigidbody>().isKinematic = false;
                        }
                    }

                    // 保持しているゲームオブジェクトを初期化
                    Object = null;
                }
                // ワープしている間は待つ
                else
                {
                    percent += Time.deltaTime;
                }
            }
            // ワープするまではゆっくりとワープオブジェクトの
            // 中心に移動
            else
            {
                // 中心に移動しきったらワープさせる
                if (percent >= WaitTime)
                {
                    // ワープ先に現在ワープしているオブジェクトを設定
                    next.GetComponent<warpOperation>().Object = Object;

                    percent = 0;
                    //WarpFlg = false;
                    WaitFlg = true;

                    // 閉じるアニメーションが再生されてないならしておく
                    if(!ani.isPlaying)
                    {
                        ani.Stop();
                        // 閉じるアニメーション
                        ani.Play("shako_close");
                    }

                    // ワープ中はオブジェクトは消す
                    if(!Object.GetComponent<MeshRenderer>())
                    {
                        Object.SetActive(false);
                    }
                    else
                    {
                        Object.GetComponent<MeshRenderer>().enabled = false;
                    }
                }
                // ワープ対象のオブジェクトが自身の中心まで移動
                else
                {
                    percent += Time.deltaTime;

                    float Wait = percent / WaitTime;

                    Object.transform.position = Vector3.Lerp(firstPos, this.transform.position, Wait);
                    Object.transform.localScale = Vector3.Lerp(firstScale, Vector3.zero, Wait);

                    if(!ani.isPlaying)
                    {
                        if (WaitTime <= 0.8f)
                        {
                            ani.Stop();
                            // 閉じるアニメーション
                            ani.Play("shako_close");

                        }
                        else
                        {
                            if (percent >= (WaitTime - 0.8f))
                            {
                                ani.Stop();
                                // 閉じるアニメーション
                                ani.Play("shako_close");
                            }
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        // 自身がワープの入り口かつワープ中でないなら処理
        if(!GoalFlg && !WarpFlg)
        {
            foreach (var obj in objList)
            {
                if (obj == other.gameObject && !obj.GetComponent<BubbleBlast>())
                {
                    // ワープを開始するフラグ
                    WarpFlg = true;

                    // ワープの対象となるオブジェクトを格納
                    Object = obj;

                    // 位置の補間をするためにこの瞬間の位置を格納
                    firstPos = obj.transform.position;

                    // 大きさの補間用
                    firstScale = obj.transform.localScale;

                    // ワープしている間は位置が変わるオブジェクトや
                    // 操作するオブジェクトは停止する

                    if (StopFlg)
                    {
                        foreach (var stopObj in objList)
                        {
                            foreach (var com in stopObj.GetComponents<MonoBehaviour>())
                            {
                                com.enabled = false;
                            }

                            foreach (var com in stopObj.GetComponents<Collider>())
                            {
                                com.enabled = false;
                            }

                            if (stopObj.GetComponent<Rigidbody>() != null)
                            {
                                stopObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                                stopObj.GetComponent<Rigidbody>().isKinematic = true;
                            }
                        }

                        foreach (var stopObj in stopList)
                        {
                            foreach (var com in stopObj.GetComponents<MonoBehaviour>())
                            {
                                com.enabled = false;
                            }

                            foreach (var com in stopObj.GetComponents<Collider>())
                            {
                                com.enabled = false;
                            }

                            if (stopObj.GetComponent<Rigidbody>() != null)
                            {
                                stopObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                                stopObj.GetComponent<Rigidbody>().isKinematic = true;
                            }
                        }
                    }
                    else
                    {
                        foreach (var com in obj.GetComponents<MonoBehaviour>())
                        {
                            com.enabled = false;
                        }

                        foreach (var com in obj.GetComponents<Collider>())
                        {
                            com.enabled = false;
                        }

                        if (obj.GetComponent<Rigidbody>() != null)
                        {
                            obj.GetComponent<Rigidbody>().velocity = Vector3.zero;
                            obj.GetComponent<Rigidbody>().isKinematic = true;
                        }
                    }

                    // 色を変える
                    SetMatColor(meshRen, Color.red);

                    break;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // 自身がワープの出口かつワープ対象のオブジェクトが格納されているとき
        // 判定から外れた瞬間、ワープの入り口にする
        if(GoalFlg)
        {
            if(Object == other.gameObject)
            {
                // 初期化
                Object = null;

                GoalFlg = false;

                // 色を変える
                SetMatColor(meshRen, Color.green);
            }
        }
    }


    void SetMatColor(SkinnedMeshRenderer mesh, Color col)
    {
        mesh.material.color = col; //meshのmaterialの色を変える
    }
}
