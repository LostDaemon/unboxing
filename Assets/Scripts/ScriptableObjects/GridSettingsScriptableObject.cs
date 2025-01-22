using UnityEngine;

[CreateAssetMenu(fileName = "GridSettings", menuName = "ScriptableObjects/Settings/GridSettings", order = 1)]
public class GridSettingsScriptableObject : BaseScriptableObject
{
    public int GridSizeX = 10;
    public int GridSizeY = 10;
    public int GridSizeZ = 10;
    public int PairsCount = 10;
    public int GenerationAttempts = 5;
    public Vector3 GridElementScale = Vector3.one;
}
