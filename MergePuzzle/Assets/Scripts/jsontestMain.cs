/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Newtonsoft;
using Newtonsoft.Json;
using System.Linq;

public class jsontestMain : MonoBehaviour
{
    //public UIshopchestscrollview scollview;
    public UISlotview slotView;

    void Start()
    {
        if (slotView == null)
        {
            slotView = FindObjectOfType<UISlotview>();
        }

        // 데이터 로드
        DataManager.Instance.LoadData();

        foreach (KeyValuePair<int, TestData> pair in DataManager.Instance.dicData)
        {
            TestData data = pair.Value;
            Debug.Log($"{data.id}, {data.name}, {data.icon_name}");
        }

        // slotView가 null인지 확인
        if (slotView != null)
        {
            slotView.Init();
        }
        else
        {
            Debug.LogError("slotView가 설정되지 않았습니다. Unity Inspector에서 설정해주세요.");
        }
    }
}
*/