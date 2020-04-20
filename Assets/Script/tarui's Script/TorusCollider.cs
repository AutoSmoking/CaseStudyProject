using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusCollider : MonoBehaviour
{
    enum ColType
    {
        Box,
        Sphere,
        Capsule,
    };

    // 分割数
    [SerializeField,Header("分割数(SphereColliderの数)"),Range(1,3000)]
    int splitNum;

    // 半径
    [SerializeField, Header("半径(中心からSphereColliderまでの距離)"), Range(0, 100)]
    float R;

    [SerializeField, Header("空のオブジェクトを入れてください")]
    GameObject Obj;

    [SerializeField]
    ColType type;

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

        GameObject InsObj;
        
        for (int i = 0; i < splitNum; i++) 
        {
            InsObj = Instantiate(Obj,
                new Vector3((R * Mathf.Cos(360.0f / splitNum * i * Mathf.PI / 180.0f) + this.transform.position.x),
                    (R * Mathf.Sin(360.0f / splitNum * i * Mathf.PI / 180.0f) + this.transform.position.y),
                     this.transform.position.z),
                Quaternion.Euler(0, 0, (360.0f / splitNum * i)),
                this.transform);

            switch(type)
            {
                case ColType.Box:
                    {
                        BoxCollider box;

                        box = InsObj.AddComponent<BoxCollider>();

                        box.isTrigger = true;

                        break;
                    }

                case ColType.Capsule:
                    {
                        CapsuleCollider capsule;

                        capsule = InsObj.AddComponent<CapsuleCollider>();

                        capsule.isTrigger = true;

                        break;
                    }

                case ColType.Sphere:
                    {
                        SphereCollider sphere;

                        sphere = InsObj.AddComponent<SphereCollider>();

                        sphere.isTrigger = true;

                        break;
                    }

                default:
                    break;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
