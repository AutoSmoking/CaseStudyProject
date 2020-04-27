using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class warpOperation : MonoBehaviour
{
    [SerializeField, Header("ワープ先")]
    GameObject next;

    [SerializeField, Header("ワープまでの待ち時間"),Range(0.0f,3.0f)]
    float WaitTime;

    [SerializeField, Header("")]
    List<GameObject> objList = new List<GameObject>() { };

    bool flg = false;

    GameObject Object;

    Vector3 firstPos;

    float percent;

    // Start is called before the first frame update
    void Start()
    {
        if(objList.Count == 0)
        {
            Debug.LogError("ワープする対象がないです。");
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(flg)
        {
            if(percent >= WaitTime)
            {
                Object.transform.position = next.transform.position;
                percent = 0;
                flg = false;

                foreach (var com in Object.GetComponent<GetComOperation>().com)
                {
                    com.enabled = true;
                }
            }
            else
            {
                percent += Time.deltaTime;

                float Wait = percent / WaitTime;

                Object.transform.position = Vector3.Lerp(firstPos, this.transform.position, Wait);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        foreach(var obj in objList)
        {
            if(obj == other.gameObject && flg == false)
            {
                //obj.transform.position = next.transform.position;
                flg = true;

                Object = obj;

                firstPos = obj.transform.position;

                foreach(var com in obj.GetComponent<GetComOperation>().com)
                {
                    com.enabled = false;
                }
            }
        }
    }
}
