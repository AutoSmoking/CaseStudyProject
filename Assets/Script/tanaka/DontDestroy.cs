using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DontDestroy : MonoBehaviour
{
    static public EventSystem instance;
    public GameObject NowSelectObj;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {

            instance = GetComponent<EventSystem>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {

            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (instance.currentSelectedGameObject != null)
        {
            NowSelectObj = instance.currentSelectedGameObject;
        }
        else 
        {
            instance.firstSelectedGameObject = NowSelectObj;
        }
    }
}
