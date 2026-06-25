using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class Inventory : NetworkBehaviour
{
    private NetworkList<int> _items = new();

    public event Action OnInventoryUpdated;

    public void AddItem(ItemData item)
    {
        if (!IsServer) return;
        _items.Add(item.Id);
    }

    public override void  OnNetworkSpawn()
    {
        _items.OnListChanged += OnInventoryChanged;
    }
    public override void OnNetworkDespawn()
    {
        _items.OnListChanged -= OnInventoryChanged;
    }

    public void RemoveItem(int itemId)
    {
        if (!IsServer) return;
        _items.Remove(itemId);
    }
    private void OnInventoryChanged(NetworkListEvent<int> e)
    {
        Debug.Log("Inventory Changed");
        OnInventoryUpdated?.Invoke();
    }
    public List<int> GetItems()
    {
        List<int> result = new();

        foreach (var item in _items)
        {
            result.Add(item);
        }

        return result;
    }

    [ServerRpc(RequireOwnership = false)]
    public void TransferItemServerRpc(NetworkObjectReference sourceRef, NetworkObjectReference targetRef, int itemId)
    {
        if(!sourceRef.TryGet(out NetworkObject sourceObj))
        {
            return;
        }
        if (!targetRef.TryGet(out NetworkObject targetObj))
        {
            return;
        }
        Inventory sourceInventory = sourceObj.GetComponent<Inventory>();
        Inventory targetInventory = targetObj.GetComponent<Inventory>();

        if (sourceInventory==null || targetInventory == null)
        {
            return;
        }

        sourceInventory.RemoveItem(itemId);

        ItemData itemData= ItemMapping.Instance.GetItem(itemId);
        targetInventory.AddItem(itemData);

    }


    public override void OnDestroy()
    {
        _items?.Dispose();
        base.OnDestroy();
    }
}