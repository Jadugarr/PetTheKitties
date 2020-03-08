using System.Collections.Generic;
using Entitas;
using Entitas.Common;
using UnityEngine;

public class EnterWorldNavigationSubStateSystem : GameReactiveSystem
{
    public EnterWorldNavigationSubStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World &&
               _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSystemMapping(GameSystemType.WorldNavigation))
        {
            CreateWorldNavigationSystems();
        }
        
        GameSystemService.AddActiveSystems(GameSystemService.GetSystemMapping(GameSystemType.WorldNavigation));
    }

    private void CreateWorldNavigationSystems()
    {
        Systems worldMovementSystems = new Feature("WorldMovementSystems")
            // Some test systems
            .Add(new ProcessRaycastTestInputSystem(_context))
            .Add(new RaycastTestSystem(_context));
        
        GameSystemService.AddSystemMapping(GameSystemType.WorldNavigation, worldMovementSystems);
    }
}