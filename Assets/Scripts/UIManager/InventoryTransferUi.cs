using TMPro;
using Unity.Netcode;
using UnityEngine;

public class InventoryTransferUi : MonoBehaviour
{
    [SerializeField] private InventorySlotUi _slotPrefab;

    [SerializeField] private Transform _sourceContent;
    [SerializeField] private Transform _targetContent;

    [SerializeField] private TMP_Text _sourceTitle;
    [SerializeField] private TMP_Text _targetTitle;

    private Inventory _sourceInventory;
    private Inventory _targetInventory;

    public static InventoryTransferUi Instance;// sigleton instance
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
        DrawInventory(_sourceInventory, _sourceContent, true);

        DrawInventory(_targetInventory, _targetContent, false);

        //UiManager.Instance.ToggleInventory();
    }

    public void Open(IInventoryHolder sourceInventory, IInventoryHolder targetInventory)
    {
        if (_sourceInventory != null)
            _sourceInventory.OnInventoryUpdated -= Refresh;

        if (_targetInventory != null)
            _targetInventory.OnInventoryUpdated -= Refresh;

        _sourceInventory = sourceInventory.GetInventory();
        _targetInventory = targetInventory.GetInventory();

        _sourceTitle.text = sourceInventory.DisplayName;
        _targetTitle.text = targetInventory.DisplayName;

        //Debug.Log(_sourceInventory.GetItems().Count + " items in source inventory");
        //Debug.Log(_targetInventory.GetItems().Count + " items in target inventory");

        _sourceInventory.OnInventoryUpdated += Refresh;
        _targetInventory.OnInventoryUpdated += Refresh;


        Refresh();
    }

    private void DrawInventory(Inventory inventory, Transform content, bool isSource)
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
                OnItemClicked(itemId, isSource);
            };
        }
    }

    private void OnItemClicked(int itemId, bool isSource)
    {
        Inventory from = isSource ? _sourceInventory : _targetInventory;
        Inventory to = isSource ? _targetInventory : _sourceInventory;

        Inventory playerInventory = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<Inventory>();

        playerInventory.TransferItemServerRpc(from.NetworkObject, to.NetworkObject, itemId);
    }

    public void Close()
    {
        if (_sourceInventory != null)
            _sourceInventory.OnInventoryUpdated -= Refresh;

        if (_targetInventory != null)
            _targetInventory.OnInventoryUpdated -= Refresh;

        _sourceInventory = null;
        _targetInventory = null;
    }
}

