using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public abstract class InfoPanelBase : MonoBehaviour
    {
        public abstract void Init(ISelectable selectable);
    }
}