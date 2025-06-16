using UnityEngine;
using UnityEngine.Events;

namespace SceneLoad
{
    [RequireComponent(typeof(LoadingScreen))]
    public class LoadingScreenPresenter : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _screen;

        public event UnityAction<float, string> ValueChanged;

        private void OnEnable() => _screen.ValueChanged += OnValueChanged;

        private void OnDisable() => _screen.ValueChanged -= OnValueChanged;

        private void OnValueChanged(float value)
        {
            float fillAmount = Mathf.Clamp01(value / 0.9f);
            string text = $"{ Mathf.RoundToInt(fillAmount * 100)}%";

            ValueChanged?.Invoke(fillAmount, text);
        }
    }
}