using System;
using UnityEngine;

namespace UI
{
    public sealed class InfoPanelTowerNode : InfoPanelBase
    {
        public TowerNode Node { get; private set; }

        public override void Init(ISelectable selectable)
        {
            if (selectable is TowerNode towerNode)
            {
                Node = towerNode;
            }
        }
    }
}

public abstract class BuilingBase : MonoBehaviour
{ }
