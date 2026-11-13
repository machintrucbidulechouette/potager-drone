using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Récolte les légumes mûrs qui se trouvent sous le drone.
/// </summary>
public class HarvestZone : MonoBehaviour
{

    // Plantations actuellement sous le drone
    private readonly List<Plantation> plantationsInZone = new List<Plantation>();

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HARVEST ENTER : " + other.name);
        Plantation plantation = other.GetComponentInParent<Plantation>();
        if (plantation != null && !plantationsInZone.Contains(plantation))
        {
            plantationsInZone.Add(plantation);
            Debug.Log("Plantation ajoutée, total : " + plantationsInZone.Count);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Plantation plantation = other.GetComponentInParent<Plantation>();
        if (plantation != null)
        {
            plantationsInZone.Remove(plantation);
        }
    }

    private void Update()
    {
        // On enlève les plantations détruites (éclatées ou récoltées)
        plantationsInZone.RemoveAll(p => p == null);

        // Parcours à l'envers : on peut retirer pendant la boucle sans casser l'index
        for (int i = plantationsInZone.Count - 1; i >= 0; i--)
        {
            if (plantationsInZone[i].TryHarvest())
            {
                plantationsInZone.RemoveAt(i);
            }
        }
    }
}