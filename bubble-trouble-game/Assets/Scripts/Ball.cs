using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 startForce;
    [SerializeField] private Rigidbody2D _ballRigidbody;
    void Start()
    {
        _ballRigidbody.AddForce(startForce, ForceMode2D.Impulse);
    }
}
