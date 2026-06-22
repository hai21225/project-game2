using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] private Transform _content;
    [SerializeField] private InventorySlotUi _slotPrefab;

    private Inventory _inventory;

    public void Bind(Inventory inventory)
    {
        if (_inventory != null)
        {
            _inventory.OnInventoryUpdated -= Refresh;
        }

        _inventory = inventory;

        _inventory.OnInventoryUpdated += Refresh;

        Refresh();
    }

    private void Refresh()
    {
        foreach (Transform child in _content)
        {
            Destroy(child.gameObject);
        }

        foreach (var itemId in _inventory.GetItems())
        {
            Debug.Log($"Adding item with ID: {itemId} to inventory UI");
            ItemData item = ItemMapping.Instance.GetItem(itemId);

            var slot = Instantiate(_slotPrefab, _content);

            slot.transform.localScale = Vector3.one;
            slot.transform.localPosition = Vector3.zero;

            slot.Setup(item);
        }
    }
}