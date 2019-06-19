using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ManageFallingStateHandlingSystem : GameReactiveSystem
{
    private static Systems _fallingStateSystems;
    
        private IGroup<GameEntity> _fallingEntities;
    
        public ManageFallingStateHandlingSystem(IContext<GameEntity> context) : base(context)
        {
            _fallingEntities = context.GetGroup(GameMatcher.CharacterState);
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
            foreach (GameEntity jumpingEntity in _fallingEntities)
            {
                if (jumpingEntity.hasCharacterState && jumpingEntity.characterState != null &&
                    jumpingEntity.characterState.State == CharacterState.Falling)
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