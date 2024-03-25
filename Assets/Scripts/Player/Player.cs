using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
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

    private void Update()
    {
        PlayerMovement();
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
