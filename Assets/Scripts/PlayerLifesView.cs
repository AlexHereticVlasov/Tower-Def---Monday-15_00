using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class PlayerLifesView : MonoBehaviour
    {
        [SerializeField] private PlayerLifes _lifes;
        [SerializeField] private TMP_Text _text;

        private void OnEnable() => _lifes.ValueChanged += OnValueChanged;

        private void OnDisable() => _lifes.ValueChanged -= OnValueChanged;

        private void OnValueChanged(int value) => _text.text = value.ToString();
    }
}
