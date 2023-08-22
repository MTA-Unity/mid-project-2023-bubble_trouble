using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] private Transform _playerTransform;

    private bool isFired;

    void Update()
    {
        if (isFired)
        {
            
        }
        else
        {
            transform.position = _playerTransform.position;
        }
    }
}
