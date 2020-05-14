using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArchCollider : MonoBehaviour
{
    // 分割数
    [SerializeField, Header("分割数(SphereColliderの数)"), Range(1, 3000)]
    int splitNum = 1;

    // 半径
    [SerializeField, Header("半径(中心からSphereColliderまでの距離)"), Range(0, 100)]
    float R = 0.0f;

    // 弧
    [SerializeField,Header("弧の度数"),Range(0, 360)]
    float MaxRot = 0.0f;

    [SerializeField]
    GameObject obj = null;

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
        rigidbody.isKinematic = false;

        for (int i = 0; i <= splitNum; i++)
        {
            Instantiate(obj,
                new Vector3(R * Mathf.Cos(MaxRot / splitNum * i * Mathf.PI / 180.0f),
                    R * Mathf.Sin(MaxRot / splitNum * i * Mathf.PI / 180.0f), 0),
                Quaternion.identity,
                this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
