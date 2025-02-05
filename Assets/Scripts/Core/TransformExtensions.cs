using UnityEngine;

public static class TransformExtensions
{
    public static void DeletChildren(this Transform parent)
    {
        int childCount = parent.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(parent.GetChild(i).gameObject);
        }
    }
}
