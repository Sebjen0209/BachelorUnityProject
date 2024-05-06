using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using Mirror;

[RequireComponent(typeof(NetworkTransformUnreliable))]
public class Player : NetworkBehaviour
{
    private void Update()
    {
        if (isLocalPlayer)
        {
            PlayerMovement();
        }
    }
    [SerializeField] private float movementSpeed = 1f;
    
    private PlayerInputSystem playerInput = null;
    private Vector2 movement;
    private Rigidbody2D rb;
    
    private void Awake()
    {
        playerInput = new PlayerInputSystem();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void PlayerMovement()
    {
        movement = playerInput.PlayerInputs.Movement.ReadValue<Vector2>();
    }

    private void Move()
    {
        rb.MovePosition(rb.position +  movement * (movementSpeed * Time.fixedDeltaTime));
    }
}
