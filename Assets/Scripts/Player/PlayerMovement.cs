using System;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [SerializeField] private float _moveSpeed = 5f;
    

    private Rigidbody2D _rb;
    private void Awake()
    {
        _rb= GetComponent<Rigidbody2D>();
    }


    private void FixedUpdate()
    {
        if(!IsOwner)
        {
            return;
        }
        Move();
    }

    private void Move()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        Debug.Log($"Input: {horizontal}, {vertical}");

        var _moveInput = new Vector2(horizontal, vertical).normalized;
        _rb.velocity = _moveInput * _moveSpeed;
    }


}