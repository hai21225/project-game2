using Cinemachine;
using Unity.Netcode;
using UnityEngine;

public class PlayerController: NetworkBehaviour
{

    [SerializeField] private float _moveSpeed = 5f;
    [SerializeField] private GameObject _flashLight;

    private NetworkVariable<bool> _isOff = new NetworkVariable<bool>(false);

    private Rigidbody2D _rb;

    private Camera _cam;

    //private bool _wasMoving=false;

    public override void OnNetworkSpawn()
    {
        _isOff.OnValueChanged += OnLightStateChange;
        OnLightStateChange(false, _isOff.Value);
        Debug.Log(
    $"Player:{name} " +
    $"IsOwner:{IsOwner} " +
    $"IsServer:{IsServer} " +
    $"OwnerId:{OwnerClientId}"
);

        if (!IsOwner)
        {
            return;
        }

        var vcam = FindObjectOfType<CinemachineVirtualCamera>();

        if (vcam != null)
        {
            vcam.Follow = transform;
        }
    }

    public override void OnNetworkDespawn()
    {
        _isOff.OnValueChanged -= OnLightStateChange;
    }
    private void Awake()
    {
        _rb= GetComponent<Rigidbody2D>();
        _cam= Camera.main;
    }
    private void Start()
    {
        Debug.Log(_rb.bodyType);
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

    private void FixedUpdate()
    {
        if(!IsOwner) return;
        Debug.Log("checlllllll");

        Movement();

    }

    private void Movement()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Debug.Log($"Input: {horizontal}, {vertical}");

        var _moveInput = new Vector2(horizontal, vertical).normalized;
        _rb.velocity= _moveInput* _moveSpeed;
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