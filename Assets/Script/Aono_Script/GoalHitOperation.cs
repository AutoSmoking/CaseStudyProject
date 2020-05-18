using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalHitOperation : MonoBehaviour
{
    Animator ani;

    GameObject bubble1 = null;
    GameObject bubble2 = null;
    GameObject bubble3 = null;

    GameObject SceneManager = null;

    bool OpenFlg = false;

    bool GameFlg = false;

    // ゴールできるようになったらtrue
    [System.NonSerialized]
    public bool GoalFlg = false;

    // Start is called before the first frame update

    void Start()
    {
        ani = this.GetComponentInChildren<Animator>();

        ani.enabled = false;

        bubble1 = GameObject.FindGameObjectWithTag("1");
        bubble2 = GameObject.FindGameObjectWithTag("2");
        bubble3 = GameObject.FindGameObjectWithTag("3");

        SceneManager = GameObject.Find("SceneManager");
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

        if(!GoalFlg && ani.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            GoalFlg = true;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(GoalFlg && other.gameObject.layer == LayerMask.NameToLayer("Bubble"))
        {
            GameFlg = SceneManager.GetComponent<SceneComponent>().GameFrag;

            if (!GameFlg)
            {
                Debug.Log("GoalHit");
                SceneManager.GetComponent<SceneComponent>().GameFrag = true;
            }
        }
    }
}
