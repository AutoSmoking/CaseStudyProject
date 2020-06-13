using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using KanKikuchi.AudioManager;

public class TitleButton : MonoBehaviour
{
    public GameObject SceneManager;
    SceneComponent scene;
    bool AnyFlag = false;
    Button[] Titlebutton;
    AlphaChange[] Alphabutton;
    bool starttrg = false;

    bool AxisTrg = false;
    int NowButton = 0;
    // Start is called before the first frame update
    void Start()
    {
        SceneManager = GameObject.Find("SceneManager");
        scene = SceneManager.GetComponent<SceneComponent>();
        scene.TitleFlag = false;
        AnyFlag = false;
        AxisTrg = false;
        starttrg = false;
        NowButton = 1;

        Titlebutton = new Button[3];
        Titlebutton[0] = GameObject.Find("Canvas/Any").GetComponent<Button>();
        Titlebutton[1] = GameObject.Find("Canvas/Start").GetComponent<Button>();
        Titlebutton[2] = GameObject.Find("Canvas/GameEnd").GetComponent<Button>();

        Titlebutton[0].interactable = true;
        Titlebutton[1].interactable = false;
        Titlebutton[2].interactable = false;

        Alphabutton = new AlphaChange[3];
        Alphabutton[0] = Titlebutton[0].gameObject.GetComponent<AlphaChange>();
        Alphabutton[1] = Titlebutton[1].gameObject.GetComponent<AlphaChange>();
        Alphabutton[2] = Titlebutton[2].gameObject.GetComponent<AlphaChange>();

        for(int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                Titlebutton[i].interactable = true;
                Alphabutton[i].AlphaStart = true;
            }
            else
            {
                Titlebutton[i].interactable = false;
                Alphabutton[i].AlphaStart = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!AnyFlag)
        {
            Titlebutton[0].Select();
        }
        else if(!starttrg)
        {
            if ((Input.GetAxis(Controll.十字キー上下.ToString()) >= 1) && AxisTrg == false)
            {
                AxisTrg = true;
                NowButton--;

                if (NowButton < 1)
                {
                    NowButton = 1;
                }
                else
                {
                    SEManager.Instance.Play(scene.EnterClip);
                    Alphabutton[1].AlphaStart = true;
                    Alphabutton[2].AlphaStart = false;
                    Alphabutton[2].AlphaReset();
                }

            }
            if ((Input.GetAxis(Controll.十字キー上下.ToString()) <= -1) && AxisTrg == false)
            {
                AxisTrg = true;
                NowButton++;

                if (NowButton > 2) 
                {
                    NowButton = 2;
                }
                else
                {
                    SEManager.Instance.Play(scene.EnterClip);
                    Alphabutton[2].AlphaStart = true;
                    Alphabutton[1].AlphaStart = false;
                    Alphabutton[1].AlphaReset();
                }

            }

            if ((Input.GetAxis(Controll.十字キー左右.ToString()) <= 0.5 &&
                Input.GetAxis(Controll.十字キー左右.ToString()) >= -0.5 &&
                Input.GetAxis(Controll.十字キー上下.ToString()) <= 0.5 &&
                Input.GetAxis(Controll.十字キー上下.ToString()) >= -0.5) &&
                AxisTrg)
            {
                AxisTrg = false;
            }

            Titlebutton[NowButton].Select();

        }

    }
    public void AnyButton()
    {
        AnyFlag = true;

        SEManager.Instance.Play(scene.EnterClip);

        for (int i = 0; i < 3; i++)
        {
            if (i == 0)
            {
                Titlebutton[i].interactable = false;
                Alphabutton[i].AlphaStart = false;
            }
            else
            {
                Titlebutton[i].interactable = true;
            }

            Alphabutton[i].AlphaReset();
        }

        Alphabutton[NowButton].AlphaStart = true;
        Titlebutton[NowButton].Select();

    }
    public void StartGame()
    {
        if (!starttrg)
        {
            scene.TitleFlag = true;
            starttrg = true;
        }
    }

    public void EndGame()
    {
        Quit();
    }

    void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#elif UNITY_STANDALONE
      UnityEngine.Application.Quit();
#endif
    }
}
