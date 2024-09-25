using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestMain : MonoBehaviour
{
    public Slider musicSlider;
    public Slider soundSlider;
    //public Button btnSave;

    void Start()
    {

        bool IsNewbie = PlayerPrefs.GetInt("newbie", 1) == 1 ? true : false;
        //Debug.Log($"newbie: {IsNewbie}");
        float musicVolume = 0f;
        if (IsNewbie)
        {
            //���带 ���� ũ�� ��ڴ� 
            musicVolume = 1f;

            //���� �ƴϾ�� �Ѵ� 
            PlayerPrefs.SetInt("newbie", 0);
            //���庼���� �������� 
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
        }
        else
        {
            //������ ������ ���� ������ ��ڴ� 
            musicVolume = PlayerPrefs.GetFloat("musicVolume");
        }
        //Debug.Log($"musicVolume: {musicVolume}");
        musicSlider.value = musicVolume;
        this.musicSlider.onValueChanged.AddListener((val) => {
            //Debug.Log(val);
            PlayerPrefs.SetFloat("musicVolume", val);
        });





        bool IsNew = PlayerPrefs.GetInt("new", 1) == 1 ? true : false;
        float soundVolume = 0f;
        if (IsNew)
        {
            soundVolume = 1f;
            PlayerPrefs.SetInt("new", 0);
            PlayerPrefs.SetFloat("soundVolume", soundVolume);
        }
        else
        {
            soundVolume = PlayerPrefs.GetFloat("soundVolume");
        }
        soundSlider.value = soundVolume;
        this.soundSlider.onValueChanged.AddListener((val) => {
            //Debug.Log(val);
            PlayerPrefs.SetFloat("soundVolume", val);
        });

        /*this.btnSave.onClick.AddListener(() => {
            PlayerPrefs.Save();
        });*/
    }

}