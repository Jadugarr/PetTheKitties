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
        _jumpingEntities = context.GetGroup(GameMatcher.CharacterState);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CharacterState,
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
            if (jumpingEntity.hasCharacterState && jumpingEntity.characterState != null &&
                (jumpingEntity.characterState.State == CharacterState.Jumping ||
                 jumpingEntity.characterState.State == CharacterState.JumpEnding))
            {
                if (!areSystemsActive)
                {
                    CreateJumpingStateSystems();
                    GameSystemService.AddActiveSystems(jumpingStateSystems, SystemsUpdateType.FixedUpdate);
                    areSystemsActive = true;
                }
                return;
            }
        }

        if (jumpingStateSystems != null && areSystemsActive)
        {
            GameSystemService.RemoveActiveSystems(jumpingStateSystems, SystemsUpdateType.FixedUpdate);
            areSystemsActive = false;
        }
    }

    private void CreateJumpingStateSystems()
    {
        if (jumpingStateSystems == null)
        {
            jumpingStateSystems = new Feature("JumpingStateSystems")
                .Add(new HandleJumpEndingStateSystem(_context))
                .Add(new AdjustEndingJumpVelocitySystem(_context));
        }
    }
}