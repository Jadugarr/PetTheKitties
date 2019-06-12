using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ManageFallingStateHandlingSystem : GameReactiveSystem
{
    private static Systems _fallingStateSystems;
    
        private IGroup<GameEntity> _fallingEntities;
    
        public ManageFallingStateHandlingSystem(IContext<GameEntity> context) : base(context)
        {
            _fallingEntities = context.GetGroup(GameMatcher.JumpState);
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
            foreach (GameEntity jumpingEntity in _fallingEntities)
            {
                if (jumpingEntity.hasJumpState && jumpingEntity.jumpState != null &&
                    jumpingEntity.jumpState.JumpState == JumpState.Falling)
                {
                    CreateFallingStateSystems();
                    GameSystemService.AddActiveSystems(_fallingStateSystems);
                    return;
                }
            }

            if (_fallingStateSystems != null)
            {
                GameSystemService.RemoveActiveSystems(_fallingStateSystems);
            }
        }
    
        private void CreateFallingStateSystems()
        {
            if (_fallingStateSystems == null)
            {
                _fallingStateSystems = new Feature("FallingStateSystems")
                    .Add(new HandleGroundedJumpStateSystem(_context));
            }
        }
}