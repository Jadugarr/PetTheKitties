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
                            Vector3 extents = characterView.GetComponent<Collider2D>().bounds.extents;
                            Vector2 raycastStartPos;

                            if (moveDirection.x > 0f)
                            {
                                raycastStartPos = new Vector2(characterView.transform.position.x + extents.x,
                                    characterView.transform.position.y - extents.y - 0.01f);
                            }
                            else
                            {
                                raycastStartPos = new Vector2(characterView.transform.position.x - extents.x,
                                    characterView.transform.position.y - extents.y - 0.01f);
                            }

                            RaycastHit2D hit =
                                Physics2D.Raycast(raycastStartPos
                                    , Vector2.down,
                                    10f);

                            if (hit.collider != null && hit.collider.gameObject.tag.Equals(Tags.Ground))
                            {
                                followEntity.isScared = false;
                                _context.CreateEntity().AddMoveCharacter(followEntity.id.Id, moveDirection);
                            }
                            else
                            {
                                followEntity.isScared = true;
                            }

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