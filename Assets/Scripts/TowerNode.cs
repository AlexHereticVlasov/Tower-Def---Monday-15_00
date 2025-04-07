using UnityEngine;
using UnityEngine.Events;

public class TowerNode : MonoBehaviour, ISelectable
{
    private BuilingBase _builing;

    public bool IsEmpty => _builing == null;

    public event UnityAction Selected;
    public event UnityAction Deselected;
    public event UnityAction ValuesChanged;

    public void Deselect()
    {
        Deselected?.Invoke();
    }

    public void Select()
    {
        Selected?.Invoke();
    }

    public void Build(BuildingData data)
    {
        _builing = Instantiate(data.Template, transform.position, Quaternion.identity, transform);
    }
}
