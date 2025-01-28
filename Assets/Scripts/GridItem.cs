using System;
using System.Linq;
using Core.Items;
using UnityEngine;
using Zenject;

public class GridItem : MonoBehaviour, IInteractive, IContainer
{
    public delegate void OnInteractionHandler(IInteractive source);
    public event OnInteractionHandler OnInteraction;
    private MaterialPropertyBlock _propertyBlock;
    private ItemType _itemType;
    private bool _isSelected;
    private LootCategoriesRepository _lootCategoriesRepository;

    public Renderer _iconRenderer;
    public Renderer _bodyRenderer;

    private Color _initialColor;

    [Inject]
    public void Construct(LootCategoriesRepository lootCategoriesRepository)
    {
        _lootCategoriesRepository = lootCategoriesRepository;
    }

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            DoSelection();
        }
    }

    public Vector3Int GridPosition { get; set; }

    public ItemType ItemType
    {
        get => _itemType;
        set
        {
            _itemType = value;
            DrawItemType(_itemType);
        }
    }

    public void Interact()
    {
        OnInteraction?.Invoke(this);
    }

    public void GetLoot()
    {

    }

    private void Awake()
    {
        this.name = ItemType.ToString() + " " + Guid.NewGuid().ToString();
        _propertyBlock ??= new MaterialPropertyBlock();
        _initialColor = _bodyRenderer.material.color;
    }

    private void DrawItemType(ItemType itemType)
    {
        Debug.Log(itemType);
        var category = _lootCategoriesRepository.Get().FirstOrDefault(c => c.ItemType == itemType);
        if (category == null)
        {
            Debug.LogError($"Unable to find Category for {itemType}");
            return;
        }

        _propertyBlock.SetTexture("_BaseMap", category.Icon);
        _initialColor = category.Color;
        _bodyRenderer.material.color = _initialColor;
        _iconRenderer.SetPropertyBlock(_propertyBlock);
    }

    public void DoSelection()
    {
        _bodyRenderer.material.color = _isSelected ? Color.yellow : _initialColor;
    }
}
