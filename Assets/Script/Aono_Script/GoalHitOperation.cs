using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHitOperation : MonoBehaviour
{
    Animator ani;

    GameObject bubble1 = null;
    GameObject bubble2 = null;
    GameObject bubble3 = null;

    bool OpenFlg = false;

    // Start is called before the first frame update

    void Start()
    {
        ani = this.GetComponentInChildren<Animator>();

        ani.enabled = false;

        bubble1 = GameObject.FindGameObjectWithTag("1");
        bubble2 = GameObject.FindGameObjectWithTag("2");
        bubble3 = GameObject.FindGameObjectWithTag("3");
    }

    //void OnTriggerEnter(Collider other)
    //{   //ゴールに接触した時にログを出す
    //    if (other.gameObject.tag == "Player")
    //    {
    //        Debug.Log("GoalHit");
    //    }
    //}

    // Update is called once per frame
    void Update()
    {
        if (!OpenFlg && bubble1 != null && !bubble1.GetComponent<BubbleOperation>().DeathFlg
            && bubble2 == null && bubble3 == null)
        {
            OpenFlg = true;

            ani.enabled = true;
        }
    }
}
