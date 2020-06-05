using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOpe : MonoBehaviour
{
    //[SerializeField, Header("中心位置")]
    //Transform center = null;

    //[SerializeField, Header("泡を格納")]
    List<GameObject> Bubble = new List<GameObject>() { };

    [SerializeField, Header("奥行の最低値"), Range(0.1f, 10.0f)]
    float Zsize = 0.1f;

    [SerializeField, Header("カメラの幅が小さくなる速さ"), Range(0.1f, 10.0f)]
    float spd = 1f;

    [SerializeField, Header("見える領域の＋α値"), Range(0.1f, 2.0f)]
    float alpha = 0.5f;

    [SerializeField, Header("カメラの中心位置")]
    Vector2 centerPos = Vector2.zero;

    Camera cam = null;

    // メイン以外のカメラ
    List<GameObject> cameras = new List<GameObject>() { };

    // Start is called before the first frame update
    private void Start()
    {
        // 泡を格納
        Bubble.Add(GameObject.FindGameObjectWithTag("1"));
        Bubble.Add(GameObject.FindGameObjectWithTag("2"));
        Bubble.Add(GameObject.FindGameObjectWithTag("3"));

        cam = this.GetComponent<Camera>();

        cameras.AddRange(GameObject.FindGameObjectsWithTag("camera"));

        CameraMove();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CameraMove();

        foreach(var cams in cameras)
        {
            Camera camera = cams.GetComponent<Camera>();

            cams.transform.position = this.transform.position;

            camera.orthographicSize = cam.orthographicSize;
        }
    }

    void CameraMove()
    {
        Vector2 max = Vector2.zero, min = Vector2.zero;
        Vector3 center = Vector3.zero;

        // 初期化
        for (int i = 0; i < Bubble.Count; i++)
        {
            // エラー防止
            if (Bubble[i] == null)
            {
                continue;
            }

            max = Bubble[i].transform.position;
            min = Bubble[i].transform.position;
            center = Bubble[i].transform.position;

            break;
        }

        // 泡が消えきったらもう処理しない
        if(max == Vector2.zero)
        {
            return;
        }

        // カメラの位置を決める計算
        for (int i = 1; i < Bubble.Count; i++)
        {
            // エラー防止
            if(Bubble[i] == null)
            {
                continue;
            }

            if (max.x < Bubble[i].transform.position.x)
            {
                max.x = Bubble[i].transform.position.x;
            }
            if (max.y < Bubble[i].transform.position.y)
            {
                max.y = Bubble[i].transform.position.y;
            }

            if (min.x > Bubble[i].transform.position.x)
            {
                min.x = Bubble[i].transform.position.x;
            }
            if (min.y > Bubble[i].transform.position.y)
            {
                min.y = Bubble[i].transform.position.y;
            }
        }

        // カメラの位置を調整
        center.x = Mathf.Lerp(max.x, min.x, 0.5f) + centerPos.x;
        center.y = Mathf.Lerp(max.y, min.y, 0.5f) + centerPos.y;
        center.z = this.transform.position.z;
        this.transform.position = center;

        // カメラの奥行の計算
        float px, py;
        float cx, cy;

        px = Mathf.Abs(max.x - min.x) / 16 * 9;
        py = Mathf.Abs(max.y - min.y);

        cx = px / 2 + alpha;
        cy = py / 2 + alpha;

        if (cx >= cy)
        {
            if (Zsize >= cx)
            {
                if (cam.orthographicSize > Zsize)
                {
                    cam.orthographicSize -= spd * Time.deltaTime;
                }
                else
                {
                    cam.orthographicSize = Zsize;
                }
            }
            else
            {
                if (cam.orthographicSize > cx)
                {
                    cam.orthographicSize -= spd * Time.deltaTime;
                }
                else
                {
                    cam.orthographicSize = cx;
                }
            }
        }
        else
        {
            if (Zsize >= cy)
            {
                if (cam.orthographicSize > Zsize)
                {
                    cam.orthographicSize -= spd * Time.deltaTime;
                }
                else
                {
                    cam.orthographicSize = Zsize;
                }
            }
            else
            {
                if (cam.orthographicSize > cy)
                {
                    cam.orthographicSize -= spd * Time.deltaTime;
                }
                else
                {
                    cam.orthographicSize = cy;
                }
            }
        }
    }
}
