using Entitas.Scripts.Common.Systems;
using UnityEngine;

namespace Entitas.Animations.Systems
{
    public class SyncMovementAnimationSystem : GameExecuteSystem
    {
        private IGroup<GameEntity> relevantEntities;

        public SyncMovementAnimationSystem(GameContext context) : base(context)
        {
            relevantEntities =
                _context.GetGroup(GameMatcher.AllOf(GameMatcher.CharacterVelocity, GameMatcher.CharacterAnimator));
        }

        protected override bool IsInValidState()
        {
            return true;
        }

        protected override void ExecuteSystem()
        {
            var entities = relevantEntities.GetEntities();
            for (var i = 0; i < entities.Length; i++)
            {
                GameEntity gameEntity = entities[i];
                gameEntity.characterAnimator.Animator.SetFloat(AnimationTriggerConstants.VelocityX,
                    Mathf.Abs(gameEntity.characterVelocity.Velocity.x));
            }
        }
    }
}