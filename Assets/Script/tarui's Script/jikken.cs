using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jikken : MonoBehaviour
{
    [SerializeField]
    GameObject obj;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            KeyUse use;
            use = obj.transform.GetChild(0).gameObject.AddComponent<KeyUse>();

            use.Init(this.gameObject);
        }
    }
}
