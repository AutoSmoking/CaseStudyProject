using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBlast : MonoBehaviour
{
    Vector3 firstScale = Vector3.zero;
    Vector3 endScale = Vector3.zero;

    float time = 0.75f;
    
    float percent = 0;

    // Start is called before the first frame update
    void Start()
    {
        firstScale = this.transform.localScale;

        endScale = firstScale * 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float Wait = 0;

        percent += Time.deltaTime;

        Wait = percent / time;

        this.transform.localScale = Vector3.Lerp(firstScale, endScale, Wait);

        if(Wait >= 1)
        {
            Destroy(this.gameObject);
        }
    }
}
