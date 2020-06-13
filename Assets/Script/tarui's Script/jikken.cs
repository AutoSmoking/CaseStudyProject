using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jikken : MonoBehaviour
{
    BubbleFadeOpe fade;

    // Start is called before the first frame update
    void Start()
    {
        fade = this.GetComponent<BubbleFadeOpe>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            fade.isFadeIn = true;
        }

        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            fade.isFadeOut = true;
        }
    }
}
