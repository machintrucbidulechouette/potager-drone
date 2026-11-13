using UnityEngine;

/// <summary>
/// Les trois étapes de vie d'une plantation.
/// </summary>
public enum GrowthState
{
    Seed,
    Ripe,
    Overripe
}

/// <summary>
/// Gère la croissance d'une plantation : graine, légume mûr, légume trop mûr, éclatement.
/// </summary>
public class Plantation : MonoBehaviour
{
    [Header("Durées (en secondes)")]
    [SerializeField] private float seedDuration = 8f;
    [SerializeField] private float ripeDuration = 6f;
    [SerializeField] private float overripeDuration = 4f;

    [Header("Récolte")]
    [SerializeField] private int pointsOnHarvest = 10;

    [Header("Modèles par étape")]
    [SerializeField] private GameObject seedModel;
    [SerializeField] private GameObject ripeModel;
    [SerializeField] private GameObject overripeModel;

    [Header("Animation (optionnelle)")]
    [SerializeField] private Animator animator;

    [Header("Effets")]
    [SerializeField] private GameObject harvestEffectPrefab;
    [SerializeField] private GameObject burstEffectPrefab;

    /// <summary>Étape actuelle, lisible par les autres scripts (récolte, par exemple).</summary>
    public GrowthState State { get; private set; }

    /// <summary>Multiplicateur de vitesse, modifié par l'arrosage.</summary>
    public float GrowthSpeed { get; set; } = 1f;

    private float timer;
    private ScoreManager scoreManager;
    private AudioManager audioManager;

    private void Start()
    {
        SetState(GrowthState.Seed);
    }

    private void Update()
    {
        timer += Time.deltaTime * GrowthSpeed;

        switch (State)
        {
            case GrowthState.Seed:
                if (timer >= seedDuration) SetState(GrowthState.Ripe);
                break;
            case GrowthState.Ripe:
                if (timer >= ripeDuration) SetState(GrowthState.Overripe);
                break;
            case GrowthState.Overripe:
                if (timer >= overripeDuration) Burst();
                break;
        }
    }

    private void SetState(GrowthState newState)
    {
        State = newState;
        timer = 0f;

        // Un seul modèle visible à la fois
        seedModel.SetActive(newState == GrowthState.Seed);
        ripeModel.SetActive(newState == GrowthState.Ripe);
        overripeModel.SetActive(newState == GrowthState.Overripe);

        if (animator != null)
        {
            animator.SetTrigger(newState.ToString());
        }
    }

    private void Burst()
    {
        if (burstEffectPrefab != null)
        {
            Instantiate(burstEffectPrefab, transform.position, Quaternion.identity);
        }
        if (audioManager != null) audioManager.PlayBurst();
        Destroy(gameObject);
    }

    /// <summary>Injection : appelée par celui qui crée la plantation.</summary>
    public void Initialize(ScoreManager manager, AudioManager audio)
    {
        scoreManager = manager;
        audioManager = audio;
    }

    /// <summary>Récolte la plantation si elle est mûre. Renvoie true si la récolte a eu lieu.</summary>
    public bool TryHarvest()
    {
        if (State != GrowthState.Ripe) return false;

        if (scoreManager != null)
        {
            scoreManager.AddPoints(pointsOnHarvest);
        }
        if (harvestEffectPrefab != null)
        {
            Instantiate(harvestEffectPrefab, transform.position, Quaternion.identity);
        }
        if (audioManager != null) audioManager.PlayHarvest();
        Destroy(gameObject);
        return true;
    }
}