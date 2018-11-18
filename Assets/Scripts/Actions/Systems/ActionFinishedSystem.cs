using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class ActionFinishedSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.ExecuteAction};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.Battle};

    public ActionFinishedSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.ActionFinished);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity actionEntity in entities)
        {
            actionEntity.ReplaceBattleAction(actionEntity.battleAction.EntityId, ActionType.ChooseAction,
                ActionATBType.Waiting);
            actionEntity.ReplaceExecutionTime(10f, 10f);
            actionEntity.RemoveTarget();
            actionEntity.isActionFinished = false;

            _context.SetNewSubstate(SubState.Waiting);
        }
    }
}