using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedleCollider : MonoBehaviour
{

    const float time = 0.97025f;

    CapsuleCollider capsule;

    bool scaleFlg = false;

    float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        capsule = this.GetComponent<CapsuleCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(scaleFlg)
        {
            t += Time.deltaTime;

            float percent = t / time;

            capsule.height = Mathf.Lerp(2.0f, 1.0f, percent);
            capsule.radius = Mathf.Lerp(0.7f, 0.3f, percent);

            if (t >= time)
            {
                t = 0;
                scaleFlg = !scaleFlg;
            }
        }
        else
        {
            t += Time.deltaTime;

            float percent = t / time;

            capsule.height = Mathf.Lerp(1.0f, 2.0f, percent);
            capsule.radius = Mathf.Lerp(0.3f, 0.7f, percent);

            if (t >= time) 
            {
                t = 0;
                scaleFlg = !scaleFlg;
            }
        }
    }
}
