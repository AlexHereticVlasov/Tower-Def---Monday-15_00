using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneLoad
{
    [RequireComponent(typeof(LoadingScreenPresenter))]
    public class LoadingSceneView : MonoBehaviour
    {
        [SerializeField] private Slider _loadBar = default;
        [SerializeField] private TMP_Text _progressText = default;
        [SerializeField] private LoadingScreenPresenter _presenter;

        private void OnEnable() => _presenter.ValueChanged += OnValueChanged;

        private void OnDisable() => _presenter.ValueChanged -= OnValueChanged;

        private void OnValueChanged(float fillAmount, string text)
        {
            _loadBar.value = fillAmount;
            _progressText.text = text;
        }
    }
}