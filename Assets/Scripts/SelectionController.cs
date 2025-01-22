using UnityEngine;
using Zenject;

public class SelectionController : MonoBehaviour
{
    private InputManager _inputManager;

    [Inject]
    public void Construct(InputManager inputManager)
    {
        this._inputManager = inputManager;
    }

    private void OnEnable()
    {
        _inputManager.OnTouchStart += OnTouchStart;
    }

    private void OnDisable()
    {
        _inputManager.OnTouchStart -= OnTouchStart;
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
