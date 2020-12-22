using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class EndGrapplingSystem : GameReactiveSystem
{
    private IGroup<GameEntity> _reticleGroup;
    
    public EndGrapplingSystem(IContext<GameEntity> context) : base(context)
    {
        _reticleGroup = context.GetGroup(GameMatcher.GrapplingHookReticle);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.CharacterState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.characterState.State != CharacterState.Grappling;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (_reticleGroup.count > 0)
        {
            GameEntity reticleEntity = _reticleGroup.GetSingleEntity();
            reticleEntity.view.View.Unlink();
            GameObject.Destroy(reticleEntity.view.View);
            reticleEntity.Destroy();
        }
    }
}