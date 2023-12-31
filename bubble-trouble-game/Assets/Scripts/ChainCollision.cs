using System;
using UnityEngine;

public class ChainCollision : MonoBehaviour
{
    // A bool to prevent multiple triggers of the ball hit by the chain
    private bool _isColliding;
    private void OnTriggerEnter2D(Collider2D other)
    {
        Chain.IsFired = false;
        
        if (other.tag.Contains("Ball") && !_isColliding)
        {
            Debug.Log("A ball has been hit");
            _isColliding = true;
            ScoreManager.Instance.AddBallHitScore(other.tag);

            // Check for winning condition - One ball in scene without nextBall
            var ball = other.GetComponent<Ball>();
            bool hasWon = GameManager.Instance.HasWonLevel(ball.HasNextBall());
            
            ball.Split();
            if (hasWon)
            {
                GameManager.Instance.ClearLevelWon();
            }
        }
    }

    private void Update()
    {
        _isColliding = false;
    }
}
