using System.Collections.Generic;
using UnityEngine;

namespace Entitas.World.Systems
{
    public class ProcessJumpInputSystem : GameReactiveSystem, ICleanupSystem
    {
        private IGroup<GameEntity> _playerGroup;
        private IGroup<GameEntity> _jumpGroup;

        public ProcessJumpInputSystem(IContext<GameEntity> context) : base(context)
        {
            _playerGroup = _context.GetGroup(GameMatcher.Player);
            _jumpGroup = _context.GetGroup(GameMatcher.JumpCharacter);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.input.InputCommand == InputCommand.Jump;// && _context.isJumpInputAvailable;
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.World &&
                   _context.subState.CurrentSubState == SubState.WorldNavigation;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            GameEntity playerEntity = _playerGroup.GetSingleEntity();

            if (playerEntity != null)
            {
                _context.CreateEntity().AddJumpCharacter(playerEntity.id.Id);
            }
        }

        public void Cleanup()
        {
            foreach (GameEntity entity in _jumpGroup.GetEntities())
            {
                entity.Destroy();
            }
        }
    }
}