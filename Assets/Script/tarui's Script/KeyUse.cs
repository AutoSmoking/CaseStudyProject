using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUse : MonoBehaviour
{
    GameObject clear = null;

    bool clearFlg = false;

    float time;

    Vector3 start,end;
    Vector3 startRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!clearFlg && clear != null)
        {
            clearFlg = true;
            time = 0;
            start = this.transform.position;
            end = clear.transform.position;

            startRot = this.transform.eulerAngles;
        }

        if(clearFlg && time <= 1.0f)
        {
            time += Time.deltaTime;

            this.transform.position = Vector3.Lerp(start, end, time);
            this.transform.eulerAngles = Vector3.Lerp(startRot, Vector3.zero, time);
        }
    }

    public void Init(GameObject clearObj)
    {
        clear = clearObj;
    }
}
