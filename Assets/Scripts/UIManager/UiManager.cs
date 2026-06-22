using UnityEngine;

public class UiManager:MonoBehaviour
{
    [SerializeField] private Canvas _inventoryCanvas;
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
        
    }

    private void Update()
    {
        //ToggleInventory();
    }

    public void ToggleInventory()
    {
        _inventoryCanvas.enabled = !_inventoryCanvas.enabled;

    }
    
}