using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Arrose les plantations situées sous le drone tant que le bouton d'arrosage est maintenu.
/// </summary>
public class DroneWatering : MonoBehaviour
{
    [SerializeField] private InputActionReference waterAction;
    [SerializeField] private ParticleSystem waterJet;
    [SerializeField] private float growthBonus = 2.5f;
    [SerializeField] private AudioSource waterSound;

    // Plantations actuellement sous le drone
    private readonly List<Plantation> plantationsUnderDrone = new List<Plantation>();
    private bool isWatering;

    private void OnEnable()
    {
        waterAction.action.Enable();
    }

    private void OnDisable()
    {
        waterAction.action.Disable();
    }

    private void Update()
    {
        bool pressed = waterAction.action.IsPressed();

        if (pressed != isWatering)
        {
            isWatering = pressed;
            SetWatering(isWatering);
        }
    }

    private void SetWatering(bool watering)
    {
        if (watering)
        {
            waterJet.Play();
            if (waterSound != null) waterSound.Play();
        }
        else
        {
            waterJet.Stop();
            if (waterSound != null) waterSound.Stop();
        }

        // On retire les références vides (plantations détruites entre-temps)
        plantationsUnderDrone.RemoveAll(p => p == null);

        foreach (Plantation p in plantationsUnderDrone)
        {
            p.GrowthSpeed = watering ? growthBonus : 1f;
        }
    }

    /// <summary>Appelé par WaterZone quand une plantation entre dans la zone.</summary>
    public void OnPlantationEnter(Plantation plantation)
    {
        if (!plantationsUnderDrone.Contains(plantation))
        {
            plantationsUnderDrone.Add(plantation);
        }
        if (isWatering) plantation.GrowthSpeed = growthBonus;
    }

    /// <summary>Appelé par WaterZone quand une plantation sort de la zone.</summary>
    public void OnPlantationExit(Plantation plantation)
    {
        plantationsUnderDrone.Remove(plantation);
        if (plantation != null) plantation.GrowthSpeed = 1f;
    }
}