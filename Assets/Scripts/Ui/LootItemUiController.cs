using Core.Items;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LootItemUiController : MonoBehaviour
{
    [SerializeField]
    private Image _image;
    [SerializeField]
    private TextMeshProUGUI _title;
    [SerializeField]
    private TextMeshProUGUI _description;
    [SerializeField]
    private Image _itemBackground;

    private Item _model;

    public Item Model
    {
        get => _model;
        set
        {
            _model = value;
            ApplyModel(_model);
        }
    }

    private void ApplyModel(Item model)
    {
        var color = RarityColors.GetColor(model.Rarity);
        _image.sprite = model.Image;
        _title.text = model.Name;
        _title.color = color;
        _description.text = model.Description;
        _itemBackground.color = color;
        //Rarity
        //Cost
    }
}
