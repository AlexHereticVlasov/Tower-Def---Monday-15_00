using UnityEngine.Events;

public sealed class BuilingTower : BuilingBase, ISelectable
{
    public event UnityAction Selected;
    public event UnityAction Deselected;
    public event UnityAction ValuesChanged;

    public void Deselect()
    {
        //throw new System.NotImplementedException();
    }

    public void Select()
    {
        //throw new System.NotImplementedException();
    }
}

public interface ITower
{
    void Upgrade();

    void Demolish();


}
