using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 startForce;
    [SerializeField] private Rigidbody2D _rb;
    void Start()
    {
        _rb.AddForce(startForce, ForceMode2D.Impulse);
    }
}
