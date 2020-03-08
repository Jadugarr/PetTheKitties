using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;
using Entitas.Common;

public class EnterFinalizeActionStateSystem : GameReactiveSystem
{
    public EnterFinalizeActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.FinalizeAction;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle &&
               _context.subState.CurrentSubState == SubState.FinalizeAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSystemMapping(GameSystemType.FinalizeAction))
        {
            CreateFinalizeActionSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSystemMapping(GameSystemType.FinalizeAction));
    }

    private void CreateFinalizeActionSystems()
    {
        Systems finalizeActionSystems = new Feature("finalizeActionSystems")
            .Add(new AddActionTimeSystem(_context))
            .Add(new ActionTimeAddedSystem(_context));
        
        GameSystemService.AddSystemMapping(GameSystemType.FinalizeAction, finalizeActionSystems);
    }
}

public class ExitFinalizeActionStateSystem : GameReactiveSystem
{
    public ExitFinalizeActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.PreviousSubState == SubState.FinalizeAction;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (GameSystemService.HasSystemMapping(GameSystemType.FinalizeAction))
        {
            GameSystemService.RemoveActiveSystems(GameSystemService.GetSystemMapping(GameSystemType.FinalizeAction));
        }
    }
}