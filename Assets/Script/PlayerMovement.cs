using System.Collections;
using System.Collections.Generic;
using Unity.Services.Matchmaker.Models;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    [Header("References")]
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform bodyTransform;
    [SerializeField] private Rigidbody2D rb2D;

    [Header("Settings")]
    [SerializeField] private float movementSpeed = 4f;
    [SerializeField] private float tunningRate = 100f;
    private Vector2 previousMovementInpult;

    public override void OnNetworkSpawn()
    {
        if (!IsOwner)
        {
            return;
        }

        inputReader.MoveEvent += HandleMove;
    }

    public override void OnNetworkDespawn()
    {
        if (!IsOwner)
        {
            return;
        }

        inputReader.MoveEvent -= HandleMove;
    }

    private void Update()
    {
        if (!IsOwner)
        {
            return;
        }

        float zRotation = previousMovementInpult.x * -tunningRate * Time.deltaTime;

        bodyTransform.Rotate(0f, 0f, zRotation);
    }

    private void FixedUpdate()
    {
        if (!IsOwner)
        {
            return;
        }

        rb2D.velocity = (Vector2)bodyTransform.up * previousMovementInpult.y * movementSpeed;

    }

    private void HandleMove(Vector2 movementInpult)
    {
        previousMovementInpult = movementInpult;
    }


}
