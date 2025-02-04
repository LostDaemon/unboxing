using System.Collections.Generic;
using UnityEngine;

namespace Core.Items
{

    public enum ItemsRarity
    {
        Undefined = 0,
        Trash,
        Common,
        Uncommon,
        Rare,
        Unique,
        Relic
    }

    public static class RarityColors
    {
        private static readonly Dictionary<ItemsRarity, Color> _colors = new Dictionary<ItemsRarity, Color>
    {
         { ItemsRarity.Undefined, new Color(0f, 0f, 0f, 1f) }, // Черный
        { ItemsRarity.Trash, new Color(0.8f, 0.8f, 0.8f, 1f) }, // Серый (мусор)
        { ItemsRarity.Common, new Color(1f, 1f, 1f, 1f) }, // Белый (обычный)
        { ItemsRarity.Uncommon, new Color(0.118f, 1f, 0f, 1f) }, // Зеленый (необычный)
        { ItemsRarity.Rare, new Color(0f, 0.439f, 1f, 1f) }, // Синий (редкий)
        { ItemsRarity.Unique, new Color(0.639f, 0.207f, 0.933f, 1f) }, // Фиолетовый (уникальный)
        { ItemsRarity.Relic, new Color(1f, 0.502f, 0f, 1f) } // Оранжевый (реликвия)
    };

        public static Color GetColor(ItemsRarity rarity)
        {
            return _colors[rarity];
        }
    }

    public static class RarityCostMultiplier
    {
        private static readonly Dictionary<ItemsRarity, float> _costMultipliers = new Dictionary<ItemsRarity, float>
    {
        { ItemsRarity.Trash, 0.5f },
        { ItemsRarity.Common, 1f },
        { ItemsRarity.Uncommon, 1.5f },
        { ItemsRarity.Rare, 2f },
        { ItemsRarity.Unique, 3f },
        { ItemsRarity.Relic, 5f }
    };

        public static float GetCostMultiplier(ItemsRarity rarity)
        {
            return _costMultipliers[rarity];
        }
    }

    public static class RarityWeights
    {
        private static readonly Dictionary<ItemsRarity, int> _rarityWeights = new Dictionary<ItemsRarity, int>
    {
        { ItemsRarity.Trash, 50 },
        { ItemsRarity.Common, 30 },
        { ItemsRarity.Uncommon, 15 },
        { ItemsRarity.Rare, 4 },
        { ItemsRarity.Unique, 1 },
        { ItemsRarity.Relic, 1 }
    };

        public static ItemsRarity GetRandomRarity()
        {
            int totalWeight = 0;
            foreach (var weight in _rarityWeights.Values)
            {
                totalWeight += weight;
            }

            int randomValue = Random.Range(0, totalWeight);
            int cumulativeWeight = 0;

            foreach (var rarity in _rarityWeights)
            {
                cumulativeWeight += rarity.Value;
                if (randomValue < cumulativeWeight)
                {
                    return rarity.Key;
                }
            }

            return ItemsRarity.Undefined;
        }
    }
}