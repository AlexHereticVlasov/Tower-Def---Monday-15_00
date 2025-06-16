using UnityEngine;
using UnityEngine.Events;

namespace SceneLoad
{
    public sealed class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private GameObject _loadingScreen = default;

        [SerializeField] private SceneLoader _sceneLoader;

        public event UnityAction<float> ValueChanged;

        private void OnEnable()
        {
            _sceneLoader.StartLoading += OnStartLoading;
            _sceneLoader.Loading += OnLoading;
        }

        private void OnDisable()
        {
            _sceneLoader.StartLoading -= OnStartLoading;
            _sceneLoader.Loading -= OnLoading;
        }

        private void OnLoading(float progress) => ValueChanged?.Invoke(progress);

        private void OnStartLoading() => _loadingScreen.SetActive(true);
    }
}