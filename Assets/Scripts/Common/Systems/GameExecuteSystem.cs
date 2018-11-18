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