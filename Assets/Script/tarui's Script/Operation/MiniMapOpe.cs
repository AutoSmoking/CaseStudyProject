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
    Transform treasureUI = null;

    [SerializeField, Header("泡用のUI")]
    List<Transform> BubblesUI = new List<Transform>() { };

    // 宝箱
    Transform treasure = null;

    // 泡
    List<GameObject> Bubbles = new List<GameObject>() { };

    // Start is called before the first frame update
    void Start()
    {
        // 宝箱を格納
        treasure = GameObject.FindGameObjectWithTag("Finish").transform;

        // 泡を格納
        Bubbles.Add(GameObject.FindGameObjectWithTag("1"));
        Bubbles.Add(GameObject.FindGameObjectWithTag("2"));
        Bubbles.Add(GameObject.FindGameObjectWithTag("3"));

        if(GameObject.FindGameObjectWithTag("2") == null)
        {
            BubblesUI[1].gameObject.SetActive(false);
        }
        if (GameObject.FindGameObjectWithTag("3") == null)
        {
            BubblesUI[2].gameObject.SetActive(false);
        }

        List<GameObject> stage = new List<GameObject>() { };

        stage.AddRange(GameObject.FindGameObjectsWithTag("stage"));
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MiniMapFunction(treasure, treasureUI);

        for (int i = 0; i < Bubbles.Count - 1; i++)  
        {
            if(Bubbles[i] == null)
            {
                if (BubblesUI[i].gameObject.activeInHierarchy)
                {
                    BubblesUI[i].gameObject.SetActive(false);
                }
                continue;
            }

            MiniMapFunction(Bubbles[i].transform, BubblesUI[i]);
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

                pos = pos / (17.0f / 2.0f) * 400.0f / 2.0f;
                break;

            case SeaArea.中:

                pos = pos / (17.0f / 2.0f) * 400.0f / 2.0f;
                break;

            case SeaArea.大:

                pos = pos / (25.0f / 2.0f) * 400.0f / 2.0f;
                break;

            default:
                pos = Vector2.zero;
                break;
        }

        len = Mathf.Sqrt(pos.x * pos.x + pos.y * pos.y);

        pos2.x = len * Mathf.Cos(theta) + this.transform.position.x;
        pos2.y = len * Mathf.Sin(theta) + this.transform.position.y;
        pos2.z = OutTrans.localPosition.z;

        OutTrans.localPosition = pos2;
    }
}
