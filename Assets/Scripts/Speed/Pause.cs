using UnityEngine;


namespace Speed
{
    public sealed class Pause : MonoBehaviour, IPause
    {
        [SerializeField] private GameSpeed _speed;

        private void Start() => Time.timeScale = 1;

        public void SetOnPause() => Time.timeScale = Time.timeScale == 0 ? _speed.Value : 0;
    }
}
