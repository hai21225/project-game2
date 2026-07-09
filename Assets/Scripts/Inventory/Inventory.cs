using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
public class Inventory : NetworkBehaviour
{
    private NetworkList<InventoryItem> _items = new();

    public event Action OnInventoryUpdated;

    public event Action OnInventoryChange;

    public void AddItem(InventoryItem item)
    {
        if (!IsServer) return;
        _items.Add(item);
        OnInventoryChange?.Invoke();
    }

    public override void  OnNetworkSpawn()
    {
        _items.OnListChanged += OnInventoryChanged;
    }
    public override void OnNetworkDespawn()
    {
        _items.OnListChanged -= OnInventoryChanged;
    }

    public InventoryItem RemoveItem(int itemId)
    {
        if (!IsServer) return default;

        for (int i = 0; i < _items.Count; i++)
        {
            if (_items[i].ItemId == itemId)
            {
                InventoryItem item = _items[i];
                _items.RemoveAt(i);

                OnInventoryChange?.Invoke();

                return item;
            }
        }

        return default;
    }
    private void OnInventoryChanged(NetworkListEvent<InventoryItem> e)
    {
        Debug.Log("Inventory Changed");
        OnInventoryUpdated?.Invoke();
    }
    public List<InventoryItem> GetItems()
    {
        List<InventoryItem> result = new();

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

        InventoryItem item =sourceInventory.RemoveItem(itemId);

        //ItemData itemData= ItemMapping.Instance.GetItem(itemId);
        targetInventory.AddItem(item);

    }


    public void Clear()
    {
        if (!IsServer) return;
        _items.Clear();
        OnInventoryChange?.Invoke();
    }

    public override void OnDestroy()
    {
        _items?.Dispose();
        base.OnDestroy();
    }
}