using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

public class UserInput : MonoBehaviour
{
    [Inject] private readonly ISelection _selection;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            _selection.GetSelectableUnderPointer();
        }

        if (Input.GetMouseButtonDown(1))
        {
            //
        }
    }
}

public interface ISelectable
{
    event UnityAction Selected;
    event UnityAction Deselected;
    event UnityAction ValuesChanged;

    void Select();
    void Deselect();
}