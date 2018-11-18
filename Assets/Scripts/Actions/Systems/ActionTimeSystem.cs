﻿using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Enums;
using Entitas.Scripts.Common.Systems;
using Entitas.Utils;
using UnityEngine;

public class ActionTimeSystem : GameExecuteSystem
{
    private IGroup<GameEntity> actionEntities;

    public ActionTimeSystem(GameContext context) : base(context)
    {
        actionEntities = _context.GetGroup(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecutionTime));
    }

    protected override bool IsInValidStates()
    {
        return _context.battleState.CurrentBattleState == BattleState.Waiting &&
               _context.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity actionEntity in actionEntities)
        {
            GameEntity performingCharacter = _context.GetEntityWithId(actionEntity.battleAction.EntityId);
            float newRemainingTime = actionEntity.executionTime.RemainingTime -
                                     Time.deltaTime * BattleActionUtils.GetActionTimeStep(
                                         actionEntity.battleAction.ActionType,
                                         performingCharacter);
            actionEntity.ReplaceExecutionTime(actionEntity.executionTime.TotalTime,
                newRemainingTime);
        }
    }
}