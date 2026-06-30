using Unity.Netcode;
using UnityEngine;

public class GameManager: NetworkBehaviour
{
    [SerializeField] private BoxLogic[] _boxes;

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
            return;

        foreach (var box in _boxes)
        {
            box.OnStateChanged += CheckBoxes;
        }

        CheckBoxes(); // kiểm tra luôn trạng thái ban đầu nếu cần
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer)
            return;

        foreach (var box in _boxes)
        {
            box.OnStateChanged += CheckBoxes;
        }

    }


    private void CheckBoxes() {

        foreach (var box in _boxes)
        {
            if (!box.IsCorrect)
            {
                return;
            }
        }
        Debug.Log("win");
    }

}