using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class takaraOpenTexture : MonoBehaviour
{
    bool GoalFlg = false;

    GameObject Goal = null;

    [SerializeField]
    Sprite Open = null;

    // Start is called before the first frame update
    void Start()
    {
        Goal = GameObject.FindGameObjectWithTag("Finish");
    }

    // Update is called once per frame
    void Update()
    {
        if(!GoalFlg && Goal.GetComponent<GoalHitOperation>().GoalFlg)
        {
            GoalFlg = true;

            this.GetComponent<Image>().sprite = Open;
        }
    }
}
