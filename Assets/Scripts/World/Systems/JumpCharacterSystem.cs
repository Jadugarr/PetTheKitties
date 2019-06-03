using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class JumpCharacterSystem : GameReactiveSystem
{
    public JumpCharacterSystem(IContext<GameEntity> context) : base(context)
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
            if (jumpingEntity != null)
            {
                GameObject characterView = jumpingEntity.view.View;

                if (characterView)
                {
                    float distanceToGround = characterView.GetComponent<Collider2D>().bounds.extents.y;
                    Vector2 rayStart = new Vector2(characterView.transform.position.x,
                        characterView.transform.position.y - distanceToGround - 0.01f);
                    Debug.DrawRay(rayStart, Vector2.down * 0.01f, Color.red, 3f);
                    RaycastHit2D hit = Physics2D.Raycast(rayStart, Vector2.down, 0.01f);

                    if (hit.collider != null)
                    {
                        Debug.Log("Tag of hit target: " + hit.collider.gameObject.tag);

                        if (hit.collider.gameObject.tag.Equals(Tags.Ground))
                        {
                            jumpingEntity.ReplaceCharacterVelocity(new Vector2(
                                jumpingEntity.hasCharacterVelocity ? jumpingEntity.characterVelocity.Velocity.x : 0f,
                                jumpingEntity.jumpForce.JumpForce));
                        }
                    }
                }
            }
        }
    }
}