using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

public class TimeSlotSlector : MonoBehaviour
{
    public List<GameObject> slots = new List<GameObject>();

    private GameObject selectedSlot;

    public Color selectColor;
    public Color normalColor;

    private void OnEnable()
    {
        SetSlotAsActive(slots[0]);
    }

    public void SetSlotAsActive(GameObject slot)
    {
        foreach(GameObject obj in slots) {
            if(obj.GetInstanceID() == slot.GetInstanceID()) {
                obj.GetComponent<Image>().color = selectColor;
                selectedSlot = slot;
            } else {
                obj.GetComponent<Image>().color = normalColor;
            }
        }
    }

    public GameObject GetSelectedSlot()
    {
        return selectedSlot;
    }
}
