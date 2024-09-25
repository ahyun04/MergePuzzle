using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Inspector에서 할당할 수 있도록 공개된 변수
    public GameObject menuPanel;
    public Button openMenuButton;
    public Button closeMenuButton;
    //public Slider MusicSlider;
    //public Slider SoundSlider;

    private bool isMenuVisible = false;

    void Start()
    {
        // 버튼 클릭 이벤트에 함수 연결
        openMenuButton.onClick.AddListener(OpenMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);

        // 메뉴 패널을 숨김
        menuPanel.SetActive(false);

        // 이전에 저장된 음량을 불러오고, Slider에 반영
       /* if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            AudioListener.volume = savedVolume;
            MusicSlider.value = savedVolume;
        }
        else
        {
            // 기본 음량 설정
            AudioListener.volume = 1f;
            MusicSlider.value = 1f;
        }

        // Slider 값 변경 시 음량을 조절하는 함수 등록
        MusicSlider.onValueChanged.AddListener(SetVolume);

        // 이전에 저장된 음량을 불러오고, Slider에 반영
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            AudioListener.volume = savedVolume;
            SoundSlider.value = savedVolume;
        }
        else
        {
            // 기본 음량 설정
            AudioListener.volume = 1f;
            SoundSlider.value = 1f;
        }

        // Slider 값 변경 시 음량을 조절하는 함수 등록
        SoundSlider.onValueChanged.AddListener(SetVolume2);*/
    }

    void OpenMenu()
    {
        // 메뉴를 보이도록 설정
        isMenuVisible = true;
        menuPanel.SetActive(isMenuVisible);
    }

    void CloseMenu()
    {
        // 메뉴를 숨기도록 설정
        isMenuVisible = false;
        menuPanel.SetActive(isMenuVisible);
    }
    /*public void SetVolume(float volume)
    {
        // 음량 설정
        AudioListener.volume = volume;

        // 설정된 음량을 PlayerPrefs에 저장
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
    public void SetVolume2(float volume)
    {
        // 음량 설정
        AudioListener.volume = volume;

        // 설정된 음량을 PlayerPrefs에 저장
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }*/
}
