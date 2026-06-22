using System.Collections.Generic;
using UnityEngine;

public class ItemMapping : MonoBehaviour
{
    public static ItemMapping Instance;

    [SerializeField] private ItemData[] _items;
    private Dictionary<int, ItemData> _itemMap= new();

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
        foreach (var item in _items)
        {
            _itemMap[item.Id] = item;
        }
    }

    public ItemData GetItem(int id)
    {
        return _itemMap[id];
    }


}