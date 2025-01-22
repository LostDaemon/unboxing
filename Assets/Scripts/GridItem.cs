using System;
using TMPro;
using UnityEngine;

public class GridItem : MonoBehaviour, IInteractive
{
    public delegate void OnInteractionHandler(IInteractive source);
    public event OnInteractionHandler OnInteraction;
    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            //_renderer.material.color = _isSelected ? Color.white : ItemsTypeHelper.GetColorByItemType(ItemType);
            _renderer.material.color = _isSelected ? Color.yellow : Color.white;
        }
    }

    public Vector3Int GridPosition { get; set; }
    public ItemTypes ItemType;
    private bool _isSelected;
    private Renderer _renderer;

    public void Interact()
    {
        OnInteraction?.Invoke(this);
    }

    private void Awake()
    {
        _renderer = this.GetComponent<Renderer>();
        this.name = ItemType.ToString() + " " + Guid.NewGuid().ToString();
        this.GetComponentInChildren<TextMeshPro>().text = ((int)ItemType).ToString();
        //_renderer.material.color = ItemsTypeHelper.GetColorByItemType(ItemType);
    }
}
