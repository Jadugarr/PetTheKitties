using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CheckKillEnemiesConditionSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};
    
    private IGroup<GameEntity> enemyEntities;

    public CheckKillEnemiesConditionSystem(GameContext context) : base(context)
    {
        enemyEntities = context.GetGroup(GameMatcher.Enemy);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Enemy, GroupEvent.Removed));
    }

    protected override bool Filter(GameEntity entity)
    {
        return enemyEntities.count == 0;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        Debug.Log("All enemies are dead!");
        WinConditionComponent winConditions = _context.winCondition;

        for (var i = 0; i < winConditions.WinConditions.Length; i++)
        {
            WinConditionState currentWinCondition = winConditions.WinConditions[i];
            if (currentWinCondition.WinCondition == WinCondition.KillEnemies)
            {
                winConditions.WinConditions[i].IsFulfilled = true;
                break;
            }
        }

        _context.ReplaceWinCondition(_context.winCondition.ConditionModifier, winConditions.WinConditions);
    }
}