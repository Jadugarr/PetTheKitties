using System;
using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Enums;
using Entitas.Extensions;
using UnityEngine;

public class ExecuteChooseActionSystem : GameReactiveSystem
{
    public ExecuteChooseActionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.ExecutionTime, GameMatcher.BattleAction));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.battleAction.ActionType == ActionType.ChooseAction && entity.executionTime.RemainingTime <= 0f;
    }

    protected override bool IsInValidStates()
    {
        return _context.battleState.CurrentBattleState == BattleState.Waiting &&
               _context.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewBattlestate(BattleState.ChooseAction);
    }
}