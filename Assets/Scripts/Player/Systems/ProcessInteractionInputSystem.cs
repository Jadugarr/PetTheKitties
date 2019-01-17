using System.Collections.Generic;
using UnityEngine;

namespace Entitas.Kitty.Systems
{
    public class ProcessInteractionInputSystem : GameReactiveSystem, ICleanupSystem
    {
        public ProcessInteractionInputSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.input.InputCommand == InputCommand.Interact && _context.isInteractInputAvailable;
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.World &&
                   _context.subState.CurrentSubState == SubState.WorldNavigation;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            foreach (GameEntity inputEntity in entities)
            {
                if (_context.hasPlayerInteraction)
                {
                    GameEntity interactableEntity =
                        _context.GetEntityWithId(_context.playerInteraction.InteractableEntityId);
                    
                    _context.ReplaceInteractionTriggered(interactableEntity.id.Id);
                }
            }
        }

        public void Cleanup()
        {
            if (_context.hasInteractionTriggered)
            {
                _context.RemoveInteractionTriggered();
            }
        }
    }
}