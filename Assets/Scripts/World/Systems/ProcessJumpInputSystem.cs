using System.Collections.Generic;

namespace Entitas.World.Systems
{
    public class ProcessJumpInputSystem : GameReactiveSystem
    {
        public ProcessJumpInputSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.input.InputCommand == InputCommand.Jump;
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.World &&
                   _context.subState.CurrentSubState == SubState.WorldNavigation;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            throw new System.NotImplementedException();
        }
    }
}