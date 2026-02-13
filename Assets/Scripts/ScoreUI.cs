using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI _score;
    private GameManager _gameManager;

    private void Awake()
    {
        _score= GetComponent<TextMeshProUGUI>();    
    }
    public void UpdateScore()
    {
        _gameManager = FindAnyObjectByType<GameManager>();
        _score.text = ("Score: " + _gameManager.Score);
    }

}