using TMPro;
using Unity.Netcode;
using UnityEngine;

public class InventoryUi : MonoBehaviour
{
    [SerializeField] private InventorySlotUi _slotPrefab;
    [SerializeField] private Transform _content;
    [SerializeField] private TMP_Text _title;

    private Inventory _inventory;

    public static InventoryUi Instance;// sigleton instance
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }


    private void Refresh()
    {

        DrawInventory( _inventory, _content);

        //UiManager.Instance.ToggleInventory();
    }

    public void Open(IInventoryHolder inventory)
    {
        if (_inventory != null)
            _inventory.OnInventoryUpdated -= Refresh;

        _inventory = inventory.GetInventory();


        _title.text = inventory.DisplayName; 

        _inventory.OnInventoryUpdated += Refresh;

        Refresh();
    }

    private void DrawInventory(Inventory inventory, Transform content)
    {
        foreach (Transform child in content)
        {
            if (child.TryGetComponent<InventorySlotUi>(out var oldSlot))
            {
                oldSlot.Clear();
            }
            Destroy(child.gameObject);

        }

        foreach (var itemId in inventory.GetItems())
        {
            ItemData item =
                ItemMapping.Instance.GetItem(itemId);

            var slot =
                Instantiate(_slotPrefab, content);

            slot.transform.localScale = Vector3.one;
            slot.transform.localPosition = Vector3.zero;

            slot.Setup(item);
            slot.OnClick += itemId =>
            {
                OnItemClicked(itemId);
            };
        }
    }

    private void OnItemClicked(int itemId)
    {
        Debug.Log($"Clicked on item with ID: {itemId}");
    }

    public void Close()
    {
        if (_inventory != null)
            _inventory.OnInventoryUpdated -= Refresh;

        _inventory = null;
    }
}

