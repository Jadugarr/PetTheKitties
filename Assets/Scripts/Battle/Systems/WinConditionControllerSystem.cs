using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;

public class WinConditionControllerSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};

    public WinConditionControllerSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.WinCondition);
    }

    protected override bool Filter(GameEntity entity)
    {
        bool everythingFulfilled = true;

        foreach (WinConditionState currentWinCondition in entity.winCondition.WinConditions)
        {
            if (currentWinCondition.IsFulfilled == false)
            {
                everythingFulfilled = false;
            }
        }

        return everythingFulfilled;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.PlayerWon);
    }
}