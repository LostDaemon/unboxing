using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : IDisposable
{
    public delegate void TouchHandler(Vector2 position);
    public event TouchHandler OnTouchStart;
    public event TouchHandler OnTouchEnd;

    private TouchInputSystem _touchInputSystem;

    [Inject]
    public InputManager(TouchInputSystem touchInputSystem)
    {
        _touchInputSystem = touchInputSystem;
        _touchInputSystem.Enable();
        _touchInputSystem.Touch.TouchPress.started += ctx => OnTouchStarted(ctx);
        _touchInputSystem.Touch.TouchPress.canceled += ctx => OnTouchEnded(ctx);
    }

    private void OnTouchEnded(InputAction.CallbackContext ctx)
    {
        var pos = _touchInputSystem.Touch.TouchPosition.ReadValue<Vector2>();
        OnTouchEnd?.Invoke(pos);
    }

    private void OnTouchStarted(InputAction.CallbackContext ctx)
    {
        var pos = _touchInputSystem.Touch.TouchPosition.ReadValue<Vector2>();
        OnTouchStart?.Invoke(pos);
    }

    public void Dispose()
    {
        _touchInputSystem.Disable();
        _touchInputSystem.Touch.TouchPress.started -= ctx => OnTouchStarted(ctx);
        _touchInputSystem.Touch.TouchPress.canceled -= ctx => OnTouchEnded(ctx);
    }
}
