using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using System.Linq;
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

    public void RemoveItem()
    {

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
}