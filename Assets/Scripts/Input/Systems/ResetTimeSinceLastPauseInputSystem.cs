using System.Collections.Generic;

namespace Entitas.Input.Systems
{
    public class ResetTimeSinceLastPauseInputSystem : GameReactiveSystem
    {
        public ResetTimeSinceLastPauseInputSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.input.InputCommand == InputCommand.Pause ||
                entity.input.InputCommand == InputCommand.Unpause;
        }

        protected override bool IsInValidState()
        {
            return true;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            _context.ReplaceTimeSinceLastPauseInput(0f);
        }
    }
}