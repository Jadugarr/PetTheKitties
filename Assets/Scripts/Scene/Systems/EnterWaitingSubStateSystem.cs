using System.Collections.Generic;
using Entitas;

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
        if (!GameSystemService.HasSubSystemMapping(SubState.Waiting))
        {
            CreateWaitingSystems();
        }

        Systems waitSystems = GameSystemService.GetSubSystemMapping(SubState.Waiting);
        GameSystemService.AddActiveSystems(waitSystems);
    }

    private void CreateWaitingSystems()
    {
    }
}