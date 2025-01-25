using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class ItemGridController : MonoBehaviour
{
    public GridItem[] itemPrefabs;
    public FxController fxController;
    public CoinsEffectController coinEffectController;
    private Vector3 _gridElementScale;



    private int _generationAttempts;
    private int _pairsCount;
    private int _gridX;
    private int _gridY;
    private int _dridZ;

    private GameManager _gameManager;

    [Inject]
    public void Construct(GridSettingsScriptableObject gameSettings, GameManager gameManager)
    {
        _gameManager = gameManager;
        LoadSettings(gameSettings);
    }

    private void LoadSettings(GridSettingsScriptableObject settings)
    {
        _gridX = settings.GridSizeX;
        _gridY = settings.GridSizeY;
        _dridZ = settings.GridSizeZ;
        _pairsCount = settings.PairsCount;
        _generationAttempts = settings.GenerationAttempts;
        _gridElementScale = settings.GridElementScale;
    }

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

    private void Awake()
    {
        _flatGrid = new Dictionary<Vector3Int, GridItem>();
    }

    private void Start()
    {

        if (!FillGrid(_generationAttempts))
        {
            Debug.LogError($"Failed to fill grid after {_generationAttempts} attempts");
        }
    }

    private bool FillGrid(int attemptsCount = 5)
    {
        for (int i = 0; i < attemptsCount; i++)
        {
            if (GenerateItems())
            {
                return true;
            }
        }
        return false;
    }

    private bool GenerateItems()
    {
        for (int i = 0; i < _pairsCount; i++)
        {
            var prefab = GetRandomPrefab();

            var firstPosition = GetAvailablePosition();
            if (!firstPosition.HasValue)
            {
                return false;
            }
            AddTile(prefab, firstPosition.Value);

            var secondPosition = GetAvailablePosition(firstPosition.Value);
            if (!secondPosition.HasValue)
            {
                return false;
            }
            AddTile(prefab, secondPosition.Value);
        }
        return true;
    }

    private void AddTile(GridItem prefab, Vector3Int position)
    {
        var truePosition = Grid2WorldPosition(position);
        var item = Instantiate(prefab, truePosition, Quaternion.identity);
        item.GridPosition = position;
        item.OnInteraction += OnInteraction;
        _flatGrid.Add(position, item);
    }

    private Vector3Int? GetAvailablePosition(Vector3Int? forbiddenPosition = null, int attemptsCount = 5)
    {
        for (int i = 0; i < attemptsCount; i++)
        {
            var x = Random.Range(0, _gridX);
            var z = Random.Range(0, _dridZ);
            var y = GetMaxColumnHeight(new Vector2Int(x, z)) + 1;
            var pos = new Vector3Int(x, y, z);
            if (CheckAvailability(pos) && !pos.Equals(forbiddenPosition))
            {
                return pos;
            }
        }
        return null;
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
        var pos = item.GridPosition;
        var top = pos + Vector3Int.up;
        var left = pos + Vector3Int.left;
        var right = pos + Vector3Int.right;
        var front = pos + Vector3Int.forward;
        var back = pos + Vector3Int.back;

        var covered = _flatGrid.ContainsKey(top);
        var onBorder = item.GridPosition.x == 0 || item.GridPosition.x == _gridX - 1 || item.GridPosition.z == 0 || item.GridPosition.z == _dridZ - 1;
        var closed = _flatGrid.ContainsKey(left) && _flatGrid.ContainsKey(right) && _flatGrid.ContainsKey(front) && _flatGrid.ContainsKey(back);
        return !covered && (onBorder || !closed);
    }

    private int GetMaxColumnHeight(Vector2Int pos)
    {
        int maxY = 0;
        for (var i = 0; i < _gridY; i++)
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
        _flatGrid.Remove(pos);
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
        if (source is GridItem item) //TODO Refactor
        {

            if (!CheckCanInteract(item))
            {
                DropSelection();
                return;
            }

            if (_selectedItem == null)
            {
                _selectedItem = item;
                _selectedItem.IsSelected = true;
                return;
            }

            if (_selectedItem == item)
            {
                DropSelection();
                return;
            }

            if (_selectedItem?.ItemType == item.ItemType)
            {
                DestroyPair(_selectedItem, item);
                DropSelection();
                return;
            }

            _selectedItem.IsSelected = false;
            _selectedItem = item;
            _selectedItem.IsSelected = true;
        }

        void DropSelection()
        {
            if (_selectedItem == null) return;
            _selectedItem.IsSelected = false;
            _selectedItem = null;
        }
    }

    private void CheckLooseConditions()
    {
        if (_flatGrid.Count == 0)
        {
            return;
        }

        if (!_flatGrid.Values
        .Where(c => CheckCanInteract(c))
        .GroupBy(c => c.ItemType)
        .Any(group => group.Count() > 1))
        {
            _gameManager.Loose();
        }
    }

    private void CheckWinConditions()
    {
        if (_flatGrid.Count == 0)
        {
            _gameManager.Win();
        }
    }

    private Vector3 Grid2WorldPosition(Vector3Int gridPosition)
    {
        return new Vector3(gridPosition.x * _gridElementScale.x, gridPosition.y * _gridElementScale.y, gridPosition.z * _gridElementScale.z);
    }



    private void DestroyPair(GridItem itemA, GridItem itemB)
    {
        var itemAPos = Grid2WorldPosition(itemA.GridPosition);
        var itemBPos = Grid2WorldPosition(itemB.GridPosition);
        fxController.ShowYellowSparks(itemAPos);
        fxController.ShowYellowSparks(itemBPos);
        coinEffectController.SpawnCoins(itemAPos, 5);
        coinEffectController.SpawnCoins(itemBPos, 5);
        Destroy(itemA.gameObject);
        Destroy(itemB.gameObject);
        RemoveItem(itemA.GridPosition);
        RemoveItem(itemB.GridPosition);
        _gameManager.AddScore();
        CheckWinConditions();
        CheckLooseConditions();
    }
}
