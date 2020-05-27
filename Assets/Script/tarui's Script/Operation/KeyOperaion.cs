using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOperaion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        // 泡にぶつかった場合
        if(other.gameObject.layer == LayerMask.NameToLayer("Bubble"))
        {
            // 泡から大きさを取ってくる
            other.GetComponent<BubbleOperation>();

            // 大きさによって処理が変わる
            // if(??? >= 2)

            // 泡の子オブジェクトに鍵を入れる
            this.transform.parent = other.transform;
        }
    }
}
