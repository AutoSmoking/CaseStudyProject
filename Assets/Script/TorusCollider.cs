using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorusCollider : MonoBehaviour
{
    // 分割数
    [SerializeField,Header("分割数(SphereColliderの数)"),Range(1,100)]
    int splitNum;

    // 太さ
    [SerializeField, Header("太さ(SphereColliderの大きさ)"), Range(0.0f, 10.0f)]
    float thickR;

    // 半径
    [SerializeField, Header("半径(中心からSphereColliderまでの距離)"), Range(0, 100)]
    float R;


    // Start is called before the first frame update
    void Start()
    {
        SphereCollider sphere;

        for (int i = 0; i < splitNum; i++) 
        {
            sphere = this.gameObject.AddComponent<SphereCollider>();

            sphere.center = new Vector3(R * Mathf.Cos(360.0f / splitNum * i * Mathf.PI / 180.0f),
                R * Mathf.Sin(360.0f / splitNum * i * Mathf.PI / 180.0f), 0);

            sphere.radius = thickR;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
