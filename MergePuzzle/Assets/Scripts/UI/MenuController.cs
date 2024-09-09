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

    private bool isMenuVisible = false;

    void Start()
    {
        // 버튼 클릭 이벤트에 함수 연결
        openMenuButton.onClick.AddListener(OpenMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);

        // 메뉴 패널을 숨김
        menuPanel.SetActive(false);
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
}
