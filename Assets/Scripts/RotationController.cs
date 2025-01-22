using UnityEngine;
using Zenject;

public class RotationController : MonoBehaviour
{
    public float RotationSpeed = 1f;
    public float MinTreshhold = 0.05f;
    private InputManager _inputManager;
    private Vector2? _currentPosition;

    [Inject]
    public void Construct(InputManager inputManager)
    {
        this._inputManager = inputManager;
    }

    private void OnEnable()
    {
        _inputManager.OnTouchEnd += OnTouchEnd;
        _inputManager.OnTouchPosition += OnTouchPosition;
    }

    private void OnDisable()
    {
        _inputManager.OnTouchEnd -= OnTouchEnd;
        _inputManager.OnTouchPosition -= OnTouchPosition;
    }

    private void OnTouchPosition(Vector2 position)
    {
        if (_currentPosition != null)
        {
            var dx = (position.x - _currentPosition.Value.x) * RotationSpeed * Time.deltaTime;
            if (Mathf.Abs(dx) > MinTreshhold)
            {
                transform.Rotate(Vector3.up, dx);
            }
        }

        _currentPosition = position;
    }

    private void OnTouchEnd(Vector2 position)
    {
        _currentPosition = null;
    }
}