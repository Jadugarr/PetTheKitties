using System.Collections.Generic;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;

namespace Entitas.Level.Systems
{
    public class UnloadLevelSystem : GameReactiveSystem
    {
        private IGroup<GameEntity> _playerGroup;
        private IGroup<GameEntity> _kittyGroup;
        
        public UnloadLevelSystem(IContext<GameEntity> context) : base(context)
        {
            _playerGroup = context.GetGroup(GameMatcher.Player);
            _kittyGroup = context.GetGroup(GameMatcher.Kitty);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.LoadNextLevel, GroupEvent.Added));
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
            foreach (GameEntity entity in _kittyGroup.GetEntities())
            {
                if (entity.view != null && entity.view.View != null)
                {
                    entity.view.View.Unlink();
                    entity.view.View.DestroyGameObject();
                }

                entity.Destroy();
            }

            foreach (GameEntity entity in _playerGroup.GetEntities())
            {
                if (entity.view != null && entity.view.View != null)
                {
                    entity.view.View.Unlink();
                    entity.view.View.DestroyGameObject();
                }

                entity.Destroy();
            }

            if (_context.hasWinCondition)
            {
                _context.RemoveWinCondition();
            }

            if (_context.hasLoseCondition)
            {
                _context.RemoveLoseCondition();
            }

            if (_context.hasCameraConfiner)
            {
                _context.RemoveCameraConfiner();
            }
        }
    }
}