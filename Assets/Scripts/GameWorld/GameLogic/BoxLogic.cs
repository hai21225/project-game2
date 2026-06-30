using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BoxLogic : NetworkBehaviour
{
    [SerializeField] private ItemContainer _items;
    [SerializeField] private ItemData[] _mainItem;

    public event Action OnStateChanged;
    public bool IsCorrect { private set; get; }

    public override void OnNetworkSpawn()
    {
        _items.GetInventory().OnInventoryUpdated += CheckChange;
    }

    public override void OnNetworkDespawn()
    {
        _items.GetInventory().OnInventoryUpdated -= CheckChange;
    }

    private void CheckChange()
    {
        if (!IsServer)
            return;

        bool correct = Check();

        if (correct != IsCorrect)
        {
            IsCorrect = correct;
            OnStateChanged?.Invoke();
        }
    }

    private bool Check()
    {
        List<int> items = _items.GetInventory().GetItems();

        if (items.Count != _mainItem.Length)
            return false;

        List<int> currentIds = new(items);
        List<int> targetIds = new();

        foreach (var item in _mainItem)
        {
            targetIds.Add(item.Id);
        }

        currentIds.Sort();
        targetIds.Sort();

        for (int i = 0; i < currentIds.Count; i++)
        {
            if (currentIds[i] != targetIds[i])
                return false;
        }

        return true;

    }
}