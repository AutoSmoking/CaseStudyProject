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

    // Start is called before the first frame update
    void Start()
    {
        firstScale = this.transform.localScale;

        endScale = firstScale * 1.5f;

        prefab = (GameObject)Resources.Load("BubblesBurst_Particle");
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
            particle = Instantiate(prefab, this.transform);
            particle.GetComponent<ParticleSystem>().Play();
        }

        if (isParticle && particle.GetComponent<ParticleSystem>().isStopped)
        {
            Destroy(this.transform.parent.gameObject);
        }
    }
}
