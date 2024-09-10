using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Image slotImage; // ���Կ� ǥ���� �̹���
    public bool isOccupied = false; // ������ �� �ִ��� ����
    public Vector2 imageSize = new Vector2(100, 100); // �̹����� �⺻ ũ�� ���� (100x100)

    // ������ �̸��� ���� �̹����� �����ϴ� �Լ�
    public void SetItem(string iconName)
    {
        if (isOccupied)
        {
            Debug.LogWarning("Slot already occupied");
            return;
        }

        // icon_name�� ���� ���ҽ����� ��������Ʈ �ε�
        Sprite iconSprite = Resources.Load<Sprite>(iconName);
        if (iconSprite != null)
        {
            slotImage.sprite = iconSprite; // �̹��� ����

            // ũ�⸦ �����ϰ� ����
            slotImage.rectTransform.sizeDelta = imageSize;
        }
        else
        {
            Debug.LogError($"Icon with name {iconName} not found in Resources.");
        }

        isOccupied = true; // ������ ��� ������ ǥ��
    }

    public void ClearSlot()
    {
        slotImage.sprite = null; // ���� �̹����� �ʱ�ȭ
        isOccupied = false; // ������ ��� �������� ǥ��
    }
}
