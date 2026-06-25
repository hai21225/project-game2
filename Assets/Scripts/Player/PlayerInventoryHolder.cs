using UnityEngine;

public class PlayerInventoryHolder: MonoBehaviour, IInventoryHolder
{
    [SerializeField] private Inventory _playerInventory;
    [SerializeField] private string _displayName = "Player Inventory";

    public string DisplayName => _displayName;

    public Inventory GetInventory()
    {
        return _playerInventory;
    }
}