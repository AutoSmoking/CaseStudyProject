using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class NeedleOperation : MonoBehaviour
{
    GameObject Bubble;
    GameObject Bubble2;
    GameObject Bubble3;

    // Start is called before the first frame update
    void Start()
    {
        Bubble = GameObject.FindGameObjectWithTag("1");
        Bubble2 = GameObject.FindGameObjectWithTag("2");
        Bubble3 = GameObject.FindGameObjectWithTag("3");
    }

    void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.tag == "1")
        {
            SEManager.Instance.Play("SE/Bubble_Death");
            Destroy(Bubble);

            // 勝手に追加
            if (Bubble2 != null) 
            {
                Bubble2.GetComponent<BubbleOperation>().DeathFlg = true;
            }
            if (Bubble3 != null)
            {
                Bubble3.GetComponent<BubbleOperation>().DeathFlg = true;
            }
        }

        if (other.gameObject.tag == "2")
        {
            SEManager.Instance.Play("SE/Bubble_Death");
            Destroy(Bubble2);

            // 勝手に追加
            if (Bubble != null)
            {
                Bubble.GetComponent<BubbleOperation>().DeathFlg = true;
            }
            if (Bubble3 != null)
            {
                Bubble3.GetComponent<BubbleOperation>().DeathFlg = true;
            }
        }

        if (other.gameObject.tag == "3")
        {
            SEManager.Instance.Play("SE/Bubble_Death");
            Destroy(Bubble3);

            // 勝手に追加
            if (Bubble2 != null)
            {
                Bubble2.GetComponent<BubbleOperation>().DeathFlg = true;
            }
            if (Bubble != null)
            {
                Bubble.GetComponent<BubbleOperation>().DeathFlg = true;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
