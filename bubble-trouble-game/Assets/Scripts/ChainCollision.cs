using UnityEngine;

public class ChainCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Chain.isFired = false;
        
        if (other.tag.Contains("Ball"))
        {
            bool _singleBall = GameManager.Instance.IsSingleBall();
            var ball = other.GetComponent<Ball>();
            ScoreManager.Instance.AddBallHitScore(other.tag);
            ball.Split();
            if (_singleBall && !ball.HasNextBall())
            {
                GameManager.Instance.LevelCleared();
            }
        }
    }
}
