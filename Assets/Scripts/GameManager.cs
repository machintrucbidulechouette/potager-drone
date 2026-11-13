using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

/// <summary>
/// Gère le déroulement de la partie : objectif, pause, fin, recommencer, quitter.
/// </summary>
public class GameManager : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private InputActionReference pauseAction;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject endPanel;

    [Header("Réglages")]
    [SerializeField] private int targetScore = 100;
    [SerializeField] private string menuSceneName = "MainMenu";
    [SerializeField] private TMPro.TMP_Text endScoreText;

    private bool isPaused;
    private bool isOver;

    private void OnEnable()
    {
        scoreManager.OnScoreChanged += CheckObjective;
        pauseAction.action.Enable();
        pauseAction.action.performed += OnPausePerformed;
    }

    private void OnDisable()
    {
        scoreManager.OnScoreChanged -= CheckObjective;
        pauseAction.action.performed -= OnPausePerformed;
        pauseAction.action.Disable();
    }

    private void Start()
    {
        // Le temps peut être resté à 0 après une partie précédente
        Time.timeScale = 1f;
        pausePanel.SetActive(false);
        endPanel.SetActive(false);
    }

    private void OnPausePerformed(InputAction.CallbackContext context)
    {
        if (isOver) return;
        SetPaused(!isPaused);
    }

    private void SetPaused(bool paused)
    {
        isPaused = paused;
        pausePanel.SetActive(paused);
        Time.timeScale = paused ? 0f : 1f;
    }

    private void CheckObjective(int score)
    {
        if (isOver || score < targetScore) return;

        isOver = true;
        HighScore.TrySet(score);
        UpdateEndPanelTexts(score);
        endPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    // --- Méthodes appelées par les boutons ---

    public void Resume()
    {
        SetPaused(false);
    }

    public void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(menuSceneName);
    }
    private void UpdateEndPanelTexts(int score)
    {
        if (endScoreText != null)
        {
            endScoreText.text = "Score : " + score + "\nMeilleur score : " + HighScore.Get();
        }
    }
}