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
    private LootScriptableObject _model;

    public LootScriptableObject Model
    {
        get => _model;
        set
        {
            _model = value;
            ApplyModel(_model);
        }
    }

    private void ApplyModel(LootScriptableObject model)
    {
        _image.sprite = model.Image;
        _title.text = model.Name;
        _description.text = model.Description;
    }
}
