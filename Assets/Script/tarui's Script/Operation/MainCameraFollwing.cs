using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCameraFollwing : MonoBehaviour
{
    [SerializeField]
    Camera main = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        this.transform.position = main.transform.position;

        this.GetComponent<Camera>().orthographicSize =
            main.orthographicSize;
    }
}
