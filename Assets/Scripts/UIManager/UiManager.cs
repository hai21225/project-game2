using UnityEngine;

public class UiManager:MonoBehaviour
{
    [SerializeField] private Canvas _inventoryCanvas;
    [SerializeField] private Canvas _inventoryTransferCanvas;

    public static UiManager Instance;
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

    private void Start()
    {
        _inventoryCanvas.enabled = false;
        _inventoryTransferCanvas.enabled = false;
        
    }
    
    //api inventory ui
    public void ToggleInventory(IInventoryHolder inventory)
    {
        if (_inventoryCanvas.enabled == true)
        {
            InventoryUi.Instance.Close();
            _inventoryCanvas.enabled = false;
        }
        else if (_inventoryCanvas.enabled == false)
        {
            InventoryUi.Instance.Open(inventory);
            _inventoryCanvas.enabled = true;
        }

    }
    

    // api inventory transfer ui
    public void ShowInventoryTransfer(IInventoryHolder source, IInventoryHolder target)
    {
        _inventoryTransferCanvas.enabled = true;
        InventoryTransferUi.Instance.Open(source, target);
    }
    public void HideInventoryTransfer()
    {
        InventoryTransferUi.Instance.Close();
        _inventoryTransferCanvas.enabled = false;
    }

    public void ToggleInventoryTransfer(IInventoryHolder source, IInventoryHolder target)
    {
        if (_inventoryTransferCanvas.enabled == true)
        {
            InventoryTransferUi.Instance.Close();
            _inventoryTransferCanvas.enabled = false;
        }
        else if (_inventoryTransferCanvas.enabled == false)
        {
            _inventoryTransferCanvas.enabled = true;
            InventoryTransferUi.Instance.Open(source, target);
        }
    }

    public bool IsInventoryTransferOpen => _inventoryTransferCanvas.enabled;
}