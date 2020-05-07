using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMove : MonoBehaviour
{
    public enum MoveType
    {
        直進,回転
    }

    public MoveType moveType;

    /* ステータス */
    public float MoveSpd;  // 速さ

    /* どこに向かって直進するか
     * 又は、どの軸を使って回転するか */
    public enum Axis
    {
        X軸,Y軸,Z軸
    }
    public Axis axis;
    // 軸の前後どちらに進むのか true:正方向　false:逆方向
    public bool LRFlag;

    // 中心座標(回転専用)
    public Vector3 centerPos;

    /* ステータス　ここまで */

    // ターンするまでの時間
    public float TurnTime;

    bool TurnFlag = false;

    float percent = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TurnFlag)
        {
            if(percent >= TurnTime)
            {
                percent = 0;
                TurnFlag = false;
            }
            else
            {
                percent += Time.deltaTime;

                if(axis == Axis.X軸)
                {

                }
                else if (axis == Axis.Y軸)
                {

                }
                else
                {

                }
            }
        }
        else
        {
            if(moveType == MoveType.回転)
            {
                RotMove();
            }
            else
            {
                StraightMove();
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other)
        {
            TurnFlag = true;
        }
    }

    void RotMove()
    {
        // ステージの中心から対象のオブジェクトまでの長さ
        float X, Y;
        float len;
        // ステージの中心から対象のオブジェクトまでの角度
        float theta;
        // 計算結果位置
        Vector3 pos = new Vector3();

        if (axis == Axis.X軸)
        {
            X = this.transform.position.x - centerPos.x;
            Y = this.transform.position.y - centerPos.y;
        }
        else if (axis == Axis.Y軸)
        {
            X = this.transform.position.x - centerPos.x;
            Y = this.transform.position.y - centerPos.y;
        }
        else
        {
            X = this.transform.position.x - centerPos.x;
            Y = this.transform.position.y - centerPos.y;
        }

        len = Mathf.Sqrt(X * X + Y * Y);
        theta = Mathf.Atan2(Y, X);


        float len2; // 直径
        len2 = len * 2;

        float N;    // 回転数[spm]
        N = (MoveSpd * Time.deltaTime) / (Mathf.PI * len2) / 60.0f;

        float rad;
        rad = N * 2 * Mathf.PI;

        theta += rad;

        if (axis == Axis.X軸)
        {
            pos.x = this.transform.position.x;
            pos.y = len * Mathf.Cos(theta) + centerPos.y;
            pos.z = len * Mathf.Sin(theta) + centerPos.z;
        }
        else if (axis == Axis.Y軸)
        {
            pos.x = len * Mathf.Sin(theta) + centerPos.x;
            pos.y = this.transform.position.z;
            pos.z = len * Mathf.Cos(theta) + centerPos.z;
        }
        else
        {
            pos.x = len * Mathf.Cos(theta) + centerPos.x;
            pos.y = len * Mathf.Sin(theta) + centerPos.y;
            pos.z = this.transform.position.z;
        }

        this.transform.position = pos;
    }

    void StraightMove()
    {
        int sign;

        if(LRFlag)
        {
            sign = 1;
        }
        else
        {
            sign = -1;
        }

        if (axis == Axis.X軸)
        {
            this.transform.position +=
                new Vector3(MoveSpd * Time.deltaTime, 0, 0) * (float)sign;
        }
        else if (axis == Axis.Y軸)
        {
            this.transform.position +=
                new Vector3(0, MoveSpd * Time.deltaTime, 0) * (float)sign;
        }
        else
        {
            this.transform.position +=
                new Vector3(0, 0, MoveSpd * Time.deltaTime) * (float)sign;
        }
    }
}
