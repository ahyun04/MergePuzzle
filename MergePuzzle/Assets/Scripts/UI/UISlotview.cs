using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISlotview : MonoBehaviour
{
    public GameObject uiPrefab; // ΩΩ∑‘ø° πËƒ°«“ «¡∏Æ∆’
    public Transform contentTrans; // ΩΩ∑‘ ƒ¡≈◊¿Ã≥ 
    private Slot[] slots; // ΩΩ∑‘ πËø≠

    public void Init()
    {
        // ∏µÁ ΩΩ∑‘¿ª ∞°¡Æø»
        slots = contentTrans.GetComponentsInChildren<Slot>();

        foreach (TestData data in DataManager.Instance.dicData.Values)
        {
            // ∫Û ΩΩ∑‘¿ª √£¿Ω
            Slot emptySlot = GetEmptySlot();
            if (emptySlot != null)
            {
                // icon_name¿ª ≈Î«ÿ ∫Û ΩΩ∑‘ø° æ∆¿Ã≈€ º≥¡§
                emptySlot.SetItem(data.icon_name);
            }
        }
    }

    // ∫Û ΩΩ∑‘ √£±‚
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
