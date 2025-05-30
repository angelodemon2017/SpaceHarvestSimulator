using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UIPanelConfig : MonoBehaviour
{
    [SerializeField] private string _baseLabel;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private Slider _slider;

    public Action<int> changeSlide;

    private void Awake()
    {
        _slider.onValueChanged.AddListener(UpdateSlider);
    }

    private void UpdateSlider(float newValue)
    {
        changeSlide?.Invoke((int)newValue);
        UpdateLabel((int)newValue);
    }

    public void UpdateLabel(int count)
    {
        _label.text = $"{_baseLabel}{count}";
    }
}