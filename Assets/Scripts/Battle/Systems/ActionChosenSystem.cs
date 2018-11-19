using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ActionChosenSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.ChooseAction};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.Battle};

    public ActionChosenSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.BattleAction, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.battleAction.ActionType != ActionType.None
            && entity.battleAction.ActionType != ActionType.ChooseAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.ChooseTarget);
    }
}