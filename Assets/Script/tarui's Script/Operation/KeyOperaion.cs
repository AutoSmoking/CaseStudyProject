using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOperaion : MonoBehaviour
{
    enum KeyType
    {
        二分割,三分割
    }

    KeyType keyType = KeyType.二分割;

    private int keyIndex = 0;
    public int KeyIndex
    {
        get
        {
            return keyIndex;
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
