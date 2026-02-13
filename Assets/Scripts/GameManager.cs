using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Score = 0;
    private ScoreUI _scoreUI;

    private void Awake()
    {
        _scoreUI = FindAnyObjectByType<ScoreUI>();
    }
    public void AddScore(int score)
    {
        this.Score += score;
        _scoreUI.UpdateScore();
    }
}
