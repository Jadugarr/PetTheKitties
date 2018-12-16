using System.Collections.Generic;

namespace Entitas.Input.Systems
{
    public class CheckInteractInputAvailableSystem : GameReactiveSystem, IInitializeSystem
    {
        public CheckInteractInputAvailableSystem(IContext<GameEntity> context) : base(context)
        {
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override bool IsInValidState()
        {
            return true;
        }

        public void Initialize()
        {
            _context.isInteractInputAvailable = true;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            for (var i = 0; i < entities.Count; i++)
            {
                GameEntity gameEntity = entities[i];

                _context.isInteractInputAvailable = gameEntity.input.InputCommand != InputCommand.Interact;
            }
        }
    }
}