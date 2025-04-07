using UnityEngine;

namespace UI
{
    public sealed class BuildButton : MonoBehaviour
    {
        [SerializeField] private InfoPanelTowerNode _infoPanel;
        [SerializeField] private BuildingBuilder _builder;
        [SerializeField] private BuildingData _data;

        public BuildingData Data => _data;

        public void TryBuildTower() => _builder.TryBuildTower(_data, _infoPanel.Node);
    }
}
