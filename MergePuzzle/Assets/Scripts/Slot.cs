using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image slotImage; // 슬롯에 표시할 이미지
    public bool isOccupied = false; // 슬롯이 차 있는지 여부
    public Vector2 imageSize = new Vector2(100, 100); // 이미지의 기본 크기 설정 (100x100)

    // 아이콘 이름을 통해 이미지를 설정하는 함수
    public void SetItem(string iconName)
    {
        if (isOccupied)
        {
            Debug.LogWarning("Slot already occupied");
            return;
        }

        // icon_name을 통해 리소스에서 스프라이트 로드
        Sprite iconSprite = Resources.Load<Sprite>(iconName);
        if (iconSprite != null)
        {
            slotImage.sprite = iconSprite; // 이미지 설정

            // 크기를 동일하게 설정
            slotImage.rectTransform.sizeDelta = imageSize;
        }
        else
        {
            Debug.LogError($"Icon with name {iconName} not found in Resources.");
        }

        isOccupied = true; // 슬롯이 사용 중임을 표시
    }

    public void ClearSlot()
    {
        slotImage.sprite = null; // 슬롯 이미지를 초기화
        isOccupied = false; // 슬롯을 비어 있음으로 표시
    }
}
