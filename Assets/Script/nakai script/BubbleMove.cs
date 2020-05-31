//===============================================
//Bubble Move.cs    作成者:中井
//内容:タイトルムービーだけの処理です
//詳細:泡の座標等の情報を取得して、動作をさせています。
//===============================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;
using UnityEngine.SceneManagement;

public class BubbleMove : MonoBehaviour
{

    //メソッド変数(インスペクター上でいじってください)
    [SerializeField, Header("左から1つ目の泡オブジェクト")]
    public GameObject Bubble1;     //泡1

    [SerializeField, Header("左から2つ目の泡オブジェクト")]
    public GameObject Bubble2;     //泡2

    [SerializeField, Header("左から3つ目の泡オブジェクト")]
    public GameObject Bubble3;     //泡3

    [SerializeField, Header("左から4つ目の泡オブジェクト")]
    public GameObject Bubble4;     //泡4

    [SerializeField, Header("左から5つ目の泡オブジェクト")]
    public GameObject Bubble5;     //泡5

    [SerializeField, Header("フェードイン処理したいので追加")]
    public Image Panel;
    public float a;
    new float[] move_x;
    new float[] move_y;
    float max_x = 3.0f;
    float max_y = 5.0f;
    new float[] x;

    public bool flg = false;     //座標処理が終わればtrue→フェードイン処理へ
    int i = 0;            //for制御用

    void Start()
    {

        //元のxの値を保持
        x = new float[] { Bubble1.transform.position.x, Bubble2.transform.position.x, Bubble3.transform.position.x, Bubble4.transform.position.x, Bubble5.transform.position.x };

        move_x = new float[] { 0.15f, 0.15f, 0.15f, 0.15f, 0.15f };
        move_y = new float[] { 0.1f, 0.1f, 0.1f, 0.1f, 0.1f };

        SEManager.Instance.Play("SE/SE_BubbleMove",1,0,1,true);
        //SEManager.Instance.Play("SE/SE_BubbleMove",1,0,1,true);
        SEManager.Instance.Play("SE/タイトル/泡の浮上音/音人/SE_BubbleRise", 1, 0, 1, true);

    }


    void Update()    //泡の座標更新とフェードイン処理
    {
        //旧バージョン
        /*座標の更新処理*/
        /*
        if (Bubble1.transform.position.y <= 1)
        {
            if (Bubble1.transform.position.x >= 0.15)
            {
                move_x *= -1;
            }
            else if (Bubble1.transform.position.x <= -0.15)
            {
                move_x *= -1;
            }

            Bubble1.transform.position += new Vector3(move_x, move_y, 0);  //泡x,y座標に加算
        }
        else
        {
            flg = true;
        }
        */

        if (Bubble1.transform.position.y <= max_y)
        {
            if (Bubble1.transform.position.x >= x[0] + max_x)
            {
                move_x[0] *= -1;
            }
            else if (Bubble1.transform.position.x <= x[0] - max_x)
            {
                move_x[0] *= -1;
            }

            Bubble1.transform.position += new Vector3(move_x[0], move_y[0], 0);  //泡x,y座標に加算
        }

        if (Bubble2.transform.position.y <= max_y)
        {
            if (Bubble2.transform.position.x >= x[1] + max_x)
            {
                move_x[1] *= -1;
            }
            else if (Bubble2.transform.position.x <= x[1] - max_x)
            {
                move_x[1] *= -1;
            }

            Bubble2.transform.position += new Vector3(move_x[1], move_y[1], 0);  //泡x,y座標に加算
        }

        if (Bubble3.transform.position.y <= max_y)
        {
            if (Bubble3.transform.position.x >= x[2] + max_x)
            {
                move_x[2] *= -1;
            }
            else if (Bubble3.transform.position.x <= x[2] - max_x)
            {
                move_x[2] *= -1;
            }

            Bubble3.transform.position += new Vector3(move_x[2], move_y[2], 0);  //泡x,y座標に加算
        }

        if (Bubble4.transform.position.y <= max_y)
        {
            if (Bubble4.transform.position.x >= x[3] + max_x)
            {
                move_x[3] *= -1;
            }
            else if (Bubble4.transform.position.x <= x[3] - max_x)
            {
                move_x[3] *= -1;
            }

            Bubble4.transform.position += new Vector3(move_x[3], move_y[3], 0);  //泡x,y座標に加算
        }

        if (Bubble5.transform.position.y <= max_y)
        {
            if (Bubble5.transform.position.x >= x[4] + max_x)
            {
                move_x[4] *= -1;
            }
            else if (Bubble5.transform.position.x <= x[4] - max_x)
            {
                move_x[4] *= -1;
            }

            Bubble5.transform.position += new Vector3(move_x[4], move_y[4], 0);  //泡x,y座標に加算
        }

        if (Bubble5.transform.position.y >= max_y && flg == false)
        {
            flg = true;
            SEManager.Instance.Stop();
        }

        if (flg == true)
        {
            Panel.color += new Color(0.0f, 0.0f, 0.0f, 0.005f);
        }
        a = Panel.color.a;
        if (Panel.color.a >= 1)
        {
            Debug.Log("change");
            SceneManager.LoadScene("Title Scene");
        }
    }

}
