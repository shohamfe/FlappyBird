using UnityEngine;

public class PipeGapTrigger : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!scored && other.CompareTag("Player"))
        {
            ScoreManager.Instance.AddScore(1);
            scored = true;
        }
    }

    public void ResetScoreFlag()
    {
        scored = false;
    }
}
