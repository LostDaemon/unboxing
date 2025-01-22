using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : IDisposable
{
    public delegate void TouchHandler(Vector2 position);
    public event TouchHandler OnTouchStart;
    public event TouchHandler OnTouchEnd;
    public event TouchHandler OnTouchPosition;
    public TouchInputSystem TouchInputSystem { get; private set; }

    [Inject]
    public InputManager(TouchInputSystem touchInputSystem)
    {
        TouchInputSystem = touchInputSystem;
        TouchInputSystem.Enable();
        TouchInputSystem.Touch.TouchPress.started += ctx => OnTouchStarted(ctx);
        TouchInputSystem.Touch.TouchPress.canceled += ctx => OnTouchEnded(ctx);
        TouchInputSystem.Touch.TouchPosition.performed += ctx => OnTouchPositioned(ctx);
    }

    private void OnTouchPositioned(InputAction.CallbackContext ctx)
    {
        var pos = TouchInputSystem.Touch.TouchPosition.ReadValue<Vector2>();
        OnTouchPosition?.Invoke(pos);
    }

    private void OnTouchEnded(InputAction.CallbackContext ctx)
    {
        var pos = TouchInputSystem.Touch.TouchPosition.ReadValue<Vector2>();
        OnTouchEnd?.Invoke(pos);
    }

    private void OnTouchStarted(InputAction.CallbackContext ctx)
    {
        var pos = TouchInputSystem.Touch.TouchPosition.ReadValue<Vector2>();
        OnTouchStart?.Invoke(pos);
    }

    public void Dispose()
    {
        TouchInputSystem.Disable();
        TouchInputSystem.Touch.TouchPress.started -= ctx => OnTouchStarted(ctx);
        TouchInputSystem.Touch.TouchPress.canceled -= ctx => OnTouchEnded(ctx);
        TouchInputSystem.Touch.TouchPosition.performed -= ctx => OnTouchPositioned(ctx);
    }
}
