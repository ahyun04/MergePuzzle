using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlotview : MonoBehaviour
{
    public GameObject uiPrefab; // ���Կ� ��ġ�� ������
    public Transform contentTrans; // ���� �����̳�
    private Slot[] slots; // ���� �迭

    public void Init()
    {
        // ��� ������ ������
        slots = contentTrans.GetComponentsInChildren<Slot>();

        foreach (TestData data in DataManager.Instance.dicData.Values)
        {
            // �� ������ ã��
            Slot emptySlot = GetEmptySlot();
            if (emptySlot != null)
            {
                // icon_name�� ���� �� ���Կ� ������ ����
                emptySlot.SetItem(data.icon_name);
            }
        }
    }

    // �� ���� ã��
    private Slot GetEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            if (!slot.isOccupied)
            {
                return slot;
            }
        }

        Debug.LogWarning("No empty slots available.");
        return null;
    }
}
