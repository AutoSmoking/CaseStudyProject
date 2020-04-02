using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallBlock : MonoBehaviour
{
    [SerializeField, Header("↓向きを示す場所")]
    Transform UnderPos;

    [SerializeField, Header("速度(0～1までの比率)"),Range(0, 1)]
    float spd;

    // 加速度的な
    float t;

    // 初期位置
    Vector3 firstPos;

    // 下に向かうタイミングを制御するフラグ（他のスクリプトで制御できるように）
    public bool UnderFlg;

    // Start is called before the first frame update
    void Awake()
    {
        firstPos = this.transform.position;

        UnderFlg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (UnderFlg)
        {
            t += spd * Time.deltaTime;
            if (t > 1) 
            {
                t = 1;
            }

            // ↓方向に向かって進む
            this.transform.position = Vector3.Lerp(firstPos, UnderPos.position, t);
        }
    }
}
