using TMPro;
using UnityEngine;

/// <summary>
/// Affiche le score à l'écran et se met à jour à chaque changement.
/// </summary>
public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private TMP_Text scoreText;

    private void OnEnable()
    {
        scoreManager.OnScoreChanged += UpdateText;
        UpdateText(scoreManager.Score);
    }

    private void OnDisable()
    {
        scoreManager.OnScoreChanged -= UpdateText;
    }

    private void UpdateText(int score)
    {
        scoreText.text = "Score : " + score;
    }
}