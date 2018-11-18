using System.Collections.Generic;

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
            GameState currentGameState = _context.gameState.CurrentGameState;
            SubState currentSubState = _context.subState.CurrentSubState;

            if (IsInValidStates())
            {
                ExecuteSystem();
            }
        }

        protected abstract bool IsInValidStates();
        protected abstract void ExecuteSystem();
    }
}