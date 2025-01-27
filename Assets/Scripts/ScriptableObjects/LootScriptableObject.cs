using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "ScriptableObjects/LootItem", order = 1)]
public class LootScriptableObject : BaseScriptableObject
{
    public string Name;
    public ItemTypes ItemType;
    public int Cost;
    public string Description;
    public Texture2D Image;
}
