using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using KanKikuchi.AudioManager;
public class DontDestroy : MonoBehaviour
{
    static public EventSystem instance;
    public GameObject NowSelectObj;
    public SceneComponent SceneCom;

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
        SceneCom = GameObject.Find("SceneManager").GetComponent<SceneComponent>();
        NowSelectObj = instance.currentSelectedGameObject;

    }

    // Update is called once per frame
    void Update()
    {
        if (instance.currentSelectedGameObject != NowSelectObj)
        {
            NowSelectObj = instance.currentSelectedGameObject;
            SEManager.Instance.Play(SceneCom.EnterClip);
        }
        else 
        {
            instance.firstSelectedGameObject = NowSelectObj;
        }
    }
}
