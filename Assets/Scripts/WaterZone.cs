using UnityEngine;

/// <summary>
/// Détecte les plantations dans la zone d'arrosage et prévient le drone.
/// </summary>
public class WaterZone : MonoBehaviour
{
    [SerializeField] private DroneWatering watering;

    private void OnTriggerEnter(Collider other)
    {

        Plantation plantation = other.GetComponentInParent<Plantation>();
        if (plantation != null)
        {
            watering.OnPlantationEnter(plantation);
        }
    }

    private void OnTriggerExit(Collider other)
    {
  
        Plantation plantation = other.GetComponentInParent<Plantation>();
        if (plantation != null)
        {
            watering.OnPlantationExit(plantation);
        }
    }
}