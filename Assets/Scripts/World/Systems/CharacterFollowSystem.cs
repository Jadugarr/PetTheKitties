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
                    float currentDistance = Vector2.Distance(entityToFollow.view.View.transform.position,
                        followEntity.view.View.transform.position);

                    if (currentDistance > MaxDistanceToCharacter)
                    {
                        GameObject characterView = followEntity.view.View;

                        if (characterView)
                        {
                            Vector2 moveDirection = entityToFollow.view.View.transform.position -
                                                    followEntity.view.View.transform.position;

                            _context.CreateEntity().AddMoveCharacter(followEntity.id.Id, moveDirection);
                            followEntity.ReplaceCharacterDirection(moveDirection.x < 0
                                ? CharacterDirection.Backward
                                : CharacterDirection.Forward);

                            if (moveDirection.y > 0.5f || followEntity.characterState.State == CharacterState.Jumping)
                            {
                                _context.CreateEntity().AddJumpCharacter(followEntity.id.Id);
                            }
                        }
                    }

                    if (followEntity.characterState.State == CharacterState.Jumping)
                    {
                        _context.CreateEntity().AddJumpCharacter(followEntity.id.Id);
                    }
                }
            }
        }
    }
}