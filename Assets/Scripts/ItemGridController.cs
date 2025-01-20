using Unity.Collections;
using UnityEngine;

public class ItemGridController : MonoBehaviour
{
    public GridItem itemPrefab;
    public int ItemCount = 10;
    public int GridX = 3;
    public int GridY = 4;
    public int GridZ = 5;

    private GridItem[,,] _grid;

    private void Awake()
    {
        _grid = new GridItem[GridX, GridY, GridZ];
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var randomCount = Random.Range(1, ItemCount);
        for (int i = 0; i < randomCount; i++)
        {
            var x = Random.Range(0, GridX);
            var z = Random.Range(0, GridZ);
            var y = GetMaxColumnHeight(new Vector2Int(x, z)) + 1;

            var pos = new Vector3Int(x, y, z);

            if (CheckAvailability(pos))
            {
                var item = Instantiate(itemPrefab, pos, Quaternion.identity);
                _grid[x, y, z] = item;
            }
        }
    }

    private bool CheckAvailability(Vector3Int pos)
    {
        return _grid[pos.x, pos.y, pos.z] == null;
    }

    private bool CheckCanInteract(Vector3Int pos)
    {
        var top = pos + Vector3Int.up;
        var left = pos + Vector3Int.left;
        var right = pos + Vector3Int.right;
        var front = pos + Vector3Int.forward;
        var back = pos + Vector3Int.back;

        return _grid[top.x, top.y, top.z] == null
            && _grid[left.x, left.y, left.z] == null
            && _grid[right.x, right.y, right.z] == null
            && _grid[front.x, front.y, front.z] == null
            && _grid[back.x, back.y, back.z] == null;
    }

    private int GetMaxColumnHeight(Vector2Int pos)
    {
        int maxY = 0;
        for (var i = 0; i < GridY; i++)
        {
            if (_grid[pos.x, i, pos.y] != null)
            {
                maxY = i;
            }
        }

        return maxY;
    }
}
