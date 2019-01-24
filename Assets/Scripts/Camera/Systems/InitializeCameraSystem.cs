using Entitas.Scripts.Common.Systems;

namespace Entitas.Camera.Systems
{
    public class InitializeCameraSystem : GameInitializeSystem
    {
        public InitializeCameraSystem(GameContext context) : base(context)
        {
        }

        protected override bool IsInValidState()
        {
            return true;
        }

        protected override void ExecuteSystem()
        {
            _context.CreateEntity().AddCamera(UnityEngine.Camera.main);
        }
    }
}