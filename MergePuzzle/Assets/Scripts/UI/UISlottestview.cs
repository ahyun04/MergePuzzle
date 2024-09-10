using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class UISlottestview : MonoBehaviour
{
    public Image iconImage;
    private int id;

    public void Init(int id)
    {
        this.id = id;
        TestData data = DataManager.Instance.GetData(id);

        // ������ �̸����� ���ҽ��� �ҷ����� �̹��� ����
        Sprite iconSprite = Resources.Load<Sprite>(data.icon_name);
        if (iconSprite != null)
        {
            iconImage.sprite = iconSprite;
        }
        iconImage.SetNativeSize();
    }
}
