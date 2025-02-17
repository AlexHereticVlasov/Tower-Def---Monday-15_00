using UnityEngine;
using UnityEngine.Events;

public class Selection : MonoBehaviour
{
    [SerializeField] private LayerMask _mask;

    private ISelectable _selectable;

    public event UnityAction<ISelectable> SelectableChanged;

    public void GetSelectableUnderPointer()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100, _mask))
        {
            if (hit.transform.TryGetComponent(out ISelectable selectable))
            {

                _selectable?.Deselect();
                _selectable = selectable;
                _selectable.Select();
                SelectableChanged(_selectable);

            }
        }
    }
}
