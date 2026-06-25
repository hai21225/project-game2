using Unity.Netcode;
using UnityEngine;

public class ItemContainer : NetworkBehaviour, IInteractable, IInventoryHolder
{
    [SerializeField] private ItemData _startItem;
    [SerializeField] private string _displayName = "name";

    [SerializeField] private ContainerInventory _containerInventory;

    public string DisplayName => _displayName;

    public void Interact()
    {
        //RequestInteractServerRpc();
        var playerLocalInventory = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponent<IInventoryHolder>();
        InventoryUi.Instance.Open(this, playerLocalInventory);
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)
        {
            _containerInventory.AddItem(_startItem);
        }
    }

    public override void OnNetworkDespawn()
    {
        
    }

    public Inventory GetInventory()
    {
        return _containerInventory;
    }
}