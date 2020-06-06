using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TaruSpin : MonoBehaviour
{
    Transform bubble = null;
    Controll AButton = Controll.Aボタン;

    // Start is called before the first frame update
    void Start()
    {
        bubble = this.transform.parent.Find("Bubble");
    }

    // Update is called once per frame
    void Update()
    {
        Spin();

        if((Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown(AButton.ToString())))
        {
            this.enabled = false;
        }
    }

    void Spin()
    {
        this.transform.eulerAngles = new Vector3(0, 0, 0);

        // ステージの中心から対象のオブジェクトまでの長さ
        Vector2 wapos,lopos;
        float len;
        // ステージの中心から対象のオブジェクトまでの角度
        float theta;
        // 計算結果位置
        Vector3 pos = new Vector3();
        
        lopos = bubble.transform.localPosition - this.transform.localPosition;

        len = Mathf.Sqrt(lopos.x * lopos.x + lopos.y * lopos.y);

        theta = (this.transform.localEulerAngles.z + 90.0f) * Mathf.Deg2Rad;

        pos.x = len * Mathf.Cos(theta) + this.transform.localPosition.x;
        pos.y = len * Mathf.Sin(theta) + this.transform.localPosition.y;
        pos.z = bubble.transform.localPosition.z;

        bubble.transform.localPosition = pos;
    }
}
