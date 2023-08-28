using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _movementSpeed = 4f;
    [SerializeField] private Rigidbody2D _playerRigidbody;
    [SerializeField] private Animator _characterAnimator;

    private const string ShootTriggerParameterName = "ShootTrigger";
    private const string DirectionParameterName = "Direction";
    private const string MovingParameterName = "Moving";

    
    private static readonly int ShootTrigger = Animator.StringToHash(ShootTriggerParameterName);
    private static readonly int Direction = Animator.StringToHash(DirectionParameterName);
    private static readonly int Moving = Animator.StringToHash(MovingParameterName);


    
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

        var isClickingOnDirection = Input.GetKeyDown(KeyCode.LeftArrow) ||
                                    Input.GetKeyDown(KeyCode.RightArrow) ||
                                    Input.GetKey(KeyCode.LeftArrow) ||
                                    Input.GetKey(KeyCode.RightArrow); 

        _characterAnimator.SetBool(Moving, isClickingOnDirection);

        var mappedValue = (horizontalInput + 1) / 2f; 
        _characterAnimator.SetFloat(Direction, mappedValue);

        if(Input.GetKeyDown(KeyCode.Space)) {
            Debug.Log("Shoot");
            _characterAnimator.SetTrigger(ShootTrigger);
        }
    }
    void FixedUpdate()
    {
        _playerRigidbody.MovePosition(_newPosition);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.collider.tag.Contains("Ball"))
        {
            GameEvents.Instance.TriggerLifeDecreaseEvent();
        }
    }
}
