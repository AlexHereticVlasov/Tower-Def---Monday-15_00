using System;
using UnityEngine;

public sealed class InfoPanelTowerNode : InfoPanelBase
{
    private TowerNode _node;

    public override void Init(ISelectable selectable)
    {
        if (selectable is TowerNode towerNode)
        {
            _node = towerNode;
        }
    }

    public void TryBuildTower(BuilingBase builing)
    {
        //ToDo: Validate
        if (CanBuild(_node, builing))
        {
            _node.Build(builing);
        }
    }

    private bool CanBuild(TowerNode node, BuilingBase builing)
    {
        if (builing is null)
            throw new Exception("Building is nit set");

        if (node.IsEmpty == false)
        {
            return false;
        }
        
        return true;
    }
}

public abstract class BuilingBase : MonoBehaviour
{ }
