using System.Collections.Generic;
using UnityEngine;

namespace Entitas.World.Systems
{
    public class ProcessJumpInputSystem : GameReactiveSystem
    {
        private IGroup<GameEntity> _playerGroup;

        public ProcessJumpInputSystem(IContext<GameEntity> context) : base(context)
        {
            _playerGroup = _context.GetGroup(GameMatcher.Player);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.input.InputCommand == InputCommand.Jump && _context.isJumpInputAvailable;
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.World &&
                   _context.subState.CurrentSubState == SubState.WorldNavigation;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            GameEntity playerEntity = _playerGroup.GetSingleEntity();

            playerEntity?.ReplaceJumpCharacter(playerEntity.id.Id);
        }
    }
}