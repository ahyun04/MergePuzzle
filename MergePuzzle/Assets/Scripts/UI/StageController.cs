using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageController : MonoBehaviour
{
    // Inspector에서 할당할 수 있도록 공개된 변수
    public GameObject menuPanel;
    public Image stageImage; // Stage1 이미지
    public Button openMenuButton;
    public Button closeMenuButton;

    void Start()
    {
        // 버튼 클릭 이벤트에 함수 연결
        openMenuButton.onClick.AddListener(OpenMenu);
        closeMenuButton.onClick.AddListener(CloseMenu);

        // 메뉴 패널과 이미지를 숨김
        menuPanel.SetActive(false);
        stageImage.gameObject.SetActive(false);
    }

    void OpenMenu()
    {
        // 메뉴를 보이도록 설정
        menuPanel.SetActive(true);
        stageImage.gameObject.SetActive(true); // Stage1 이미지를 보이게 함
    }

    void CloseMenu()
    {
        // 메뉴와 이미지를 숨김
        menuPanel.SetActive(false);
        stageImage.gameObject.SetActive(false);
    }
}
