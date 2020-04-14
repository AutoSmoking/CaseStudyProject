using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusCollider : MonoBehaviour
{
    // 分割数
    [SerializeField,Header("分割数(SphereColliderの数)"),Range(1,3000)]
    int splitNum;

    // 半径
    [SerializeField, Header("半径(中心からSphereColliderまでの距離)"), Range(0, 100)]
    float R;
    
    [SerializeField]
    GameObject obj;

    // Start is called before the first frame update
    void Awake()
    {
        // リジットボディ追加
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        if (rigidbody == null)
        {
            rigidbody = this.gameObject.AddComponent<Rigidbody>();
        }
        rigidbody.useGravity = false;
        rigidbody.isKinematic = true;
        
        for (int i = 0; i < splitNum; i++) 
        {
            Instantiate(obj,
                new Vector3((R * Mathf.Cos(360.0f / splitNum * i * Mathf.PI / 180.0f) + this.transform.position.x),
                    (R * Mathf.Sin(360.0f / splitNum * i * Mathf.PI / 180.0f) + this.transform.position.y),
                     this.transform.position.z),
                Quaternion.Euler(0, 0, (360.0f / splitNum * i)),
                this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
