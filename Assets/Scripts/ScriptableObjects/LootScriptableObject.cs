using Core.Items;
using UnityEngine;

[CreateAssetMenu(fileName = "Loot", menuName = "ScriptableObjects/LootItem", order = 1)]
public class LootScriptableObject : BaseScriptableObject
{
    public string Name;
    public ItemType ItemType;
    public int Cost;
    public string Description;
    public Sprite Image;
}
