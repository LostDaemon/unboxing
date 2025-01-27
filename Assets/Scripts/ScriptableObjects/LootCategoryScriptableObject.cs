using Core.Items;
using UnityEngine;

[CreateAssetMenu(fileName = "LootCategory", menuName = "ScriptableObjects/LootCategory", order = 1)]
public class LootCategoryScriptableObject : BaseScriptableObject
{
    public ItemType ItemType;
    public string Description;
    public Texture Icon;
}
