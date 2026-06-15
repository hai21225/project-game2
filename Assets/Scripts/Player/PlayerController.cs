using Cinemachine;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerController: NetworkBehaviour
{
    [SerializeField] private GameObject _flashLight;
    private NetworkVariable<bool> _isOff = new NetworkVariable<bool>(false);
    private Camera _cam;

    public static readonly List<PlayerController> Players = new();

    public override void OnNetworkSpawn()
    {
        _isOff.OnValueChanged += OnLightStateChange;
        OnLightStateChange(false, _isOff.Value);

        if (!IsOwner)
        {
            return;
        }

        var vcam = FindObjectOfType<CinemachineVirtualCamera>();

        if (vcam != null)
        {
            vcam.Follow = transform;
        }
        Players.Add(this);
    }

    public override void OnNetworkDespawn()
    {
        _isOff.OnValueChanged -= OnLightStateChange;
        Players.Remove(this);
    }
    private void Awake()
    {
        _cam= Camera.main;
    }
    private void Update()
    {
        if(!IsOwner)
        {
            return;
        }
        FlashLightRotation();
        TurnOffLight();
    }

    private void FlashLightRotation()
    {
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x < 0 || mousePos.x > Screen.width ||
            mousePos.y < 0 || mousePos.y > Screen.height)
        {
            return;
        }

        Vector3 mouseWorldPos = _cam.ScreenToWorldPoint(mousePos);
        mouseWorldPos.z = 0f;

        Vector3 dir = (mouseWorldPos - transform.position).normalized;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }

    private void TurnOffLight()
    {
        if (!IsOwner) return;

        if (Input.GetMouseButtonDown(1))
        {
            ToggleLightServerRpc();
        }

    }

    [ServerRpc] 
    private void ToggleLightServerRpc()
    {
        _isOff.Value = !_isOff.Value;
    }

    private void OnLightStateChange(bool previousValue, bool newValue)
    {
        _flashLight.SetActive(!newValue);
    }
}