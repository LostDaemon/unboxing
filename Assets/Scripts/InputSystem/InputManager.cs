using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

public class InputManager : IDisposable
{
    private bool IsPinching = false;
    private float InitialPinchDistance = 0;
    public delegate void TouchHandler(Vector2 position);
    public delegate void PinchHandler(float delta);
    public delegate void MouseScrollHandler(float delta);
    public event TouchHandler OnPrimaryTouchStart;
    public event TouchHandler OnPrimaryTouchEnd;
    public event TouchHandler OnPrimaryTouchPosition;
    public event TouchHandler OnSecondaryTouchPosition;
    public event PinchHandler OnPinch;
    public event MouseScrollHandler OnMouseScroll;

    public TouchInputSystem TouchInputSystem { get; private set; }

    [Inject]
    public InputManager(TouchInputSystem touchInputSystem)
    {
        TouchInputSystem = touchInputSystem;
        TouchInputSystem.Enable();
        TouchInputSystem.Touch.PrimaryTouchContact.started += ctx => OnPrimaryTouchStarted(ctx);
        TouchInputSystem.Touch.PrimaryTouchContact.canceled += ctx => OnPrimaryTouchEnded(ctx);
        TouchInputSystem.Touch.PrimaryTouchPosition.performed += ctx => OnPrimaryTouchPositioned(ctx);
        TouchInputSystem.Touch.SecondaryTouchContact.performed += ctx => OnSecondaryTouchPositioned(ctx);
        TouchInputSystem.Touch.MouseScroll.performed += OnMouseScrolled;
    }

    public void Dispose()
    {
        TouchInputSystem.Disable();
        TouchInputSystem.Touch.PrimaryTouchContact.started -= ctx => OnPrimaryTouchStarted(ctx);
        TouchInputSystem.Touch.PrimaryTouchContact.canceled -= ctx => OnPrimaryTouchEnded(ctx);
        TouchInputSystem.Touch.PrimaryTouchPosition.performed -= ctx => OnPrimaryTouchPositioned(ctx);
        TouchInputSystem.Touch.SecondaryTouchContact.performed -= ctx => OnSecondaryTouchPositioned(ctx);
        TouchInputSystem.Touch.MouseScroll.performed -= OnMouseScrolled;
    }

    private void OnPrimaryTouchPositioned(InputAction.CallbackContext ctx)
    {
        var pos = TouchInputSystem.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
        OnPrimaryTouchPosition?.Invoke(pos);
    }

    private void OnPrimaryTouchEnded(InputAction.CallbackContext ctx)
    {
        var pos = TouchInputSystem.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
        OnPrimaryTouchEnd?.Invoke(pos);
    }

    private void OnPrimaryTouchStarted(InputAction.CallbackContext ctx)
    {
        var pos = TouchInputSystem.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
        OnPrimaryTouchStart?.Invoke(pos);
    }

    private void OnSecondaryTouchPositioned(InputAction.CallbackContext context)
    {
        if (!TouchInputSystem.Touch.PrimaryTouchPosition.IsPressed())
        { return; }
        var pos = TouchInputSystem.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
        OnSecondaryTouchPosition(pos);
        DetectPinch();
    }

    private void DetectPinch()
    {
        if (!(TouchInputSystem.Touch.PrimaryTouchPosition.IsPressed() && TouchInputSystem.Touch.SecondaryTouchPosition.IsPressed()))
        {
            return;
        }

        var primaryPos = TouchInputSystem.Touch.PrimaryTouchPosition.ReadValue<Vector2>();
        var secondaryPos = TouchInputSystem.Touch.SecondaryTouchPosition.ReadValue<Vector2>();
        var currentDistance = Vector2.Distance(primaryPos, secondaryPos);

        if (!IsPinching)
        {
            InitialPinchDistance = currentDistance;
            IsPinching = true;
        }
        else
        {
            var delta = currentDistance - InitialPinchDistance;
            OnPinch?.Invoke(delta);
        }
    }

    private void OnMouseScrolled(InputAction.CallbackContext context)
    {
        var currentScroll = context.ReadValue<float>();
        OnMouseScroll?.Invoke(currentScroll);
    }

}
