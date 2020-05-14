using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObject : MonoBehaviour
{
    // 中心座標
    [SerializeField]
    Transform Center = null;

    // 回転速度
    [SerializeField]
    float spd = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // ステージの中心から対象のオブジェクトまでの長さ
        float X, Y;
        float len;
        // ステージの中心から対象のオブジェクトまでの角度
        float theta;
        // 計算結果位置
        Vector3 pos = new Vector3();

        X = this.transform.position.x - Center.position.x;
        Y = this.transform.position.y - Center.position.y;
        len = Mathf.Sqrt(X * X + Y * Y);
        theta = Mathf.Atan2(Y, X);

        theta += spd * Time.deltaTime;

        pos.x = len * Mathf.Cos(theta) + Center.position.x;
        pos.y = len * Mathf.Sin(theta) + Center.position.y;
        pos.z = this.transform.position.z;

        this.transform.position = pos;
    }
}
