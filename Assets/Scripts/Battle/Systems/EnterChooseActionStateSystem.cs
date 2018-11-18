using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Enums;
using Entitas.Battle.Systems;
using UnityEngine;

public class EnterChooseActionStateSystem : GameReactiveSystem
{
    public EnterChooseActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
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
        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(BattleState.ChooseAction));
    }
}