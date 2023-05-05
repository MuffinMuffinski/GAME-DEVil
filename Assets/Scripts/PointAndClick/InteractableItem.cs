using System;
using UnityEngine;
using TMPro;

public class InteractableItem : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textFieldItem;
    public string itemName;

    public void OnStartHovering()
    {
        textFieldItem.text = itemName;
    }

    public void OnStopHovering()
    {
        textFieldItem.text = "";
    }

    public void OnClickOnItem()
    {
        GameManager.Instance.OnItemClick(itemName);
    }
}
