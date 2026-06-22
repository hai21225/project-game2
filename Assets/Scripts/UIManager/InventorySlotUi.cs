using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUi : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;

    public void Setup(ItemData item)
    {
        _icon.sprite = item.Icon;
        _nameText.text = item.ItemName;
    }
}