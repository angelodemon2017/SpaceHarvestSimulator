using System;
using TMPro;
using UnityEngine;

public class UIPanelConfigInput : MonoBehaviour
{
    [SerializeField] private string _baseLabel;
    [SerializeField] private TextMeshProUGUI _label;
    [SerializeField] private TMP_InputField _input;

    public Action<int> changeValue;

    private void Awake()
    {
        _input.onValueChanged.AddListener(OnChangeInput);
    }

    private void OnChangeInput(string newValue)
    {
        if (int.TryParse(newValue, out int result))
        {
            changeValue?.Invoke(result);
            UpdateLabel(result);
        }
    }

    public void UpdateLabel(int count)
    {
        _label.text = $"{_baseLabel}{count}";
    }
}