using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] private TextMeshProUGUI _scoreText;
    private int score = 0;

    private void Start()
    {
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        if (GameManager.Instance.CurrentState == GameState.Playing)
        {
            score += amount;
            UpdateScoreText();
        }
    }

    public void ResetScore()
    {
        score = 0;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        if (_scoreText != null)
        {
            _scoreText.text = score.ToString();
        }
    }

    public int GetScore()
    {
        return score;
    }
}
