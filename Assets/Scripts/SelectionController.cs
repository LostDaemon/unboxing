using UnityEngine;

public class SelectionController : MonoBehaviour
{
    public InputManager inputManager; //TODO: USE DI
    private void OnEnable()
    {
        inputManager.OnTouchStart += OnTouchStart;
    }

    private void OnDisable()
    {
        inputManager.OnTouchStart -= OnTouchStart;
    }

    private void OnTouchStart(Vector2 position)
    {
        var ray = Camera.main.ScreenPointToRay(position);
        if (Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            GameObject hitObject = hitInfo.collider.gameObject;
            if (hitObject.TryGetComponent<IInteractive>(out var interactive))
            {
                interactive.Interact();
            }
        }
    }
}
