using UnityEngine;

/// <summary>
/// Joue les sons du jeu et la musique d'ambiance.
/// </summary>
public class AudioManager : MonoBehaviour
{
    [Header("Sources")]
    [SerializeField] private AudioSource sfxSource;
    [SerializeField] private AudioSource musicSource;

    [Header("Effets")]
    [SerializeField] private AudioClip plantClip;
    [SerializeField] private AudioClip harvestClip;
    [SerializeField] private AudioClip burstClip;
    [SerializeField, Range(0f, 1f)] private float sfxVolume = 0.7f;

    [Header("Musique")]
    [SerializeField] private AudioClip musicClip;
    [SerializeField, Range(0f, 1f)] private float musicVolume = 0.3f;

    private void Start()
    {
        musicSource.clip = musicClip;
        musicSource.loop = true;
        musicSource.volume = musicVolume;
        musicSource.Play();
    }

    public void PlayPlant() { Play(plantClip); }
    public void PlayHarvest() { Play(harvestClip); }
    public void PlayBurst() { Play(burstClip); }

    private void Play(AudioClip clip)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, sfxVolume);
    }
}