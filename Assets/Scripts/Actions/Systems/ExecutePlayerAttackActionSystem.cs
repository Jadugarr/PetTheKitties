﻿using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ExecutePlayerAttackActionSystem : GameReactiveSystem
{
    public ExecutePlayerAttackActionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecutionTime));
    }

    protected override bool Filter(GameEntity entity)
    {
        if (entity.hasBattleAction)
        {
            return entity.battleAction.ActionType == ActionType.AttackCharacter &&
                   entity.executionTime.RemainingTime < 0f;
        }

        return false;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle &&
               _context.subState.CurrentSubState == SubState.ExecuteAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            GameEntity attacker = _context.GetEntityWithId(gameEntity.battleAction.EntityId);
            GameEntity defender = _context.GetEntityWithId(gameEntity.target.TargetId);

            if (attacker != null && defender != null)
            {
                defender.ReplaceHealth(
                    defender.health.Health -
                    Math.Max(0,
                        attacker.attack.AttackValue -
                        defender.defenseStat.DefenseValue));

                gameEntity.isActionFinished = true;
            }
        }
    }
}