

// コントローラー切り替え
//#define XBOX
//#define PS4

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinOperation : MonoBehaviour
{
    // 操作系
#if (XBOX)
    public enum Controll
    {
        Aボタン,Bボタン,Xボタン,Yボタン,
        L1ボタン,R1ボタン,BACKボタン,HOMEボタン,
        左スティック押し込み,右スティック押し込み,
        左スティック左右,左スティック上下,
        右スティック左右,右スティック上下,
        L2トリガー,R2トリガー,
        十字キー左右,十字キー上下
    }

    [SerializeField, Header("左回転用キー")]
    Controll LeftSpin;

    [SerializeField, Header("右回転用キー")]
    Controll RightSpin;
#else
    [SerializeField, Header("左回転用キー")]
    KeyCode LeftSpin = KeyCode.LeftArrow;

    [SerializeField, Header("右回転用キー")]
    KeyCode RightSpin = KeyCode.RightArrow;
#endif

    [SerializeField, Header("回転方向フラグ false:逆回転　true:正回転")]
    bool LRFlag = false;

    [SerializeField, Header("スティック系の感度"), Range(0, 1)]
    float stickSense = 0.5f;

    // Start is called before the first frame update

    [SerializeField, Header("回転の加速度"), Range(0.001f, 0.1f)]
    float SpinAcceleration = 0.001f;

    [SerializeField, Header("回転速度の最大値"), Range(0.001f, 1.0f)]
    float SpinMaxSpeed = 0.001f;

    [SerializeField, Header("回転停止後の滑る度合 大：滑らない　小：めっちゃ滑る"), Range(0.01f, 10.0f)]
    float SpinSlide = 0.01f;

    // 回転の現在速度
    [SerializeField]
    float SpinSpeed = 0;

    [SerializeField, Header("影響があるオブジェクト 入れないとバグ")]
    List<GameObject> StageObj = new List<GameObject>() { };

    [SerializeField, Header("動くオブジェクト専用 入れないとバグ")]
    List<GameObject> BubbleObj = new List<GameObject>() { };

    [SerializeField, Header("中心の海域の場合はtrueにしてください")]
    bool CenterFlg = false;

    //[SerializeField, Header("回転中に止めるオブジェクト")]
    //List<GameObject> stopObj = new List<GameObject>() { };

    float StopSpin = 0;

    bool SlideFlg = false;

    // 回転の停止を検知する
    public static bool stopFlg = false;

    float t = 0.0f;

    void Start()
    {
#if (XBOX || PS4)
        if (LeftSpin == RightSpin)
        {
            Debug.LogError("左右の回転に同じキーが割り当てられてるよ");
        }
#endif
    }

    private void FixedUpdate()
    {
        // 操作部分
        SpinControl();
    }

    // Update is called once per frame
    void OnTriggerStay(Collider collider)
    {
        // オブジェクトのアップデート
        SpinUpdate(collider);
    }

    // 回転が止まる前の滑る部分
    void SlideOpe()
    {
        if (!SlideFlg)
        {
            StopSpin = SpinSpeed;
        }

        SlideFlg = true;

        SpinSpeed = Mathf.Lerp(StopSpin, 0.0f, t);

        t += SpinSlide * Time.deltaTime;
    }

    // 操作部分
    void SpinControl()
    {
        if (
#if (XBOX || PS4)
                (((int)LeftSpin >= 10)
#endif
#if XBOX
                    && (Input.GetAxis(LeftSpin.ToString()) > stickSense)
#elif PS4
#else
#endif

#if (XBOX || PS4)
               || ((int)LeftSpin >= 0 && (int)LeftSpin < 10)
#endif
#if XBOX
                && (Input.GetButton(LeftSpin.ToString())))
#elif PS4
#else
                  (Input.GetKey(LeftSpin)))
#endif

        {
            if (stopFlg)
            {
                stopFlg = false;
            }

            if (LRFlag)
            {
                if (SpinSpeed >= 0)
                {
                    SpinSpeed += SpinAcceleration * Time.deltaTime;

                    t = 0.0f;
                    SlideFlg = false;
                }
                else
                {
                    SlideOpe();
                }
            }
            else
            {
                if (SpinSpeed <= 0)
                {
                    SpinSpeed -= SpinAcceleration * Time.deltaTime;

                    t = 0.0f;
                    SlideFlg = false;
                }
                else
                {
                    SlideOpe();
                }
            }
        }


        else if (
#if (XBOX || PS4)
                (((int)RightSpin >= 10)
#endif
#if XBOX
                    && (Input.GetAxis(RightSpin.ToString()) > stickSense)
#elif PS4
#else
#endif

#if (XBOX || PS4)
               || ((int)RightSpin >= 0 && (int)RightSpin < 10)
#endif
#if XBOX
                && (Input.GetButton(RightSpin.ToString())))
#elif PS4
#else
                  (Input.GetKey(RightSpin)))
#endif
        {

            if (stopFlg)
            {
                stopFlg = false;
            }

            if (LRFlag)
            {
                if (SpinSpeed <= 0)
                {
                    SpinSpeed -= SpinAcceleration * Time.deltaTime;

                    t = 0.0f;
                    SlideFlg = false;
                }
                else
                {
                    SlideOpe();
                }
            }
            else
            {
                if (SpinSpeed >= 0)
                {
                    SpinSpeed += SpinAcceleration * Time.deltaTime;

                    t = 0.0f;
                    SlideFlg = false;
                }
                else
                {
                    SlideOpe();
                }
            }
        }

        else if (SpinSpeed != 0.0f)
        {

            if (stopFlg)
            {
                stopFlg = false;
            }

            SlideOpe();
        }

        else if (SpinSpeed <= 0.01f && SpinSpeed >= -0.01f && stopFlg == false)
        {
            stopFlg = true;
        }


        // 速度の制限
        SpinSpeed = Mathf.Clamp(SpinSpeed, -SpinMaxSpeed, SpinMaxSpeed);
    }

    // 適用されてるオブジェクトを回転させる
    void SpinUpdate(Collider collider)
    {
        if (SpinSpeed != 0.0f)
        {
            bool BubbleFlg = false;

            foreach(var obj in BubbleObj)
            {
                if(obj == collider.gameObject)
                {
                    float r;
                    r = 0.5f * Mathf.Max(obj.transform.localScale.x, obj.transform.localScale.y, obj.transform.localScale.z);

                    if (CenterFlg ||
                        !CircleCollider2D(new Vector2(obj.transform.position.x, obj.transform.position.y),
                        r,
                        new Vector2(this.transform.position.x, this.transform.position.y),
                        (0.5f *
                        Mathf.Max(this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z) / 2 - r * 2 + 0.1f)))
                    {
                        SpinMath(obj);
                    }

                    BubbleFlg = true;
                    break;
                }
            }

            if (!BubbleFlg)
            {
                foreach (var obj in StageObj)
                {
                    if (obj == collider.gameObject)
                    {
                        /* 回転の挙動 */
                        SpinMath(obj);

                        break;
                    }
                }
            }
        }
    }

    void SpinMath(GameObject obj)
    {
        // ステージの中心から対象のオブジェクトまでの長さ
        float X, Y;
        float len;
        // ステージの中心から対象のオブジェクトまでの角度
        float theta;
        // 計算結果位置
        Vector3 pos = new Vector3();

        X = obj.transform.position.x - this.transform.position.x;
        Y = obj.transform.position.y - this.transform.position.y;
        len = Mathf.Sqrt(X * X + Y * Y);
        theta = Mathf.Atan2(Y, X);


        //float len2; // 直径
        //len2 = len * 2;

        //float N;    // 回転数[spm]
        //N = (SpinSpeed * Time.deltaTime) / (Mathf.PI * len2) / 60.0f;

        //float rad;
        //rad = N * 2 * Mathf.PI;

        //theta += rad;
        theta += SpinSpeed;

        pos.x = len * Mathf.Cos(theta) + this.transform.position.x;
        pos.y = len * Mathf.Sin(theta) + this.transform.position.y;
        pos.z = obj.transform.position.z;

        obj.transform.position = pos;



        // ワールドのz軸に沿って SpinSpeed 分回転
        //obj.transform.Rotate(new Vector3(0, 0, theta));

        obj.transform.Rotate(0, 0, (SpinSpeed * Mathf.Rad2Deg));
    }

    bool CircleCollider2D(Vector2 posA,float radA,Vector2 posB,float radB)
    {
        float a = posA.x - posB.x;
        float b = posA.y - posB.y;
        float c = Mathf.Sqrt(a * a + b * b);

        if (c <= radA + radB)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //勝手に追加しました
    public static bool GetstopFlg()
    {
        return stopFlg;
    }
}
