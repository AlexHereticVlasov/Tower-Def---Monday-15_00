using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace UI
{
    public sealed class InfoPanelEnemy : InfoPanelBase
    {
        [SerializeField] private TMP_Text _nameText;
        [SerializeField] private Image _image;
        [SerializeField] private Slider _healthBar;
        [SerializeField] private TMP_Text _healthText;

        public override void Init(ISelectable selectable)
        {
            if (selectable is Enemy enemy)
            {
                enemy.Stats.ValueChanged += OnValueChanged;

                //TODO: Get Enemy Data
                //_nameText.text = ;
                //_image

                OnValueChanged(enemy.Stats);
            }
        }

        private void OnValueChanged(IStatsReadOnly stats)
        {
            //ToDo: UI
            _healthBar.value = stats.GetNormilizeHealth();
            _healthText.text = $"{stats.GetHealth():F2} / {stats.GetMaxHealth():F2}";
        }
    }
}