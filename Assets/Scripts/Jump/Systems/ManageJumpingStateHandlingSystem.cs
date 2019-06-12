using System.Collections.Generic;
using Entitas;

public class ManageJumpingStateHandlingSystem : GameReactiveSystem
{
    private static Systems _jumpingStateSystems;

    private IGroup<GameEntity> _jumpingEntities;

    public ManageJumpingStateHandlingSystem(IContext<GameEntity> context) : base(context)
    {
        _jumpingEntities = context.GetGroup(GameMatcher.JumpState);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.JumpState,
            GroupEvent.AddedOrRemoved));
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
        foreach (GameEntity jumpingEntity in _jumpingEntities)
        {
            if (jumpingEntity.hasJumpState && jumpingEntity.jumpState != null &&
                (jumpingEntity.jumpState.JumpState == JumpState.Jumping ||
                 jumpingEntity.jumpState.JumpState == JumpState.JumpEnding))
            {
                CreateJumpingStateSystems();
                GameSystemService.AddActiveSystems(_jumpingStateSystems);
                return;
            }
        }

        if (_jumpingStateSystems != null)
        {
            GameSystemService.RemoveActiveSystems(_jumpingStateSystems);
        }
    }

    private void CreateJumpingStateSystems()
    {
        if (_jumpingStateSystems == null)
        {
            _jumpingStateSystems = new Feature("JumpingStateSystems")
                .Add(new HandleJumpEndingStateSystem(_context))
                .Add(new AdjustEndingJumpVelocitySystem(_context))
                .Add(new HandleFallingJumpStateSystem(_context));
        }
    }
}