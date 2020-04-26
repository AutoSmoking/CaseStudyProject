using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrelOperation : MonoBehaviour
{
    bool Flag = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Flag = true;
        }

        if (Flag != false)
        {
            //Transform myTransform = this.transform;
            //Vector3 pos = myTransform.position;
            //GameObject obj = (GameObject)Resources.Load("Bubble");
            //Instantiate(obj, new Vector3(pos.x, pos.y, pos.z), Quaternion.identity);

            Destroy(this.gameObject);
        }
    }
}
