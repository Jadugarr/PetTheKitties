using System.Collections.Generic;

namespace Entitas.Scripts.Common.Systems
{
    public abstract class GameExecuteSystem : IExecuteSystem
    {
        protected GameContext _context;

        public GameExecuteSystem(GameContext context)
        {
            _context = context;
        }

        public void Execute()
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