using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CheckPlayerDeadConditionSystem : GameReactiveSystem
{
    private IGroup<GameEntity> playerEntities;

    public CheckPlayerDeadConditionSystem(IContext<GameEntity> context) : base(context)
    {
        playerEntities = context.GetGroup(GameMatcher.Player);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Player, GroupEvent.Removed));
    }

    protected override bool Filter(GameEntity entity)
    {
        return playerEntities.count == 0
               && _context.hasLoseCondition
               && !_context.isWinConditionsFulfilled && !_context.isLoseConditionsFulfilled;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        LoseConditionComponent loseConditions = _context.loseCondition;

        for (var i = 0; i < loseConditions.LoseConditions.Length; i++)
        {
            LoseConditionState currentLoseCondition = loseConditions.LoseConditions[i];
            if (currentLoseCondition.LoseCondition == LoseCondition.PlayerDead)
            {
                loseConditions.LoseConditions[i].IsFulfilled = true;
                break;
            }
        }

        _context.ReplaceLoseCondition(_context.loseCondition.ConditionModifier, loseConditions.LoseConditions);
    }
}