using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

public class AnimationComponent : MonoBehaviour
{
    public Animator[] ani = new Animator[3];
    public int[] trans=new int[3];
    public bool[] Ani;
    public bool[] Direction;
    public Button[] img = new Button[3];
    bool Uptrg;
    public Button BackButton;
    public Animation a;
    public int[] HashName=new int[3];
    public bool time = false;
    public bool AllAnimation = false;
    public bool change = false;
    public SceneComponent Scene;
    public bool trg = false;
    // Start is called before the first frame update
    void Start()
    {
        Scene = GameObject.Find("SceneManager").GetComponent<SceneComponent>();

        ani = new Animator[3];
        trans = new int[3];
        Ani = new bool[3];
        Direction = new bool[3];
        img = new Button[3];
        HashName = new int[3];

        ani[0] = GameObject.Find("1~10").GetComponent<Animator>();
        ani[1] = GameObject.Find("11~20").GetComponent<Animator>();
        ani[2] = GameObject.Find("21~30").GetComponent<Animator>();
        img[0] = GameObject.Find("1~10").GetComponent<Button>();
        img[1] = GameObject.Find("11~20").GetComponent<Button>();
        img[2] = GameObject.Find("21~30").GetComponent<Button>();

        for (int i = 0; i < 3; i++)
        {
            trans[i] = i + 1;
            Ani[i] = ani[i].GetBool("Ani");
            Direction[i] = ani[i].GetBool("Direction");
            ani[i].enabled = true;
            ani[i].SetInteger("trans", trans[i]);
        }

        //if (gameObject.name == "1~10")
        //{
        //    trans = 1;
        //    img = GameObject.Find("1~10").GetComponent<Image>();
        //}
        //if (gameObject.name == "11~20")
        //{
        //    trans = 2;
        //    img = GameObject.Find("11~20").GetComponent<Image>();
        //}
        //if (gameObject.name == "21~30")
        //{
        //    trans = 3;
        //    img = GameObject.Find("21~30").GetComponent<Image>();
        //}

        BackButton = GameObject.Find("戻る").GetComponent<Button>();
    }

    // Update is called once per frame
    void Update()
    {
        trg = false;
        for(int i = 0; i < 3; i++)
        {
            //アニメーション切り替え完了
            if (HashName[i] != ani[i].GetCurrentAnimatorStateInfo(0).nameHash && change == true) 
            {
                Ani[i] = false;
                ani[i].SetBool("Ani", Ani[i]);
            }

            //HashName[i] = ani[i].GetCurrentAnimatorStateInfo(0).nameHash;

            //if (ani[i].GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && change == true)
            //{
            //    change = false;
            //    break;
            //}

            ////再生
            //if (ani[i].GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f &&
            //    change == false)
            //{
            //    Ani[i] = false;
            //}

            //
            //Debug.Log(ani[i].GetCurrentAnimatorStateInfo(0).normalizedTime);

        }

        //全てのアニメーションの切り替え完了
        for (int i = 0; i < 3; i++)
        {
            if (Ani[i] == false)
            {
                AllAnimation = false;
            }
            else
            {
                AllAnimation = true;
                break;
            }
        }

        if (AllAnimation == false)
        {
            change = false;
        }

        //キー操作受付判定
        for (int i = 0; i < 3; i++)
        {
            if (img[i].interactable == false ||
                ani[i].GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f ||
                AllAnimation == true||
                !Scene.AllFade)
            {
                time = false;
                break;
            }
            else
            {
                time = true;
            }
        }

        //Debug.Log(ani.GetCurrentAnimatorStateInfo(0).normalizedTime);
        if (time == true ) 
        {

            if ((Input.GetAxisRaw(Controll.十字キー上下.ToString()) >= 1) && trg == false && !Uptrg)  
            {
                trg = true;
                Uptrg = true;
                SEManager.Instance.Play(Scene.EnterClip);
                Debug.Log("UP");
            }
            if ((Input.GetAxisRaw(Controll.十字キー上下.ToString()) <= -1) && trg == false && Uptrg) 
            {
                trg = true;
                Uptrg = false;
                SEManager.Instance.Play(Scene.EnterClip);
                Debug.Log("Down");
            }

            if (Uptrg == false)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (trans[i] == 1)
                    {
                        GameObject.Find(ani[i].name).GetComponent<Button>().Select();
                    }
                }

                if ((Input.GetAxis(Controll.十字キー左右.ToString()) >= 1)&& trg == false)
                {
                    trg = true;
                    LeftTurn();
                    change = true;
                    SEManager.Instance.Play(Scene.EnterClip);
                    //Ani = true;
                    //Direction = false;
                    //Debug.Log("Left");
                    //trans++;
                    //if (trans > 3)
                    //{
                    //    trans = 1;
                    //}
                    //HashName = ani.GetCurrentAnimatorStateInfo(0).nameHash;
                }
                if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= -1) && trg == false)
                {
                    trg = true;
                    RightTurn();
                    change = true;
                    SEManager.Instance.Play(Scene.EnterClip);
                }
            }
            else
            {
                BackButton.Select();
            }

            SetStatus();
        }
    }

    public void LeftTurn()
    {
        for (int i = 0; i < 3; i++)
        {
            HashName[i] = ani[i].GetCurrentAnimatorStateInfo(0).nameHash;
            Ani[i] = true;
            Direction[i] = true;
            //Debug.Log("Right");
            trans[i]--;
            if (trans[i] < 1)
            {
                trans[i] = 3;
            }

            if (trans[i] == 1)
            {
                GameObject.Find(ani[i].name).GetComponent<Button>().Select();
            }

        }
    }
    public void RightTurn()
    {
        for (int i = 0; i < 3; i++)
        {
            HashName[i] = ani[i].GetCurrentAnimatorStateInfo(0).nameHash;
            Ani[i] = true;
            Direction[i] = true;
            //Debug.Log("Right");
            trans[i]++;
            if (trans[i] > 3)
            {
                trans[i] = 1;
            }

            if (trans[i] == 1)
            {
                GameObject.Find(ani[i].name).GetComponent<Button>().Select();
            }

        }

    }
    public void SetStatus()
    {
        for(int i = 0; i < 3; i++)
        {
            ani[i].SetBool("Direction", Direction[i]);
            ani[i].SetBool("Ani", Ani[i]);
            ani[i].SetInteger("trans", trans[i]);
        }
    }
}
