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
    public Axis MoveAxis;

    // 軸の前後どちらに進むのか true:正方向　false:逆方向
    public bool LRFlag;

    // 中心座標(回転専用)
    public Transform centerPos;

    /* ステータス　ここまで */

    // ターンするまでの時間
    public float TurnTime;

    bool TurnFlag = false;

    float percent = 0;
    
    Vector3 FirstCAngle;
    Vector3 EndCAngle;

    // ターン用の子オブジェクト
    Transform child = null;

    // 当たり判定のバグ取り用
    float WaitTime = 0;

    //[SerializeField]
    float ColStopTime = 0.1f;

    // Start is called before the first frame update
    void Awake()
    {
        // ステージの中心から対象のオブジェクトまでの長さ
        float X, Y;
        float len;
        // ステージの中心から対象のオブジェクトまでの角度
        float theta;

        if (MoveAxis == Axis.X軸)
        {
            X = this.transform.position.y - centerPos.position.y;
            Y = this.transform.position.z - centerPos.position.z;
        }
        else if (MoveAxis == Axis.Y軸)
        {
            X = this.transform.position.z - centerPos.position.z;
            Y = this.transform.position.x - centerPos.position.x;
        }
        else
        {
            X = this.transform.position.x - centerPos.position.x;
            Y = this.transform.position.y - centerPos.position.y;
        }

        len = Mathf.Sqrt(X * X + Y * Y);
        theta = Mathf.Atan2(Y, X);

        float angle = theta * Mathf.Rad2Deg;
        float rot = Mathf.Lerp(angle, angle - 180.0f, 0.5f);

        if (MoveAxis == Axis.X軸)
        {
            if (LRFlag)
            {
                this.transform.eulerAngles = new Vector3(-rot, 180.0f, 0);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(rot, 0, 0);
            }
        }
        else if (MoveAxis == Axis.Y軸)
        {
            if (LRFlag)
            {
                this.transform.eulerAngles = new Vector3(0, -rot, 180.0f);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0, rot, 0);
            }
        }
        else
        {
            if (LRFlag)
            {
                this.transform.eulerAngles = new Vector3(180, 0, -rot);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0, 0, rot);
            }
        }

        // 子オブジェクトを取得
        child = this.transform.GetChild(0);

        // 角度を調整
        if(LRFlag)
        {
            child.transform.localEulerAngles = new Vector3(0, 180.0f);
        }
        else
        {
            child.transform.localEulerAngles = Vector3.zero;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(TurnFlag)
        {
            if (percent >= TurnTime)
            {
                percent = 0;
                TurnFlag = false;
                FirstCAngle = new Vector3();
                EndCAngle = new Vector3();
                WaitTime = 0;

                if (LRFlag) child.transform.localEulerAngles = Vector3.zero;

                LRFlag = !LRFlag;
            }
            else
            {
                percent += Time.deltaTime;

                float Wait = percent / TurnTime;

                Vector3 endAngle = Vector3.zero;

                // ステージの中心から対象のオブジェクトまでの長さ
                float X, Y;
                // ステージの中心から対象のオブジェクトまでの角度
                float theta;

                if (MoveAxis == Axis.X軸)
                {
                    X = this.transform.position.y - centerPos.position.y;
                    Y = this.transform.position.z - centerPos.position.z;
                }
                else if (MoveAxis == Axis.Y軸)
                {
                    X = this.transform.position.z - centerPos.position.z;
                    Y = this.transform.position.x - centerPos.position.x;
                }
                else
                {
                    X = this.transform.position.x - centerPos.position.x;
                    Y = this.transform.position.y - centerPos.position.y;
                }
                
                theta = Mathf.Atan2(Y, X);
                
                float rot;

                if (!LRFlag)
                {
                    float angle = theta * Mathf.Rad2Deg;
                    rot = Mathf.Lerp(angle, angle + 180.0f, 0.5f);
                }
                else
                {
                    float angle = theta * Mathf.Rad2Deg;
                    rot = Mathf.Lerp(angle, angle - 180.0f, 0.5f);
                }


                if (MoveAxis == Axis.X軸)
                {
                    if (!LRFlag)
                    {
                        endAngle = new Vector3(180, 90, -rot - 90);
                    }
                    else
                    {
                        endAngle = new Vector3(0, 90, rot + 90);
                    }
                }
                else if (MoveAxis == Axis.Y軸)
                {
                    if (!LRFlag)
                    {
                        endAngle = new Vector3(90, rot - 90.0f, 0);
                    }
                    else
                    {
                        endAngle = new Vector3(-90, rot - 90.0f, 0);
                    }
                }
                else
                {
                    if (!LRFlag)

                    {
                        endAngle = new Vector3(180, 0, -rot);
                    }
                    else
                    {
                        endAngle = new Vector3(0, 0, rot);
                    }
                }


                this.transform.eulerAngles = endAngle;

                child.transform.localEulerAngles =
                    Vector3.Lerp(Vector3.zero, new Vector3(0, 180.0f), Wait);
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

            if(WaitTime <= ColStopTime)
            {
                WaitTime += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Block" && !TurnFlag && WaitTime > ColStopTime)
        {
            TurnFlag = true;
            FirstCAngle = child.transform.localEulerAngles;
            EndCAngle = new Vector3(FirstCAngle.x, FirstCAngle.y + 180.0f, FirstCAngle.z);
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

        if (MoveAxis == Axis.X軸)
        {
            X = this.transform.position.y - centerPos.position.y;
            Y = this.transform.position.z - centerPos.position.z;
        }
        else if (MoveAxis == Axis.Y軸)
        {
            X = this.transform.position.z - centerPos.position.z;
            Y = this.transform.position.x - centerPos.position.x;
        }
        else
        {
            X = this.transform.position.x - centerPos.position.x;
            Y = this.transform.position.y - centerPos.position.y;
        }

        len = Mathf.Sqrt(X * X + Y * Y);
        theta = Mathf.Atan2(Y, X);


        float len2; // 直径
        len2 = len * 2;

        float N;    // 回転数[spm]
        N = (MoveSpd * Time.deltaTime) / (Mathf.PI * len2) / 60.0f;

        float rad;
        rad = N * 2 * Mathf.PI;

        float rot;

        if (LRFlag)
        {
            theta -= rad;
            float angle = theta * Mathf.Rad2Deg;
            rot = Mathf.Lerp(angle, angle + 180.0f, 0.5f);
        }
        else
        {
            theta += rad;
            float angle = theta * Mathf.Rad2Deg;
            rot = Mathf.Lerp(angle, angle - 180.0f, 0.5f);
        }


        if (MoveAxis == Axis.X軸)
        {
            pos.x = this.transform.position.x;
            pos.y = len * Mathf.Cos(theta) + centerPos.position.y;
            pos.z = len * Mathf.Sin(theta) + centerPos.position.z;

            if (LRFlag)
            {
                this.transform.eulerAngles = new Vector3(180, 90, -rot - 90);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0, 90, rot + 90);
            }
        }
        else if (MoveAxis == Axis.Y軸)
        {
            pos.x = len * Mathf.Sin(theta) + centerPos.position.x;
            pos.y = this.transform.position.y;
            pos.z = len * Mathf.Cos(theta) + centerPos.position.z;

            if (LRFlag)
            {
                this.transform.eulerAngles = new Vector3(90, rot - 90.0f, 0);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(-90, rot - 90.0f, 0);
            }
        }
        else
        {
            pos.x = len * Mathf.Cos(theta) + centerPos.position.x;
            pos.y = len * Mathf.Sin(theta) + centerPos.position.y;
            pos.z = this.transform.position.z;

            if (LRFlag)
            {
                this.transform.eulerAngles = new Vector3(180, 0, -rot);
            }
            else
            {
                this.transform.eulerAngles = new Vector3(0, 180, -rot);
            }
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

        if (MoveAxis == Axis.X軸)
        {
            this.transform.position +=
                new Vector3(MoveSpd * Time.deltaTime, 0, 0) * (float)sign;
        }
        else if (MoveAxis == Axis.Y軸)
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
