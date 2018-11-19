using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;

public class EnterChooseTargetStateSystem : GameReactiveSystem
{
    public EnterChooseTargetStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ChooseTarget;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle &&
               _context.subState.CurrentSubState == SubState.ChooseTarget;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.ChooseTarget))
        {
            CreateChooseTargetSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.ChooseTarget));
    }

    private void CreateChooseTargetSystems()
    {
    }
}