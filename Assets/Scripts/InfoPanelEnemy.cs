public sealed class InfoPanelEnemy : InfoPanelBase
{
    public override void Init(ISelectable selectable)
    {
        if (selectable is Enemy enemy)
        {
            enemy.Stats.ValueChanged += OnValueChanged;
            OnValueChanged(enemy.Stats);
        }
    }

    private void OnValueChanged(IStatsReadOnly stats)
    {
        //ToDo: UI
    }
}
