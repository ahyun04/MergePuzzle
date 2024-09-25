using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    // Inspector���� �Ҵ��� �� �ֵ��� ������ ����
    public GameObject menuPanel;
    public Button openMenuButton;
    public Button closeMenuButton;
    //public Slider MusicSlider;
    //public Slider SoundSlider;

    private bool isMenuVisible = false;

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ����
        openMenuButton.onClick.AddListener(OpenMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);

        // �޴� �г��� ����
        menuPanel.SetActive(false);

        // ������ ����� ������ �ҷ�����, Slider�� �ݿ�
       /* if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            AudioListener.volume = savedVolume;
            MusicSlider.value = savedVolume;
        }
        else
        {
            // �⺻ ���� ����
            AudioListener.volume = 1f;
            MusicSlider.value = 1f;
        }

        // Slider �� ���� �� ������ �����ϴ� �Լ� ���
        MusicSlider.onValueChanged.AddListener(SetVolume);

        // ������ ����� ������ �ҷ�����, Slider�� �ݿ�
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            AudioListener.volume = savedVolume;
            SoundSlider.value = savedVolume;
        }
        else
        {
            // �⺻ ���� ����
            AudioListener.volume = 1f;
            SoundSlider.value = 1f;
        }

        // Slider �� ���� �� ������ �����ϴ� �Լ� ���
        SoundSlider.onValueChanged.AddListener(SetVolume2);*/
    }

    void OpenMenu()
    {
        // �޴��� ���̵��� ����
        isMenuVisible = true;
        menuPanel.SetActive(isMenuVisible);
    }

    void CloseMenu()
    {
        // �޴��� ���⵵�� ����
        isMenuVisible = false;
        menuPanel.SetActive(isMenuVisible);
    }
    /*public void SetVolume(float volume)
    {
        // ���� ����
        AudioListener.volume = volume;

        // ������ ������ PlayerPrefs�� ����
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
    public void SetVolume2(float volume)
    {
        // ���� ����
        AudioListener.volume = volume;

        // ������ ������ PlayerPrefs�� ����
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }*/
}
