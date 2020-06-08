﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlphaChange : MonoBehaviour
{
    [SerializeField, Header("アルファ値変化量")]
    float Amount;
    [SerializeField, Header("アルファ値最大時の待機時間")]
    float WaitTime;
    Image AButton = null;
    bool AlphaTrg = false;
    float time = 0.0f;
        // Start is called before the first frame update
    void Start()
    {
        AButton = gameObject.GetComponent<Image>();   
    }

    // Update is called once per frame
    void Update()
    {
        if (AlphaTrg)
        {
            AButton.color+=new Color(0.0f, 0.0f, 0.0f, 0.05f);
            if (AButton.color.a >= 1)
            {
                time += Time.deltaTime;
                if (time >= 0.5f)
                {
                    AlphaTrg = false;
                    time = 0;
                }
            }
        }
        else
        {
            AButton.color -= new Color(0.0f, 0.0f, 0.0f, 0.05f);
            if (AButton.color.a <= 0.0f) 
            {
                AlphaTrg = true;
            }
        }
    }
}