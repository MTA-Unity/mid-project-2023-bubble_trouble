using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

    public UnityEvent BallCreatedEvent = new UnityEvent();
    public UnityEvent BallDestroyedEvent = new UnityEvent();
    public UnityEvent LifeDecreaseEvent = new UnityEvent();
    public UnityEvent LifeIncrementEvent = new UnityEvent();
    public UnityEvent TimeUpEvent = new UnityEvent();

    void Awake()
    {
        // Singleton
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TriggerBallCreatedEvent()
    {
        BallCreatedEvent?.Invoke();
    }
    
    public void TriggerBallDestroyedEvent()
    {
        BallDestroyedEvent?.Invoke();
    }
    
    public void TriggerLifeDecreaseEvent()
    {
        LifeDecreaseEvent?.Invoke();
    }
    
    public void TriggerLifeIncrementEvent()
    {
        LifeIncrementEvent?.Invoke();
    }
    
    public void TriggerTimeUpEvent()
    {
        TimeUpEvent?.Invoke();
    }
}
