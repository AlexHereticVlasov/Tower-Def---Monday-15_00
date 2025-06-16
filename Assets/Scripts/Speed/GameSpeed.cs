using UnityEngine;


namespace Speed
{
    public sealed class GameSpeed : MonoBehaviour, IGameSpeed
    {
        public float Value { get; private set; } = 1;

        public void SetValue(float newScale)
        {
            if (newScale < 0)
                throw new System.Exception("Time Scale can't be negative");

            Value = newScale;
            Time.timeScale = newScale;
        }
    }
}
