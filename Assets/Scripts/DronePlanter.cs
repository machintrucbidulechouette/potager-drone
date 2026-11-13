using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Fait apparaître une plantation sous le drone quand on appuie sur le bouton de plantation.
/// </summary>
public class DronePlanter : MonoBehaviour
{
    [SerializeField] private InputActionReference plantAction;
    [SerializeField] private GameObject plantationPrefab;
    [SerializeField] private float groundHeight = 0.15f;
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Collider[] plantableAreas;

    private void OnEnable()
    {
        plantAction.action.Enable();
        plantAction.action.performed += OnPlantPerformed;
    }

    private void OnDisable()
    {
        plantAction.action.performed -= OnPlantPerformed;
        plantAction.action.Disable();
    }

    private void OnPlantPerformed(InputAction.CallbackContext context)
    {
        Plant();
    }

    private void Plant()
    {
        if (Time.timeScale == 0f) return;
        if (!IsInPlantableArea(transform.position)) return;
        Vector3 position = new Vector3(transform.position.x, groundHeight, transform.position.z);
        GameObject newPlantation = Instantiate(plantationPrefab, position, Quaternion.identity);
        Plantation plantation = newPlantation.GetComponent<Plantation>();
        if (plantation != null)
        {
            plantation.Initialize(scoreManager, audioManager);
        }
        if (audioManager != null) audioManager.PlayPlant();
    }
    /// <summary>Renvoie true si la position (vue de dessus) est dans une zone plantable.</summary>
    private bool IsInPlantableArea(Vector3 position)
    {
        // Si aucune zone n'est définie, on autorise partout
        if (plantableAreas == null || plantableAreas.Length == 0) return true;

        foreach (Collider area in plantableAreas)
        {
            if (area == null) continue;

            // On teste à la hauteur du centre du collider pour ignorer la hauteur du drone
            Vector3 testPoint = new Vector3(position.x, area.bounds.center.y, position.z);
            if ((area.ClosestPoint(testPoint) - testPoint).sqrMagnitude < 0.0001f)
            {
                return true;
            }
        }
        return false;
    }
}