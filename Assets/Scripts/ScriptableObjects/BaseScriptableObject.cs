using System;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectClassIdAttribute : PropertyAttribute
{
}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(ScriptableObjectClassIdAttribute))]
public class ScriptableObjectClassIdDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        GUI.enabled = false;
        if (string.IsNullOrEmpty(property.stringValue)) property.stringValue = Guid.NewGuid().ToString();
        EditorGUI.PropertyField(position, property, label, true);
        GUI.enabled = true;
    }
}
#endif

public class BaseScriptableObject : ScriptableObject
{
    [ScriptableObjectClassId] public string ClassId;
}