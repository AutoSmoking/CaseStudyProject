using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyOperaion : MonoBehaviour
{
    enum KeyType
    {
        二分割,三分割
    }

    KeyType keyType = KeyType.二分割;

    // 今持っている鍵のモデル
    GameObject Key = null;

    [SerializeField,Header("鍵のモデルを格納")]
    List<GameObject> KeyModel = new List<GameObject>() { };

    [SerializeField, Header("鍵のアニメーション")]
    RuntimeAnimatorController aniCon;

    Animator anim;

    // Start is called before the first frame update
    void Awake()
    {
        // ここに鍵のモデルを子オブジェクトにするというしょりをいれる
        switch(this.gameObject.tag)
        {
            case "1":
                {
                    if (keyType == KeyType.二分割)
                    {
                        Key = Instantiate(KeyModel[0], this.transform);
                    }
                    else
                    {
                        Key = Instantiate(KeyModel[2], this.transform);
                    }
                }
                break;

            case "2":
                {
                    if (keyType == KeyType.二分割)
                    {
                        Key = Instantiate(KeyModel[1], this.transform);
                    }
                    else
                    {
                        Key = Instantiate(KeyModel[3], this.transform);
                    }
                }
                break;

            case "3":
                {
                    Key = Instantiate(KeyModel[4], this.transform);
                }
                break;
        }

        anim = Key.transform.GetChild(0).gameObject.AddComponent<Animator>();
        anim.runtimeAnimatorController = aniCon;
        anim.Play(0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision collision)
    {

        /* 泡は一番に統合されるので無し */
        //if(collision.gameObject.tag == "1")
        //{
        //}

        if (collision.gameObject.tag == "2")
        {
            switch (this.gameObject.tag)
            {
                case "1":
                    {
                        // ここで鍵の子オブジェクトを変える
                        Destroy(Key);

                        if(keyType == KeyType.二分割)
                        {
                            Key = Instantiate(KeyModel[8], this.transform);
                        }
                        else
                        {
                            Key = Instantiate(KeyModel[5], this.transform);
                        }

                        anim = Key.transform.GetChild(0).gameObject.AddComponent<Animator>();
                        anim.runtimeAnimatorController = aniCon;
                        anim.Play(0);
                    }
                    break;

                //case "2":
                //    {
                //        // 同じタグなので無し
                //    }
                //    break;

                //case "3":
                //    {
                //        // 統合されるので無し
                //    }
                //    break;
            }
        }
        else if (collision.gameObject.tag == "3")
        {
            switch (this.gameObject.tag)
            {
                case "1":
                    {
                        // ここで鍵の子オブジェクトを変える
                        Destroy(Key);

                        Key = Instantiate(KeyModel[6], this.transform);

                        anim = Key.transform.GetChild(0).gameObject.AddComponent<Animator>();
                        anim.runtimeAnimatorController = aniCon;
                        anim.Play(0);
                    }
                    break;

                case "2":
                    {
                        // ここで鍵の子オブジェクトを変える
                        Destroy(Key);

                        Key = Instantiate(KeyModel[7], this.transform);

                        anim = Key.transform.GetChild(0).gameObject.AddComponent<Animator>();
                        anim.runtimeAnimatorController = aniCon;
                        anim.Play(0);
                    }
                    break;

                //case "3":
                //    {
                //        // 同じタグなので無し
                //    }
                //    break;
            }
        }
    }
}
