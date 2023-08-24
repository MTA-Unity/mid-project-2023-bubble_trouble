using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 startForce;
    [SerializeField] private Rigidbody2D _ballRigidbody;
    [SerializeField] private GameObject nextBall;
    void Start()
    {
        // Trigger the event when a ball is created
        GameEvents.Instance.TriggerBallCreatedEvent();
        Debug.Log("Ball Created");
        _ballRigidbody.AddForce(startForce, ForceMode2D.Impulse);
    }

    public void Split()
    {
        if (nextBall != null)
        {
            GameObject ball1 = Instantiate(nextBall, _ballRigidbody.position + Vector2.right / 4f, Quaternion.identity);
            GameObject ball2 = Instantiate(nextBall, _ballRigidbody.position + Vector2.left / 4f, Quaternion.identity);

            ball1.GetComponent<Ball>().startForce = new Vector2(2f, 5f);
            ball2.GetComponent<Ball>().startForce = new Vector2(-2f, 5f);
        }
        
        Destroy(gameObject);
    }
    
    private void OnDestroy()
    {
        // Trigger the event when a ball is destroyed
        GameEvents.Instance.TriggerBallDestroyedEvent();
    }
}
