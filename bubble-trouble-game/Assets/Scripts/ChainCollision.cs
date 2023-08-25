using UnityEngine;

public class ChainCollision : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Chain.isFired = false;

        if (other.tag == "Ball")
        {
            other.GetComponent<Ball>().Split();
        }
        GameManager.Instance.CheckLevelCleared();
    }
}
