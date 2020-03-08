using System.Collections.Generic;
using Entitas;
using Entitas.Common;
using UnityEngine;

public class ExitExecuteActionStateSystem : GameReactiveSystem
{
    public ExitExecuteActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.ExecuteAction;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (GameSystemService.HasSystemMapping(GameSystemType.ExecuteAction))
        {
            GameSystemService.RemoveActiveSystems(GameSystemService.GetSystemMapping(GameSystemType.ExecuteAction));
        }
    }
}