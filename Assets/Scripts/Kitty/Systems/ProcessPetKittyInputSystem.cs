using System.Collections.Generic;
using UnityEngine;

namespace Entitas.Kitty.Systems
{
    public class ProcessPetKittyInputSystem : GameReactiveSystem
    {
        public ProcessPetKittyInputSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
        }

        protected override bool Filter(GameEntity entity)
        {
            if (entity.input.InputCommand == InputCommand.Interact && _context.hasPlayerInteraction)
            {
                GameEntity interactableEntity =
                    _context.GetEntityWithId(_context.playerInteraction.InteractableEntityId);
                return interactableEntity != null && interactableEntity.isKitty;
            }
            
            return false;
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.World &&
                   _context.subState.CurrentSubState == SubState.WorldNavigation;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            Debug.Log("Kitty pet!");
        }
    }
}