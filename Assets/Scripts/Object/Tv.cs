using Unity.Netcode;
using UnityEngine;

public class Tv : NetworkBehaviour,IInteractable
{
    [SerializeField] private Animator _animator;

    private NetworkVariable<bool> _turnOff = new(
        true,
        NetworkVariableReadPermission.Everyone,
        NetworkVariableWritePermission.Server
    );

    private void OnEnable()
    {
        _turnOff.OnValueChanged += OnTvStateChanged;
        OnTvStateChanged(false, _turnOff.Value);
    }

    private void OnDisable()
    {
        _turnOff.OnValueChanged -= OnTvStateChanged;
    }

    private void Start()
    {

        _animator.SetBool("isOff", _turnOff.Value);
    }

    private void OnTvStateChanged(bool previous, bool current)
    {
        _animator.SetBool("isOff", current);
    }

    public void Interact()
    {
        if (IsServer)
        {
            ToggleTv();
        }
        else
        {
            ToggleTvServerRpc();
        }

    }

    [ServerRpc(RequireOwnership = false)]
    private void ToggleTvServerRpc()
    {
        ToggleTv();
    }

    private void ToggleTv()
    {
        _turnOff.Value = !_turnOff.Value;
    }
}