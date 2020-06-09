using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class GoalHitOperation : MonoBehaviour
{
    Animation ani;

    GameObject bubble1 = null;
    GameObject bubble2 = null;
    GameObject bubble3 = null;

    GameObject SceneManager = null;

    bool OpenFlg = false;

    bool GameFlg = false;

    // ゴールできるようになったらtrue
    [System.NonSerialized]
    public bool GoalFlg = false;

    // エフェクト
    GameObject prefab = null;
    GameObject particle = null;

    //以下ゴールクリア演出用
    //by Kaito
    bool CreateFlag = false;
    [SerializeField,Header("ゴールクリア用オブジェクト")]
    public GameObject GoalClear = null;

    // Start is called before the first frame update

    void Start()
    {
        ani = this.GetComponentInChildren<Animation>();

        bubble1 = GameObject.FindGameObjectWithTag("1");
        bubble2 = GameObject.FindGameObjectWithTag("2");
        bubble3 = GameObject.FindGameObjectWithTag("3");

        SceneManager = GameObject.Find("SceneManager");

        prefab = (GameObject)Resources.Load("BoxOpen_Effect");
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

            //ani.Play("takarabako_open");
            //SEManager.Instance.Play("宝箱/SE_GoalOpen");

            particle = Instantiate(prefab, this.transform);
            particle.GetComponentInChildren<ParticleSystem>().Play();
            SEManager.Instance.Play("宝箱/SE_GoalKirakira", 2, 0, 1, true);
        }

        if(!GoalFlg && OpenFlg && !ani.IsPlaying("takarabako_open"))
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
                CreateObjects();
            }
        }
    }

    void CreateObjects()
    {
        if (CreateFlag)
        {
        }
        else
        {
            Debug.Log("CreateObjects On");
            GoalClear = Instantiate(GoalClear, new Vector3(0, 0, 0), Quaternion.identity);
            GoalClearProductionOperater gcp = GoalClear.GetComponent<GoalClearProductionOperater>();
            gcp.Init(ani, bubble1, this.gameObject, SceneManager);
            CreateFlag = true;
        }
    }
}
