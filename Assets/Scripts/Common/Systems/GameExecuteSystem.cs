using System.Collections.Generic;

namespace Entitas.Scripts.Common.Systems
{
    public abstract class GameExecuteSystem : IExecuteSystem
    {
        protected abstract IList<SubState> ValidSubStates { get; }
        protected abstract IList<GameState> ValidGameStates { get; }
        
        protected GameContext _context;

        public GameExecuteSystem(GameContext context)
        {
            _context = context;
        }

        public void Execute()
        {
            GameState currentGameState = _context.gameState.CurrentGameState;
            SubState currentSubState = _context.subState.CurrentSubState;

            if ((ValidGameStates.Contains(currentGameState) || ValidGameStates.Contains(GameState.Undefined)) 
                && (ValidSubStates.Contains(currentSubState) || ValidSubStates.Contains(SubState.Undefined)))
            {
                ExecuteSystem();
            }
        }

        protected abstract void ExecuteSystem();
    }
}