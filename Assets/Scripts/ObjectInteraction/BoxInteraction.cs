using UnityEngine;

public class BoxInteraction : MonoBehaviour, IInteractable, IItemReceiver
{
    private const string _itemName = "Book";

    private ItemData _item;

    public void Interact()
    {
        Debug.Log("Interacted with the box. Waiting for the correct item to be received.");
        InventoryUi.Instance.OpenForReceiver(this);
    }

    public void ReceiveItem(int id)
    {
        _item = ItemMapping.Instance.GetItem(id);

        if (_item != null && _item.ItemName == _itemName)
        {
            Debug.Log($"Received item: {_item.ItemName}");
            // Add logic for what happens when the correct item is received
        }
        else
        {
            Debug.Log($"Received incorrect item: {(_item != null ? _item.ItemName : "null")}");
            // Add logic for what happens when the incorrect item is received
        }
    }
}