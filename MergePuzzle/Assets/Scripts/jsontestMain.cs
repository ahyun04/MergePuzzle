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

        // ������ �ε�
        DataManager.Instance.LoadData();

        foreach (KeyValuePair<int, TestData> pair in DataManager.Instance.dicData)
        {
            TestData data = pair.Value;
            Debug.Log($"{data.id}, {data.name}, {data.icon_name}");
        }

        // slotView�� null���� Ȯ��
        if (slotView != null)
        {
            slotView.Init();
        }
        else
        {
            Debug.LogError("slotView�� �������� �ʾҽ��ϴ�. Unity Inspector���� �������ּ���.");
        }
    }
}
*/