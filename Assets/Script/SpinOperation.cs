using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinOperation : MonoBehaviour
{
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

    }

    // Update is called once per frame
    void Update()
    {
        // 回転の挙動
        {
            if (Input.GetKey(KeyCode.LeftArrow) && SpinSpeed >= 0)
            {
                SpinSpeed += SpinAcceleration * Time.deltaTime;

                t = 0.0f;
                StopFlg = false;
            }
            else if (Input.GetKey(KeyCode.RightArrow) && SpinSpeed <= 0)
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