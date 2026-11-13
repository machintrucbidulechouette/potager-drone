using UnityEngine;

/// <summary>
/// Garde le drone à l'intérieur d'une zone de forme libre, définie par des colliders.
/// </summary>
public class DroneBounds : MonoBehaviour
{
    [SerializeField] private Collider[] areaColliders;

    private void LateUpdate()
    {
        if (areaColliders == null || areaColliders.Length == 0) return;

        Vector3 pos = transform.position;

        // Si on est déjà dans une des zones, rien à faire
        foreach (Collider area in areaColliders)
        {
            if (area == null) continue;
            if (IsInside(area, pos)) return;
        }

        // Sinon, on se recale sur le point le plus proche parmi toutes les zones
        Vector3 bestPoint = pos;
        float bestDistance = float.MaxValue;

        foreach (Collider area in areaColliders)
        {
            if (area == null) continue;
            Vector3 closest = area.ClosestPoint(pos);
            float distance = (closest - pos).sqrMagnitude;
            if (distance < bestDistance)
            {
                bestDistance = distance;
                bestPoint = closest;
            }
        }

        transform.position = bestPoint;
    }

    private bool IsInside(Collider area, Vector3 point)
    {
        // ClosestPoint renvoie le point lui-même s'il est à l'intérieur du collider
        return (area.ClosestPoint(point) - point).sqrMagnitude < 0.0001f;
    }
}