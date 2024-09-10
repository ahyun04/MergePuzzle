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

        // 아이콘 이름으로 리소스를 불러오고 이미지 설정
        Sprite iconSprite = Resources.Load<Sprite>(data.icon_name);
        if (iconSprite != null)
        {
            iconImage.sprite = iconSprite;
        }
        iconImage.SetNativeSize();
    }
}
