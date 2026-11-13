using TMPro;
using UnityEngine;

/// <summary>
/// Affiche le meilleur score sur l'écran titre.
/// </summary>
public class BestScoreDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text bestScoreText;

    private void Start()
    {
        bestScoreText.text = "Meilleur score : " + HighScore.Get();
    }
}