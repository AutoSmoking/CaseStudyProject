using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBlast : MonoBehaviour
{
    Vector3 firstScale = Vector3.zero;
    Vector3 endScale = Vector3.zero;

    float time = 0.75f;
    
    float percent = 0;

    GameObject prefab = null;
    GameObject particle = null;

    bool isParticle = false;
    bool isend = false;

    List<GameObject> bubble = new List<GameObject>() { };

    // Start is called before the first frame update
    void Start()
    {
        firstScale = this.transform.localScale;

        endScale = firstScale * 1.5f;

        prefab = (GameObject)Resources.Load("BubblesBurst_Particle");

        bubble.Add(GameObject.FindGameObjectWithTag("1"));
        bubble.Add(GameObject.FindGameObjectWithTag("2"));
        bubble.Add(GameObject.FindGameObjectWithTag("3"));
    }

    // Update is called once per frame
    void Update()
    {
        float Wait = 0;

        percent += Time.deltaTime;

        Wait = percent / time;

        this.transform.localScale = Vector3.Lerp(firstScale, endScale, Wait);

        if(Wait >= 1 && !isParticle)
        {
            this.transform.GetComponent<MeshRenderer>().enabled = false;

            isParticle = true;
            isend = true;
            particle = Instantiate(prefab, this.transform);
            particle.GetComponent<ParticleSystem>().Play();

            this.transform.GetChild(0).GetComponent<ParticleSystem>().Stop();
        }

        // 泡が割れた演出(パーティクル)が終わった時
        if (isParticle && isend)
        {
            isend = false;

            //Destroy(this.transform.parent.gameObject);

            for(int i=0;i<bubble.Count;i++)
            {
                if(bubble[i] == null)
                {
                    continue;
                }

                bubble[i].GetComponent<BubbleOperation>().DeathFlg = true;
            }

            // 追加20200725

            Rigidbody childRig = null;

            for (int i = 0; i < this.transform.childCount; i++)
            {
                if(this.transform.GetChild(i).tag == "Key")
                {
                    childRig = this.transform.GetChild(i).GetComponent<Rigidbody>();

                    // 鍵を落とす
                    childRig.isKinematic = false;
                    childRig.AddForce(new Vector3(0, -50.0f, 0.0f));

                    StartCoroutine(endGame());
                }
            }
        }
    }


    IEnumerator endGame()
    {
        yield return new WaitForSeconds(1.0f);

        // ここでフェードのフラグを立てる
        GameObject.Find("SceneManager").GetComponent<SceneComponent>().GameOverFlag = true;
    }
}
