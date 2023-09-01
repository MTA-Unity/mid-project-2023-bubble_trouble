using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Vector2 startForce;
    [SerializeField] private Rigidbody2D _ballRigidbody;
    [SerializeField] private GameObject nextBall;
    private AudioSource src;
    [SerializeField] private AudioClip ac;
    
    void Start()
    {
        Debug.Log("Ball Created");
        
        // Trigger the event when a ball is created
        GameEvents.Instance.TriggerBallCreatedEvent();
        _ballRigidbody.AddForce(startForce, ForceMode2D.Impulse);
        src = FindObjectOfType<AudioSource>();
    }

    public void Split()
    {
        Debug.Log("In Split");

        src.clip = ac;
        if (GameUI.Instance.IsAudioEnable())
        {
            src.Play();
        }

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

    public bool HasNextBall()
    {
        return nextBall;
    }
}
