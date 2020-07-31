using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanKikuchi.AudioManager;

public class NeedleOperation : MonoBehaviour
{
    List<GameObject> bubbleList = new List<GameObject>() { };
    List<GameObject> stageList = new List<GameObject>() { };

    // Start is called before the first frame update
    void Start()
    {
        bubbleList.Add(GameObject.FindGameObjectWithTag("1"));
        bubbleList.Add(GameObject.FindGameObjectWithTag("2"));
        bubbleList.Add(GameObject.FindGameObjectWithTag("3"));

        stageList.AddRange(GameObject.FindGameObjectsWithTag("stage"));
    }

    void OnCollisionEnter(Collision other)
    {
        //if (other.gameObject.tag == "1")
        //{
        //    SEManager.Instance.Play("SE/Bubble_Death");
        //    Destroy(Bubble);

        //    // 勝手に追加
        //    if (Bubble2 != null) 
        //    {
        //        Bubble2.GetComponent<BubbleOperation>().DeathFlg = true;
        //    }
        //    if (Bubble3 != null)
        //    {
        //        Bubble3.GetComponent<BubbleOperation>().DeathFlg = true;
        //    }
        //}

        //if (other.gameObject.tag == "2")
        //{
        //    SEManager.Instance.Play("SE/Bubble_Death");
        //    Destroy(Bubble2);

        //    // 勝手に追加
        //    if (Bubble != null)
        //    {
        //        Bubble.GetComponent<BubbleOperation>().DeathFlg = true;
        //    }
        //    if (Bubble3 != null)
        //    {
        //        Bubble3.GetComponent<BubbleOperation>().DeathFlg = true;
        //    }
        //}

        //if (other.gameObject.tag == "3")
        //{
        //    SEManager.Instance.Play("SE/Bubble_Death");
        //    Destroy(Bubble3);

        //    // 勝手に追加
        //    if (Bubble2 != null)
        //    {
        //        Bubble2.GetComponent<BubbleOperation>().DeathFlg = true;
        //    }
        //    if (Bubble != null)
        //    {
        //        Bubble.GetComponent<BubbleOperation>().DeathFlg = true;
        //    }
        //}

        if (other.gameObject.layer == LayerMask.NameToLayer("Bubble") &&
            other.gameObject.GetComponent<Rigidbody>().isKinematic == false) 
        {
            SEManager.Instance.Play("SE_Needle", 2);

            foreach(var obj in bubbleList)
            {
                if(obj == null)
                {
                    continue;
                }

                foreach(var com in obj.GetComponents<Behaviour>())
                {
                    com.enabled = false;
                }

                if(obj.GetComponent<Collider>())
                {
                    obj.GetComponent<Collider>().isTrigger = true;
                }

                if(obj.GetComponent<Rigidbody>())
                {
                    obj.GetComponent<Rigidbody>().isKinematic = true;
                }
            }

            foreach(var obj in stageList)
            {
                foreach (var com in obj.GetComponents<Behaviour>())
                {
                    com.enabled = false;
                }
            }

            StartCoroutine(Blast(other.gameObject));
        }
    }

    IEnumerator Blast(GameObject other)
    {
        yield return new WaitForSeconds(0.1f);

        other.AddComponent<BubbleBlast>();
        
        //yield return new WaitForSeconds(1.0f);

        //foreach (var obj in bubbleList)
        //{
        //    if(obj == other)
        //    {
        //        continue;
        //    }

        //    if(obj == null)
        //    {
        //        continue;
        //    }

        //    obj.AddComponent<BubbleBlast>();
        //}
    }

    // Update is called once per frame
    //    void Update()
    //    {
    //    }
}
