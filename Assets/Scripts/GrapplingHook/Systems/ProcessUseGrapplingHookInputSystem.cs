using System.Collections.Generic;
using Entitas;


public class ProcessUseGrapplingHookInputSystem : GameReactiveSystem
{
    private IGroup<GameEntity> _playerGroup;
    private IGroup<GameEntity> _reticleGroup;

    public ProcessUseGrapplingHookInputSystem(IContext<GameEntity> context) : base(context)
    {
        _playerGroup = context.GetGroup(GameMatcher.Player);
        _reticleGroup = context.GetGroup(GameMatcher.GrapplingHookReticle);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.UseGrapple && _context.isUseGrapplingHookInputAvailable && _reticleGroup != null && _reticleGroup.count > 0;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (_playerGroup.count > 0)
        {
            GameEntity playerEntity = _playerGroup.GetSingleEntity();
            playerEntity.isUseGrapplingHook = true;

            _context.isUseGrapplingHookInputAvailable = false;
        }
    }
}