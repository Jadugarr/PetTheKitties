using Entitas.Scripts.Common.Systems;

namespace Entitas.Camera.Systems
{
    public class InitializeCameraSystem : GameInitializeSystem, ITearDownSystem
    {
        private IGroup<GameEntity> _cameraGroup;

        public InitializeCameraSystem(GameContext context) : base(context)
        {
            _cameraGroup = context.GetGroup(GameMatcher.Camera);
        }

        protected override bool IsInValidState()
        {
            return true;
        }

        protected override void ExecuteSystem()
        {
            _context.CreateEntity().AddCamera(UnityEngine.Camera.main);
        }

        public void TearDown()
        {
            foreach (GameEntity entity in _cameraGroup.GetEntities())
            {
                entity.Destroy();
            }
        }
    }
}