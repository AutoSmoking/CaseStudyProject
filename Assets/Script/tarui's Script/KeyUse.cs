using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyUse : MonoBehaviour
{
    GameObject clear = null;

    bool moveFlg = false;
    bool openFlg = false;

    float time;
    float time2;
    float count;

    Vector3 start,end;
    Vector3 startRot, endRot;

    Vector3 rot = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.parent.GetComponent<BubbleOperation>().enabled = false;
        this.transform.parent.GetComponent<Rigidbody>().isKinematic = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!moveFlg && clear != null)
        {
            moveFlg = true;
            time = 0;
            start = this.transform.position;
            end = clear.transform.position;
            end.z -= 2.0f;

            startRot = this.transform.eulerAngles;
            endRot = new Vector3(0, -90, 90);
        }

        if(moveFlg && time <= 1.0f)
        {
            time += Time.deltaTime;

            this.transform.position = Vector3.Lerp(start, end, time);

            rot.x = Mathf.LerpAngle(startRot.x, endRot.x, time);
            rot.y = Mathf.LerpAngle(startRot.y, endRot.y, time);
            rot.z = Mathf.LerpAngle(startRot.z, endRot.z, time);
            this.transform.eulerAngles = rot;

            if(time >= 1.0f)
            {
                openFlg = true;
                count = 0;
                time2 = 0;

                startRot = this.transform.eulerAngles;
                endRot = new Vector3(90, -90, 90);
            }
        }

        count += Time.deltaTime;

        if(openFlg && count >= 0.25f && time2 <= 1.0f)
        {
            rot.x = Mathf.LerpAngle(startRot.x, endRot.x, time2);
            rot.y = Mathf.LerpAngle(startRot.y, endRot.y, time2);
            rot.z = Mathf.LerpAngle(startRot.z, endRot.z, time2);
            this.transform.eulerAngles = rot;

            time2 += Time.deltaTime * 2;

            if(time2 >= 1.0f)
            {
                // ここでゴールのフラグ立てる
                clear.GetComponent<GoalHitOperation>().ClearFlg = true;
            }
        }
    }

    public void Init(GameObject clearObj)
    {
        clear = clearObj;
    }
}
