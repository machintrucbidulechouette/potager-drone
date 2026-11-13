using UnityEngine;

/// <summary>
/// Lit et écrit le meilleur score dans PlayerPrefs.
/// </summary>
public static class HighScore
{
    private const string Key = "BestScore";

    /// <summary>Renvoie le meilleur score enregistré (0 au premier lancement).</summary>
    public static int Get()
    {
        return PlayerPrefs.GetInt(Key, 0);
    }

    /// <summary>Enregistre le score s'il bat le record. Renvoie true si c'est un nouveau record.</summary>
    public static bool TrySet(int score)
    {
        if (score <= Get()) return false;

        PlayerPrefs.SetInt(Key, score);
        PlayerPrefs.Save();
        return true;
    }
}