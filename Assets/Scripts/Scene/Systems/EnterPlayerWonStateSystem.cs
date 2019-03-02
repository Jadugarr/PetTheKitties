using System.Collections.Generic;
using Entitas;

public class EnterPlayerWonStateSystem : GameReactiveSystem
{
    public EnterPlayerWonStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.PlayerWon;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        GameSystemService.AddActiveSystems(GameSystemService.GetSubSystemMapping(SubState.PlayerWon));
    }
}