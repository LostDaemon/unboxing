using UnityEngine;

[CreateAssetMenu(fileName = "CameraSettings", menuName = "ScriptableObjects/Settings/CameraSettings", order = 2)]
public class CameraSettingsScriptableObject : BaseScriptableObject
{
    public float RotationSpeed = 1f;
    public float MinTreshhold = 0.05f;
    public float AutoZoomMultiplier = 2f;
    public float PinchZoomSpeed = 1;
    public float MouseScrollZoomSpeed = 1;
}
