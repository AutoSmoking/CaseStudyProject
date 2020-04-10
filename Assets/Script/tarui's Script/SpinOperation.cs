

// コントローラー切り替え
//#define XBOX
//#define PS4

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinOperation : MonoBehaviour
{
    // 操作系
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

    [SerializeField, Header("スティック系の感度"), Range(0, 1)]
    float stickSense = 0.5f;

    // Start is called before the first frame update

    [SerializeField, Header("回転の加速度"), Range(0, 10.0f)]
    float SpinAcceleration;

    [SerializeField, Header("回転速度の最大値"), Range(0, 10.0f)]
    float SpinMaxSpeed;

    [SerializeField, Header("回転停止後の滑る度合"), Range(0, 10.0f)]
    float SpinSlide;

    // 回転の現在速度
    [SerializeField]
    float SpinSpeed = 0;

    float StopSpin = 0;

    bool StopFlg = false;

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

    // Update is called once per frame
    void Update()
    {
        // 回転の挙動
        {
            if ((SpinSpeed >= 0)
#if (XBOX || PS4)
               && (((int)LeftSpin >= 10)
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
                 && (Input.GetKey(KeyCode.LeftArrow)))
#endif

            {
                SpinSpeed += SpinAcceleration * Time.deltaTime;

                t = 0.0f;
                StopFlg = false;
            }


            else if ((SpinSpeed <= 0)
#if (XBOX || PS4)
               && (((int)RightSpin >= 10)
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
                 && (Input.GetKey(KeyCode.RightArrow)))
#endif
            {
                SpinSpeed -= SpinAcceleration * Time.deltaTime;

                t = 0.0f;
                StopFlg = false;
            }

            else if (SpinSpeed != 0.0f)
            {
                if (!StopFlg)
                {
                    StopSpin = SpinSpeed;
                }

                StopFlg = true;

                SpinSpeed = Mathf.Lerp(StopSpin, 0.0f, t);

                t += SpinSlide * Time.deltaTime;
            }

            // 速度の制限
            SpinSpeed = Mathf.Clamp(SpinSpeed, -SpinMaxSpeed, SpinMaxSpeed);
        }

        // ワールドのy軸に沿って1秒間に90度回転
        transform.Rotate(new Vector3(0, 0, SpinSpeed), Space.World);
    }
}
