
// デバッグ用のログを出すときはコメントアウトしてね
//#define RELEASE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaArea : MonoBehaviour
{
    [SerializeField, Header("回転の仕様の変更 false:角度で制御　true:距離で制御")]
    bool rotFlag;


    [SerializeField, Header("回転方向フラグ false:左　true:右")]
    bool LRFlag;

    [SerializeField, Header("影響があるオブジェクト(多分泡かな) 入れないとバグ")]
    List<GameObject> BubbleObj = new List<GameObject>() { };

    [SerializeField, Header("ステージの中心座標 SpinObject入れとけばいいかと")]
    Transform AreaCenter;

    [SerializeField, Header("回転速度"), Range(0, 1000)]
    float spd;

    // 動作フラグ
    public bool DoFlg = false;

    // Start is called before the first frame update
    void Start()
    {
#if RELEASE
#else
        if (BubbleObj == null)
        {
            Debug.Log(this.name + "の BubbleObj に値が入ってないYO！");
        }
        if (spd == 0)
        {
            Debug.Log(this.name + "の spd の値が０だけど大丈夫？");
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider collision)
    {
        if (DoFlg)
        {
            // 当たっているものが設定したゲームオブジェクトの場合
            foreach (var obj in BubbleObj)
            {
                if (collision.transform.gameObject == obj)
                {
                    // ステージの中心から対象のオブジェクトまでの長さ
                    float X, Y;
                    float len;
                    // ステージの中心から対象のオブジェクトまでの角度
                    float theta;
                    // 計算結果位置
                    Vector3 pos = new Vector3();

                    X = obj.transform.position.x - AreaCenter.position.x;
                    Y = obj.transform.position.y - AreaCenter.position.y;
                    len = Mathf.Sqrt(X * X + Y * Y);
                    theta = Mathf.Atan2(Y, X);

                    if (rotFlag)
                    {
                        float len2; // 直径

                        len2 = len * 2;

                        float N;    // 回転数[spm]

                        N = (spd * Time.deltaTime) / (Mathf.PI * len2) / 60.0f;

                        float rad;

                        rad = N * 2 * Mathf.PI;

                        if (LRFlag)
                        {
                            theta -= rad;
                        }
                        else
                        {
                            theta += rad;
                        }

                        pos.x = len * Mathf.Cos(theta) + AreaCenter.position.x;
                        pos.y = len * Mathf.Sin(theta) + AreaCenter.position.y;
                        pos.z = obj.transform.position.z;

                        obj.transform.position = pos;
                    }
                    else
                    {
                        if (LRFlag)
                        {
                            theta -= spd * Time.deltaTime;
                        }
                        else
                        {
                            theta += spd * Time.deltaTime;
                        }

                        pos.x = len * Mathf.Cos(theta) + AreaCenter.position.x;
                        pos.y = len * Mathf.Sin(theta) + AreaCenter.position.y;
                        pos.z = obj.transform.position.z;

                        obj.transform.position = pos;
                    }

                    break;
                }
            }
        }
    }
}
