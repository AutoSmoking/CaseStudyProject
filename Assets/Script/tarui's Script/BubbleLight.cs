using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLight : MonoBehaviour
{
    Light light = null;

    Transform bubble;

    float spotAngle = 0;

    // Start is called before the first frame update
    void Start()
    {
        light = this.GetComponent<Light>();

        bubble = this.transform.parent;

        spotAngle = light.spotAngle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        light.spotAngle = bubble.localScale.x * spotAngle;
    }
}
