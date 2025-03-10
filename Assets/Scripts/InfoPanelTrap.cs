namespace UI
{
    public sealed class InfoPanelTrap : InfoPanelBase
    {
        public override void Init(ISelectable selectable)
        {
            if (selectable is ITrapReadOnly trap)
            {
                //_damageText.text = trap.Damage;
            }
        }
    }
}