using UnityEngine;

namespace Core.Items
{
    public sealed class Item
    {
        public string Name { get; set; }
        public ItemType ItemType { get; set; }
        public float Cost { get; set; }
        public string Description { get; set; }
        public Sprite Image { get; set; }
        public ItemsRarity Rarity { get; set; }
    }
}