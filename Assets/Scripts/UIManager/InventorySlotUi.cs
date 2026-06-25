using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlotUi : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _nameText;
    [SerializeField] private Button _button;

    public event Action<int> OnClick;

    private int _itemId;

    private void Awake()
    {
        _button.onClick.AddListener(()=>
        {
            OnClick?.Invoke(_itemId);
        });
    }

    public void Setup(ItemData item)
    {
        _itemId = item.Id;

        _icon.sprite = item.Icon;
        _nameText.text = item.ItemName;
    }

    public void Clear()
    {
        OnClick = null;
    }
}