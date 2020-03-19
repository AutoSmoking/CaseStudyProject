
// デバッグ用のログを出すときはコメントアウトしてね
//#define RELEASE

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeaArea : MonoBehaviour
{ 
    [SerializeField, Header("回転方向フラグ false:左　true:右")]
    bool LRFlag;

    [SerializeField, Header("影響があるオブジェクト(多分泡かな) 入れないとバグ")]
    GameObject obj;

    [SerializeField, Header("回転速度"), Range(0, 30)]
    float spd;

    // Start is called before the first frame update
    void Start()
    {
#if RELEASE
#else
        if (obj == null)
        {
            Debug.Log(this.name + "の Obj に値が入ってないYO！");
        }
        if(spd == 0)
        {
            Debug.Log(this.name + "の spd の値が０だけど大丈夫？");
        }
#endif
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
