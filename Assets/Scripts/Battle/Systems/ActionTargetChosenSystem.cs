using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Enums;
using Entitas.Extensions;
using UnityEngine;

public class ActionTargetChosenSystem : GameReactiveSystem
{
    public ActionTargetChosenSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Target, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidStates()
    {
        return _context.battleState.CurrentBattleState == BattleState.ChooseAction &&
               _context.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.SetNewBattlestate(BattleState.FinalizeAction);
    }
}