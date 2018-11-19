using System.Collections.Generic;
using Entitas.Extensions;
using Entitas.Scripts.Common.Systems;

namespace Entitas.Actions.Systems
{
    public class InitializeExecuteActionSystem : GameInitializeSystem
    {
        public InitializeExecuteActionSystem(GameContext context) : base(context)
        {
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.Battle &&
                   _context.subState.CurrentSubState == SubState.ExecuteAction;
        }

        protected override void ExecuteSystem()
        {
            GameEntity[] potentialEntities = _context.GetEntities(GameMatcher.ExecutionTime);

            bool entityFound = false;

            foreach (GameEntity entity in potentialEntities)
            {
                if (entity.executionTime.RemainingTime <= 0f)
                {
                    entityFound = true;
                    entity.ReplaceExecutionTime(entity.executionTime.TotalTime, entity.executionTime.RemainingTime);
                    break;
                }
            }

            if (!entityFound)
            {
                _context.SetNewSubstate(SubState.Waiting);
            }
        }
    }
}