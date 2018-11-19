using System.Collections.Generic;

namespace Entitas.Scripts.Common.Systems
{
    public abstract class GameInitializeSystem : IInitializeSystem
    {
        protected abstract IList<SubState> ValidSubStates { get; }
        protected abstract IList<GameState> ValidGameStates { get; }
        
        protected GameContext _context;

        public GameInitializeSystem(GameContext context)
        {
            _context = context;
        }

        public void Initialize()
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