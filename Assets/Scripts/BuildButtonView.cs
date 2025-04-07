using TMPro;
using UnityEngine;

namespace UI
{
    public sealed class BuildButtonView : MonoBehaviour
    {
        [SerializeField] private BuildButton _button;
        [SerializeField] private TMP_Text _text;

        private void Start()
        {
            _text.text = $"{_button.Data.Cost}";
        }
    }
}
