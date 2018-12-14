namespace Entitas.Scripts.Common.Systems
{
    public abstract class GameInitializeSystem : IInitializeSystem
    {
        protected GameContext _context;

        public GameInitializeSystem(GameContext context)
        {
            _context = context;
        }

        public void Initialize()
        {
            if (IsInValidState())
            {
                ExecuteSystem();
            }
        }

        protected abstract bool IsInValidState();

        protected abstract void ExecuteSystem();
    }
}