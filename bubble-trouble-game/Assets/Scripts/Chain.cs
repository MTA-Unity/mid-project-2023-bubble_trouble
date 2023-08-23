using UnityEngine;

public class Chain : MonoBehaviour
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
