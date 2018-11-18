using Entitas;

public class ActionChooserProperties : IWidgetProperties
{
    public GameEntity BattleActionEntity;
    public BattleActionChoice[] ActionChoices;
    public GameContext Context;

    public ActionChooserProperties(GameEntity battleActionEntity, BattleActionChoice[] actionChoices, GameContext context)
    {
        BattleActionEntity = battleActionEntity;
        ActionChoices = actionChoices;
        Context = context;
    }
}