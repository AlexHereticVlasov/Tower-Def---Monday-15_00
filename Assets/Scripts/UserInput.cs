using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class UserInput : MonoBehaviour
{
    [SerializeField] private Selection _selection;

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