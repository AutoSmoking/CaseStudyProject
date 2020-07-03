using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeAnim : MonoBehaviour
{
    Animator anim;
    public bool isFadeIn = false;
    public bool isFadeOut = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isFadeOut)
        {
            anim.SetBool("FadeOut", true);
        }
        if (isFadeIn)
        {
            anim.SetBool("FadeIn", true);
        }

        if (isFadeIn || isFadeOut)
            if (
                (anim.GetCurrentAnimatorStateInfo(0).IsName("FadeOut") ||
                 anim.GetCurrentAnimatorStateInfo(0).IsName("FadeIn")) &&
                 anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1
                )
            {
                isFadeIn = false;
                isFadeOut = false;
                anim.SetBool("FadeOut", false);
                anim.SetBool("FadeIn", false);
            }
    }
}
