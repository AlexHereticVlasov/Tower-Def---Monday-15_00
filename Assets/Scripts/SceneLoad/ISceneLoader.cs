using UnityEngine.Events;

namespace SceneLoad
{
    public interface ISceneLoader
    {
        event UnityAction StartLoading;
        event UnityAction<float> Loading;

        void LoadScene(int buildIndex);
        void LoadNextScene();
        void Restart();
    }
}