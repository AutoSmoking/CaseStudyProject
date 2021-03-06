﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class warpOpenTexture : MonoBehaviour
{
    [System.NonSerialized]
    public GameObject warp = null;

    warpOperation warpOperation = null;
    bool isNoNull = false;

    [SerializeField]
    Sprite open = null;
    [SerializeField]
    Sprite close = null;

    Image Image = null;

    Material material = null;

    // Start is called before the first frame update
    void Start()
    {
        Image = this.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isNoNull)
        {
            if (!warpOperation.GoalFlg)
            {
                Image.sprite = open;
            }
            else
            {
                Image.sprite = close;
            }

            Image.color = material.color;
        }
        else
        {
            if (warp != null)
            {
                warpOperation = warp.GetComponent<warpOperation>();

                material = warpOperation.transform.GetChild(0).GetChild(1).GetComponent<SkinnedMeshRenderer>().material;

                isNoNull = true;
            }
        }

    }
}
