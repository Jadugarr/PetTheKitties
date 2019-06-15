using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ManageJumpingStateHandlingSystem : GameReactiveSystem
{
    private static Systems jumpingStateSystems;
    private bool areSystemsActive = false;

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
                if (!areSystemsActive)
                {
                    CreateJumpingStateSystems();
                    Debug.Log("Add");
                    GameSystemService.AddActiveSystems(jumpingStateSystems);
                    areSystemsActive = true;
                }
                return;
            }
        }

        if (jumpingStateSystems != null && areSystemsActive)
        {
            Debug.Log("Remove");
            GameSystemService.RemoveActiveSystems(jumpingStateSystems);
            areSystemsActive = false;
        }
    }

    private void CreateJumpingStateSystems()
    {
        if (jumpingStateSystems == null)
        {
            jumpingStateSystems = new Feature("JumpingStateSystems")
                .Add(new HandleJumpEndingStateSystem(_context))
                .Add(new AdjustEndingJumpVelocitySystem(_context))
                .Add(new HandleFallingJumpStateSystem(_context));
        }
    }
}