using UnityEngine;

public class SelectionMediator : MonoBehaviour
{
    [SerializeField] private Selection _selection;

    [SerializeField] private InfoPanelBase[] _panels;  

    private void OnEnable()
    {
        _selection.SelectableChanged += OnSelectableChanged;
    }

    private void OnDisable()
    {
        _selection.SelectableChanged -= OnSelectableChanged;
    }

    private void OnSelectableChanged(ISelectable selectable)
    {
        if (selectable is Enemy enemy)
        {
            for (int i = 0; i < _panels.Length; i++)
            {
                _panels[i].gameObject.SetActive(i == 0);
                _panels[0].Init(enemy);
                enemy.Died += OnDied;
                enemy.ReachedTarget += OnReachedTarget;
            }
        }
        else if (selectable is TrapBase trap)
        {
            for (int i = 0; i < _panels.Length; i++)
            {
                _panels[i].gameObject.SetActive(i == 1);
                _panels[1].Init(trap);
            }
        }
    }

    private void OnReachedTarget(Enemy enemy)
    {
        enemy.ReachedTarget -= OnReachedTarget;
        enemy.Died -= OnDied;
    }

    private void OnDied(Enemy enemy)
    {
        enemy.ReachedTarget -= OnReachedTarget;
        enemy.Died -= OnDied;

    }
}
