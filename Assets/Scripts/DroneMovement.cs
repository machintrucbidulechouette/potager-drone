using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Déplace le drone dans le plan horizontal, à hauteur constante.
/// </summary>
public class DroneMovement : MonoBehaviour
{
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private float speed = 5f;

    private void OnEnable()
    {
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
    }

    private void Update()
    {
        Vector2 input = moveAction.action.ReadValue<Vector2>();
        Vector3 direction = new Vector3(input.x, 0f, input.y);
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
}