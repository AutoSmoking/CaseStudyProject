using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightPos : MonoBehaviour
{
    [SerializeField]
    Transform fish = null;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localPosition = fish.localPosition + new Vector3(0, 0, -12.0f);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = fish.localPosition + new Vector3(0, 0, -12.0f);
    }
}
