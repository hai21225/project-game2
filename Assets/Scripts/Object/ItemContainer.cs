using Unity.Netcode;
using UnityEngine;

public class ItemContainer : NetworkBehaviour, IInteractable
{
    [SerializeField] private ItemData _itemData;

    private NetworkVariable<bool> _hasItem = new(true);




    public void Interact()
    {
        RequestInteractServerRpc();
    }

    [ServerRpc(RequireOwnership = false)]
    private void RequestInteractServerRpc(ServerRpcParams rpcParams =default)
    {
        if (!_hasItem.Value)
            return;

        ulong clientId = rpcParams.Receive.SenderClientId;

        _hasItem.Value = false;

        GiveItem(clientId);
    }

    private void GiveItem(ulong clientId)
    {
        Debug.Log($"Giving {_itemData.name} to client {clientId}");
        var player = NetworkManager.Singleton.ConnectedClients[clientId].PlayerObject;
        var inventory = player.GetComponent<Inventory>();
        inventory.AddItem(_itemData);
    }
}