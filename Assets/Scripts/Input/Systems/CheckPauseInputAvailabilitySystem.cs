using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CheckPauseInputAvailabilitySystem : GameReactiveSystem, IInitializeSystem
{
    public CheckPauseInputAvailabilitySystem(IContext<GameEntity> context) : base(context)
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

    public void Initialize()
    {
        _context.isPauseInputAvailable = true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        for (int i = 0; i < entities.Count; i++)
        {
            GameEntity currentInput = entities[i];

            if (_context.isPauseInputAvailable && (currentInput.input.InputCommand == InputCommand.Pause ||
                                                   currentInput.input.InputCommand == InputCommand.Unpause))
            {
                _context.isPauseInputAvailable = false;
            }
            else if (!_context.isPauseInputAvailable && (currentInput.input.InputCommand != InputCommand.Pause &&
                                                         currentInput.input.InputCommand != InputCommand.Unpause))
            {
                _context.isPauseInputAvailable = true;
            }
        }
    }
}