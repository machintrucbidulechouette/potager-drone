using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Boutons de l'écran titre.
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private string gameSceneName = "Game";

    public void Play()
    {
        SceneManager.LoadScene(gameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}