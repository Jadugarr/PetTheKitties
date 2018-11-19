using System.Collections.Generic;
using Entitas;

public class RenderPositionSystem : GameReactiveSystem
{
    public RenderPositionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(Matcher<GameEntity>.AllOf(GameMatcher.Position, GameMatcher.View));
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
        foreach (GameEntity e in entities)
        {
            PositionComponent pos = e.position;
            e.view.View.transform.position = pos.position;
        }
    }
}