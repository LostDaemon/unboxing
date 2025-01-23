using UnityEngine;
using Zenject;

public class CameraController : MonoBehaviour
{
    private float _rotationSpeed;
    private float _minTreshhold = 0.05f;
    private InputManager _inputManager;
    private Vector2? _currentPosition;

    [Inject]
    public void Construct(InputManager inputManager, GridSettingsScriptableObject gameSettings, CameraSettingsScriptableObject cameraSettings)
    {
        this._inputManager = inputManager;
        LoadSettings(gameSettings, cameraSettings);
    }

    private void LoadSettings(GridSettingsScriptableObject gameSettings, CameraSettingsScriptableObject cameraSettings)
    {
        var x = gameSettings.GridSizeX * gameSettings.GridElementScale.x / 2f;
        var z = gameSettings.GridSizeZ * gameSettings.GridElementScale.z / 2f;
        this.transform.position = new Vector3(x, 0f, z);
        var distance = Mathf.Max(x, z) * cameraSettings.AutoZoomMultiplier;
        this.GetComponentInChildren<Camera>().transform.localPosition = new Vector3(0, distance, -distance);

        _rotationSpeed = cameraSettings.RotationSpeed;
        _minTreshhold = cameraSettings.MinTreshhold;
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
            var dx = (position.x - _currentPosition.Value.x) * _rotationSpeed * Time.deltaTime;
            if (Mathf.Abs(dx) > _minTreshhold)
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