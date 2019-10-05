using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class RaycastTestSystem : GameReactiveSystem
{
    private IGroup<GameEntity> playerGroup;
    
    public RaycastTestSystem(IContext<GameEntity> context) : base(context)
    {
        playerGroup = context.GetGroup(GameMatcher.Player);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.RaycastTest);
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
        GroundCheckUtil.CheckIfCharacterOnGround(playerGroup.GetSingleEntity().view.View);
    }
}