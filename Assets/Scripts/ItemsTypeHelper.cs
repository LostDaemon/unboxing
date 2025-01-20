using System.Collections.Generic;
using UnityEngine;

public static class ItemsTypeHelper
{
    private static readonly Dictionary<ItemTypes, Color> _colors = new Dictionary<ItemTypes, Color>(){
        {ItemTypes.Type1, Color.red},
        {ItemTypes.Type2, Color.blue},
        {ItemTypes.Type3, Color.green}
    };

    public static Color GetColorByItemType(ItemTypes itemType)
    {
        return _colors[itemType]; //TODO
    }
}
