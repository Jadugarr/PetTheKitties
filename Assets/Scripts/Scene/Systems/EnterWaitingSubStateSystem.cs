using System.Collections.Generic;
using Entitas;
using Entitas.Common;

public class EnterWaitingSubStateSystem : GameReactiveSystem
{
    public EnterWaitingSubStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.Waiting;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle &&
               _context.subState.CurrentSubState == SubState.Waiting;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSystemMapping(GameSystemType.Wait))
        {
            CreateWaitingSystems();
        }

        Systems waitSystems = GameSystemService.GetSystemMapping(GameSystemType.Wait);
        GameSystemService.AddActiveSystems(waitSystems);
    }

    private void CreateWaitingSystems()
    {
        Systems waitStateSystems = new Feature("WaitingSubStateSystems")
            .Add(new ActionTimeSystem(_context))
            //Actions
            .Add(new ExecuteChooseActionSystem(_context))
            .Add(new ExecuteActionsSystem(_context));
        
        GameSystemService.AddSystemMapping(GameSystemType.Wait, waitStateSystems);
    }
}