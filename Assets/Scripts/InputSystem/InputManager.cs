using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{

    public delegate void TouchHandler(Vector2 position);
    public event TouchHandler OnTouchStart;
    public event TouchHandler OnTouchEnd;

    private TouchInputSystem _touchInputSystem;

    private void Awake()
    {
        _touchInputSystem = new TouchInputSystem();
    }

    private void OnEnable()
    {
        _touchInputSystem.Enable(); //!
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

    private void OnDisable()
    {
        _touchInputSystem.Disable();
        _touchInputSystem.Touch.TouchPress.started -= ctx => OnTouchStarted(ctx);
        _touchInputSystem.Touch.TouchPress.canceled -= ctx => OnTouchEnded(ctx);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
