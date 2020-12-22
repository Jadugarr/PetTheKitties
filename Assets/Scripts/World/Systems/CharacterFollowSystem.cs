using Entitas.Scripts.Common.Systems;
using UnityEngine;

namespace Entitas.World.Systems
{
    public class CharacterFollowSystem : GameExecuteSystem
    {
        private const float MaxDistanceToCharacter = 2f;

        private IGroup<GameEntity> _followGroup;

        public CharacterFollowSystem(GameContext context) : base(context)
        {
            _followGroup = _context.GetGroup(GameMatcher.FollowCharacter);
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.World &&
                   _context.subState.CurrentSubState == SubState.WorldNavigation;
        }

        protected override void ExecuteSystem()
        {
            foreach (GameEntity followEntity in _followGroup.GetEntities())
            {
                GameEntity entityToFollow = _context.GetEntityWithId(followEntity.followCharacter.EntityToFollowId);
                if (entityToFollow != null)
                {
                    float currentDistance = Vector2.Distance(entityToFollow.position.position,
                        followEntity.position.position);

                    if (currentDistance > MaxDistanceToCharacter)
                    {
                        GameObject characterView = followEntity.view.View;

                        if (characterView)
                        {
                            Vector2 moveDirection = entityToFollow.position.position -
                                                    followEntity.position.position;

                            followEntity.ReplaceMoveCharacter(followEntity.id.Id, moveDirection);
                            followEntity.ReplaceCharacterDirection(moveDirection.x < 0
                                ? CharacterDirection.Backward
                                : CharacterDirection.Forward);

                            if (moveDirection.y > 0.5f || followEntity.characterState.State == CharacterState.Jumping)
                            {
                                followEntity.ReplaceJumpCharacter(followEntity.id.Id);
                            }
                        }
                    }

                    if (followEntity.characterState.State == CharacterState.Jumping)
                    {
                        followEntity.ReplaceJumpCharacter(followEntity.id.Id);
                    }
                }
            }
        }
    }
}