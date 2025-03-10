using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private StorageBase _storage;
    [SerializeField] private TMP_Text _text;

    private void OnEnable() => _storage.AmountChanged += OnAmountChanged;

    private void OnDisable() => _storage.AmountChanged -= OnAmountChanged;

    private void OnAmountChanged(int amount) => _text.text = amount.ToString();
}
