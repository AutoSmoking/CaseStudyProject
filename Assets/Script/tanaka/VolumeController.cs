using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeController: MonoBehaviour
{
    public enum VolumeType { MASTER, BGM, SE }

    [SerializeField]
    VolumeType volumeType = 0;

    Slider slider;
    public GameObject BGMObject;
    public GameObject SEObject;

    void Start()
    {
        slider = GetComponent<Slider>();
        BGMObject = GameObject.Find("BGMManager");
        SEObject = GameObject.Find("SEManager");
        //soundManager = FindObjectOfType<SoundManager>();
    }

    public void OnValueChanged()
    {
        //switch (volumeType)
        //{
        //    case VolumeType.MASTER:
        //        soundManager.Volume = slider.value;
        //        break;
        //    case VolumeType.BGM:
        //        soundManager.BgmVolume = slider.value;
        //        break;
        //    case VolumeType.SE:
        //        soundManager.SeVolume = slider.value;
        //        break;
        //}
    }
}