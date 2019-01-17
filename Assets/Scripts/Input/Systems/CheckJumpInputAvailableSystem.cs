using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CheckJumpInputAvailableSystem : GameReactiveSystem
{
    public CheckJumpInputAvailableSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            GameEntity currentInput = entities[i];

            _context.isJumpInputAvailable = currentInput.input.InputCommand != InputCommand.Jump;
        }
    }
}