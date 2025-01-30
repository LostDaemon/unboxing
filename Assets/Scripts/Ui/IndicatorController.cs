using System;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class IndicatorController : MonoBehaviour
{
    [SerializeField]
    private Sprite _image;
    [SerializeField]
    private float _value = 0;

    [SerializeField]
    private string _formatter = "{0}";

    private TextMeshProUGUI _label;
    private float _nextTick = 0;
    private float _displayValue = 0;
    private float progress = 0f;

    private float _fontSize = 0f;
    private Color _fontColor = Color.black;

    public float Value
    {
        get => _value;
        set
        {
            _value = value;
            progress = 0;
        }
    }

    void Start()
    {
        _label = this.GetComponentInChildren<TextMeshProUGUI>();
        if (_label == null)
        {
            Debug.LogError($"Unable to find {nameof(_label)}");
        }

        _fontSize = _label.fontSize;
        _fontColor = _label.color;
    }

    const float TickInterval = 0.025f;
    const float lerpSpeed = 0.1f;
    // Update is called once per frame
    void LateUpdate()
    {
        if (_displayValue != _value)
        {
            ShowChanging(true);
            if (Time.time > _nextTick)
            {
                _nextTick = Time.time + TickInterval;
                progress += lerpSpeed;
                ShowAnimation(progress);
            }
        }
        else
        {
            ShowChanging(false);
        }
    }

    private void ShowChanging(bool isChanging)
    {
        return;
        // _label.fontSize = isChanging ? _fontSize * 1.1f : _fontSize;
        // _label.color = isChanging ? Color.white : _fontColor;
    }

    private void ShowAnimation(float progress)
    {
        _displayValue = (int)Mathf.Lerp(_displayValue, _value, progress);
        _label.text = string.Format(_formatter, _displayValue);
    }
}
