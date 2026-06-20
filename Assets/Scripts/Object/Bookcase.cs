using Unity.Netcode;
using UnityEngine;

public class Bookcase : NetworkBehaviour, IInteractable
{
    [SerializeField] private ItemData _itemData;

    private NetworkVariable<bool> _hasItem = new(true);




    public void Interact()
    {
        if (!IsServer) return;

        if (!_hasItem.Value)
            return;

        _hasItem.Value = false;

        var book = Instantiate(
            _itemData.Prefab,
            transform.position,
            Quaternion.identity
        );

        book.GetComponent<NetworkObject>().Spawn();
    }



    

}