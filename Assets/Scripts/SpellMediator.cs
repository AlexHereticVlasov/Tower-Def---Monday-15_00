using UnityEngine;
using UnityEngine.Events;

namespace SpellSystem
{
    public class SpellMediator : MonoBehaviour, ISpellMediator
    {
        [SerializeField] private LayerMask _mask;
        [SerializeField] private Spell[] _spells;


        private int _index = -1;

        public event UnityAction Finished;
        public event UnityAction Started;

        public bool HasCast => _index >= 0 && _index < _spells.Length;

        public bool TryGetPointUnderCursor(out Vector3 point)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 100, _mask))
            {
                point = hit.point;
                return true;
            }

            point = Vector3.zero;
            return false;
        }

        public void Cast(Vector3 point)
        {
            Instantiate(_spells[_index].SpellEffect, point, Quaternion.identity);
            _index = -1;
            Finished?.Invoke();
        }

        public void Cancel()
        {
            _index = -1;
            Finished?.Invoke();
        }

        public void SetSpell(int index)
        {
            Started?.Invoke();
            _index = index;
        }
    }
}