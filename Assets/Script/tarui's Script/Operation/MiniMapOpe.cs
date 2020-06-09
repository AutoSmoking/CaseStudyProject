using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapOpe : MonoBehaviour
{
    enum SeaArea
    {
        小, 中, 大
    }

    [SerializeField, Header("海域の最大の大きさ")]
    SeaArea seaArea = SeaArea.中;

    [SerializeField, Header("宝箱用のUI")]
    Transform UIT = null;

    [SerializeField, Header("泡用のUI")]
    Transform UIB = null;

    [SerializeField, Header("ハリセンボン用のUI")]
    Transform UIF = null;

    [SerializeField, Header("シャコガイ用のUI")]
    Transform UIS = null;

    // 宝箱
    Transform treasure = null;
    // UI用
    GameObject treasureUI = null;

    // 泡
    List<GameObject> Bubbles = new List<GameObject>() { };
    // UI用
    List<GameObject> BubblesUI = new List<GameObject>() { };


    // ハリセンボン
    List<GameObject> NeedleFish = new List<GameObject>() { };
    // UI用
    List<GameObject> NeedleFishsUI = new List<GameObject>() { };


    // シャコガイ
    List<GameObject> syako = new List<GameObject>() { };
    // UI用
    List<GameObject> syakoUI = new List<GameObject>() { };

    // Start is called before the first frame update
    void Start()
    {
        // 宝箱を格納
        treasure = GameObject.FindGameObjectWithTag("Finish").transform;

        // 泡を格納
        Bubbles.Add(GameObject.FindGameObjectWithTag("1"));
        Bubbles.Add(GameObject.FindGameObjectWithTag("2"));
        Bubbles.Add(GameObject.FindGameObjectWithTag("3"));

        // ハリセンボンを格納
        NeedleFish.AddRange(GameObject.FindGameObjectsWithTag("fish"));

        // シャコガイを格納
        syako.AddRange(GameObject.FindGameObjectsWithTag("warp"));
        
        List<GameObject> stage = new List<GameObject>() { };

        stage.AddRange(GameObject.FindGameObjectsWithTag("stage"));

        // 泡のUIを生成
        for (int i = 0; i < Bubbles.Count; i++)
        {
            BubblesUI.Add(GameObject.Instantiate(UIB.gameObject, this.transform));
        }

        // ハリセンボンのUIを生成
        for (int i = 0; i < NeedleFish.Count; i++)
        {
            NeedleFishsUI.Add(GameObject.Instantiate(UIF.gameObject, this.transform));
        }

        // 宝箱のUIを生成
        treasureUI = GameObject.Instantiate(UIT.gameObject, this.transform);

        // シャコガイのUIを生成
        for (int i = 0; i < syako.Count; i++)
        {
            syakoUI.Add(GameObject.Instantiate(UIS.gameObject, this.transform));
            syakoUI[i].GetComponent<warpOpenTexture>().warp = syako[i];
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // 宝箱
        MiniMapFunction(treasure, treasureUI.transform);

        // 泡
        for (int i = 0; i < Bubbles.Count; i++)  
        {
            if(Bubbles[i] == null)
            {
                if (BubblesUI[i].gameObject.activeInHierarchy)
                {
                    BubblesUI[i].gameObject.SetActive(false);
                }
                continue;
            }

            MiniMapFunction(Bubbles[i].transform, BubblesUI[i].transform);
        }

        // ハリセンボン
        for (int i = 0; i < NeedleFish.Count; i++)
        {
            if (NeedleFish[i] == null)
            {
                if (NeedleFishsUI[i].gameObject.activeInHierarchy)
                {
                    NeedleFishsUI[i].gameObject.SetActive(false);
                }
                continue;
            }

            MiniMapFunction(NeedleFish[i].transform, NeedleFishsUI[i].transform);
        }

        // シャコガイ
        for (int i = 0; i < syako.Count; i++)
        {
            if (syako[i] == null)
            {
                if (syakoUI[i].gameObject.activeInHierarchy)
                {
                    syakoUI[i].gameObject.SetActive(false);
                }
                continue;
            }

            MiniMapFunction(syako[i].transform, syakoUI[i].transform);
        }
    }

    void MiniMapFunction(Transform InTrans,Transform OutTrans)
    {
        // ステージの中心から対象のオブジェクトまでの長さ
        Vector2 pos;
        float len;
        // ステージの中心から対象のオブジェクトまでの角度
        float theta;
        // 計算結果位置
        Vector3 pos2 = Vector3.zero;

        pos.x = InTrans.position.x;
        pos.y = InTrans.position.y;
        theta = Mathf.Atan2(pos.y, pos.x);

        switch (seaArea)
        {
            case SeaArea.小:

                pos = pos / (17.5f / 2.0f) * 400.0f / 2.0f;
                break;

            case SeaArea.中:

                pos = pos / (17.5f / 2.0f) * 400.0f / 2.0f;
                break;

            case SeaArea.大:

                pos = pos / (25.5f / 2.0f) * 400.0f / 2.0f;
                break;

            default:
                pos = Vector2.zero;
                break;
        }

        len = Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y);

        pos2.x = len * Mathf.Cos(theta);
        pos2.y = len * Mathf.Sin(theta);
        pos2.z = OutTrans.localPosition.z;

        OutTrans.localPosition = pos2;
    }
}
