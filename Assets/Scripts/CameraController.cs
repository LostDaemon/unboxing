using System;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using Zenject;

public class CameraController : MonoBehaviour
{
    private float _rotationSpeed;
    private float _minTreshhold = 0.05f;
    private InputManager _inputManager;
    private Vector2? _currentPosition;
    private CameraSettingsScriptableObject _cameraSettings;

    private float _maxFov = 60;
    private float _minFov = 20;
    private float _currentFov = 60;
    private Camera _camera;
    [Inject]
    public void Construct(InputManager inputManager, GridSettingsScriptableObject gameSettings, CameraSettingsScriptableObject cameraSettings)
    {
        _inputManager = inputManager;
        _cameraSettings = cameraSettings;
        LoadSettings(gameSettings, cameraSettings);
    }

    private void LoadSettings(GridSettingsScriptableObject gameSettings, CameraSettingsScriptableObject cameraSettings)
    {
        var x = (gameSettings.GridSizeX - 1) * gameSettings.GridElementScale.x / 2f;
        var z = (gameSettings.GridSizeZ - 1) * gameSettings.GridElementScale.z / 2f;
        this.transform.position = new Vector3(x, 0f, z);
        var distance = Mathf.Max(x, z) * cameraSettings.AutoZoomMultiplier;
        this.GetComponentInChildren<Camera>().transform.localPosition = new Vector3(0, distance, -distance);

        _rotationSpeed = cameraSettings.RotationSpeed;
        _minTreshhold = cameraSettings.MinTreshhold;
    }

    private void OnEnable()
    {
        _camera ??= this.GetComponentInChildren<Camera>();
        _inputManager.OnPrimaryTouchEnd += OnTouchEnd;
        _inputManager.OnPrimaryTouchPosition += OnTouchPosition;
        _inputManager.OnPinch += OnPinch;
        _inputManager.OnMouseScroll += OnMouseScroll;
    }

    private void OnMouseScroll(float delta)
    {
        ChangeFov(delta, _cameraSettings.MouseScrollZoomSpeed);
    }

    private void ChangeFov(float delta, float multiplier)
    {
        _currentFov = Mathf.Clamp(_currentFov - delta * multiplier, _minFov, _maxFov);
        _camera.fieldOfView = _currentFov;
    }

    private void OnPinch(float delta)
    {
        ChangeFov(delta, _cameraSettings.PinchZoomSpeed);
    }

    private void OnDisable()
    {
        _inputManager.OnPrimaryTouchEnd -= OnTouchEnd;
        _inputManager.OnPrimaryTouchPosition -= OnTouchPosition;
        _inputManager.OnPinch -= OnPinch;
        _inputManager.OnMouseScroll -= OnMouseScroll;
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