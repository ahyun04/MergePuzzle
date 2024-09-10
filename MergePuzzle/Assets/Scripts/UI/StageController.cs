using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    // Inspector���� �Ҵ��� �� �ֵ��� ������ ����
    public GameObject menuPanel;
    public Image stageImage; // Stage1 �̹���
    public Button openMenuButton;
    public Button closeMenuButton;

    void Start()
    {
        // ��ư Ŭ�� �̺�Ʈ�� �Լ� ����
        openMenuButton.onClick.AddListener(OpenMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);

        // �޴� �гΰ� �̹����� ����
        menuPanel.SetActive(false);
        stageImage.gameObject.SetActive(false);
    }

    void OpenMenu()
    {
        // �޴��� ���̵��� ����
        menuPanel.SetActive(true);
        stageImage.gameObject.SetActive(true); // Stage1 �̹����� ���̰� ��
    }

    void CloseMenu()
    {
        // �޴��� �̹����� ����
        menuPanel.SetActive(false);
        stageImage.gameObject.SetActive(false);
    }
}
