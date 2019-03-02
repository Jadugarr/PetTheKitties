using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class LoseConditionControllerSystem : GameReactiveSystem
{
    public LoseConditionControllerSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.LoseCondition);
    }

    protected override bool Filter(GameEntity entity)
    {
        bool everythingFulfilled = true;

        foreach (LoseConditionState currentLoseCondition in entity.loseCondition.LoseConditions)
        {
            if (currentLoseCondition.IsFulfilled == false)
            {
                everythingFulfilled = false;
            }
        }

        return everythingFulfilled;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewSubstate(SubState.PlayerLost);
    }
}