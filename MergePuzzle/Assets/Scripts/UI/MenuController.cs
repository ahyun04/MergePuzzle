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

    private bool isMenuVisible = false;

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ����
        openMenuButton.onClick.AddListener(OpenMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);

        // �޴� �г��� ����
        menuPanel.SetActive(false);
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
}
