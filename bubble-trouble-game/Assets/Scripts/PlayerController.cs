using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4f;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    
    private Vector2 _newPosition;

    private void Awake()
    {
        _newPosition = _playerRigidbody.position;
    }

    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");
        var horizontalMovementDelta = horizontalInput * Time.fixedDeltaTime * _movementSpeed;
        
        _newPosition = _playerRigidbody.position + Vector2.right * horizontalMovementDelta;
    }
    void FixedUpdate()
    {
        _playerRigidbody.MovePosition(_newPosition);
    }
}
