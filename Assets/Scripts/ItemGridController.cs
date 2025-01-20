using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemGridController : MonoBehaviour
{
    public SelectionController selectionController; //TODO: USE DI
    public GridItem[] itemPrefabs;
    public int ItemCount = 10;
    public int GridX = 3;
    public int GridY = 4;
    public int GridZ = 5;

    private Dictionary<Vector3Int, GridItem> _flatGrid;
    private GridItem _selectedItem;

    private void OnEnable()
    {
        SubscribeAll();
    }

    private void OnDisable()
    {
        UnsubscribeAll();
    }

    private void SetSelected(GridItem source)
    {
        _selectedItem = source;
        Debug.Log(_selectedItem);
    }

    private void Awake()
    {

        _flatGrid = new Dictionary<Vector3Int, GridItem>();
    }

    private void Start()
    {
        for (int i = 0; i < ItemCount; i++)
        {
            var x = Random.Range(0, GridX);
            var z = Random.Range(0, GridZ);
            var y = GetMaxColumnHeight(new Vector2Int(x, z)) + 1;
            var pos = new Vector3Int(x, y, z);
            var prefab = GetRandomPrefab();

            if (CheckAvailability(pos))
            {
                var item = Instantiate(prefab, pos, Quaternion.identity);
                item.GridPosition = pos;
                item.OnInteraction += OnInteraction;
                _flatGrid.Add(pos, item);
            }
        }
    }

    private GridItem GetRandomPrefab()
    {
        return itemPrefabs[Random.Range(0, itemPrefabs.Length)];
    }

    private bool CheckAvailability(Vector3Int pos)
    {
        return !_flatGrid.ContainsKey(pos);
    }

    private bool CheckCanInteract(GridItem item)
    {
        if (item.GridPosition.x == 0 || item.GridPosition.x == GridX - 1 || item.GridPosition.z == 0 || item.GridPosition.z == GridZ - 1)
        {
            return true;
        }

        var pos = item.GridPosition;
        var top = pos + Vector3Int.up;
        var left = pos + Vector3Int.left;
        var right = pos + Vector3Int.right;
        var front = pos + Vector3Int.forward;
        var back = pos + Vector3Int.back;

        return !(_flatGrid.ContainsKey(top) || (_flatGrid.ContainsKey(left) && _flatGrid.ContainsKey(right) && _flatGrid.ContainsKey(front) && _flatGrid.ContainsKey(back)));
    }

    private int GetMaxColumnHeight(Vector2Int pos)
    {
        int maxY = 0;
        for (var i = 0; i < GridY; i++)
        {
            var curpos = new Vector3Int(pos.x, i, pos.y);

            if (_flatGrid.ContainsKey(curpos))
            {
                maxY = i;
            }
        }

        return maxY;
    }

    private void RemoveItem(Vector3Int pos)
    {
        if (_flatGrid.ContainsKey(pos))
        {
            _flatGrid.Remove(pos); //O(1) =)
        }
    }

    private void SubscribeAll()
    {
        _flatGrid.Values.ToList().ForEach(item => item.OnInteraction += OnInteraction);
    }

    private void UnsubscribeAll()
    {
        _flatGrid.Values.ToList().ForEach(item => item.OnInteraction -= OnInteraction);
    }

    private void OnInteraction(IInteractive source)
    {
        if (source is GridItem item)
        {
            if (!CheckCanInteract(item))
            {
                _selectedItem.Interact();
                SetSelected(null);
                return;
            }

            if (_selectedItem == null)
            {
                _selectedItem = item;
                SetSelected(item);
                return;
            }

            if (_selectedItem == item)
            {
                return;
            }

            if (_selectedItem.ItemType == item.ItemType)
            {
                Destroy(_selectedItem.gameObject);
                Destroy(item.gameObject);
                RemoveItem(_selectedItem.GridPosition);
                RemoveItem(item.GridPosition);
                SetSelected(null);
                return;
            }

            _selectedItem.Interact();
            SetSelected(item);
        }
    }
}
