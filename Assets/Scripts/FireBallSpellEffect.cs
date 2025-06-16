namespace SpellSystem
{
    public class FireBallSpellEffect : SpellEffectBase
    {
        private void Start()
        {
            Destroy(gameObject, 5);

            //TODo: Damage Logic
        }

        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            
        }

        private void OnTriggerExit(UnityEngine.Collider other)
        {
            
        }
    }
}