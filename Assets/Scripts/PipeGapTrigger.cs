using UnityEngine;

public class PipeGapTrigger : MonoBehaviour
{
    private bool scored = false;

    private void OnTriggerExit2D(Collider2D other)
    {
        if (!scored && other.CompareTag("Player"))
        {
            scored = true;
            ScoreManager.Instance.AddScore(1);
        }
    }

    public void ResetScoreFlag()
    {
        scored = false;
    }
}