using System;
using UnityEngine;

public class GridItem : MonoBehaviour, IInteractive
{
    public delegate void OnInteractionHandler(IInteractive source);
    public event OnInteractionHandler OnInteraction;


    public Vector3Int GridPosition { get; set; }
    public ItemTypes ItemType;
    private bool _isSelected;
    private Renderer _renderer;

    public void Interact()
    {
        OnInteraction?.Invoke(this);
        _isSelected = !_isSelected;
        if (_isSelected)
        {
            _renderer.material.color = Color.white;

            return;
        }

        _renderer.material.color = ItemsTypeHelper.GetColorByItemType(ItemType);
    }

    private void Awake()
    {
        _renderer = this.GetComponent<Renderer>();
        this.name = ItemType.ToString() + " " + Guid.NewGuid().ToString();
        _renderer.material.color = ItemsTypeHelper.GetColorByItemType(ItemType);
    }
}
