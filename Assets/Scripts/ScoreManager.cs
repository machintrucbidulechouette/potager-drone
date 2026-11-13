using System;
using UnityEngine;

/// <summary>
/// Garde le score de la partie et prévient ceux qui veulent le savoir quand il change.
/// </summary>
public class ScoreManager : MonoBehaviour
{
    public int Score { get; private set; }

    /// <summary>Événement déclenché à chaque changement de score (l'UI s'y abonnera en 3.2).</summary>
    public event Action<int> OnScoreChanged;

    public void AddPoints(int points)
    {

        Score += points;
        OnScoreChanged?.Invoke(Score);
    }

    public void ResetScore()
    {
        Score = 0;
        OnScoreChanged?.Invoke(Score);
    }
}