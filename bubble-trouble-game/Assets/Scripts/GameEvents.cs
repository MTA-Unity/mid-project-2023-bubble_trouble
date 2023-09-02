using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameEvents : MonoBehaviour
{
    public static GameEvents Instance { get; private set; }

    public UnityEvent ballCreatedEvent = new UnityEvent();
    public UnityEvent ballDestroyedEvent = new UnityEvent();
    public UnityEvent lifeDecreaseEvent = new UnityEvent();
    public UnityEvent lifeIncrementEvent = new UnityEvent();
    public UnityEvent timeUpEvent = new UnityEvent();

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
        ballCreatedEvent?.Invoke();
    }
    
    public void TriggerBallDestroyedEvent()
    {
        ballDestroyedEvent?.Invoke();
    }
    
    public void TriggerLifeDecreaseEvent()
    {
        lifeDecreaseEvent?.Invoke();
    }
    
    public void TriggerLifeIncrementEvent()
    {
        lifeIncrementEvent?.Invoke();
    }
    
    public void TriggerTimeUpEvent()
    {
        timeUpEvent?.Invoke();
    }
}
