using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class StartJumpCharacterSystem : GameReactiveSystem
{
    public StartJumpCharacterSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.JumpCharacter, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World &&
               _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity jumpCharacterEntity in entities)
        {
            GameEntity jumpingEntity = _context.GetEntityWithId(jumpCharacterEntity.jumpCharacter.JumpEntityId);
            if (jumpingEntity != null && jumpingEntity.hasJumpState && jumpingEntity.jumpState.JumpState == JumpState.Grounded)
            {
                GameObject characterView = jumpingEntity.view.View;

                if (characterView)
                {
                    RaycastHit2D hit = GroundCheckUtil.CheckIfCharacterOnGround(characterView);

                    if (hit.collider != null)
                    {
                        Debug.Log("Tag of hit target: " + hit.collider.gameObject.tag);

                        if (hit.collider.gameObject.tag.Equals(Tags.Ground))
                        {
                            jumpingEntity.ReplaceCharacterVelocity(new Vector2(
                                jumpingEntity.hasCharacterVelocity ? jumpingEntity.characterVelocity.Velocity.x : 0f,
                                jumpingEntity.jumpForce.JumpForce));
                            jumpingEntity.ReplaceJumpState(JumpState.Jumping);
                        }
                    }
                }
            }
        }
    }
}