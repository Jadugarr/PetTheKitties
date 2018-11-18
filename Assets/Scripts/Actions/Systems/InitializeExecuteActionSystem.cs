using System.Collections.Generic;
using Entitas.Battle.Enums;
using Entitas.Extensions;
using Entitas.Scripts.Common.Systems;

namespace Entitas.Actions.Systems
{
    public class InitializeExecuteActionSystem : GameInitializeSystem
    {
        public InitializeExecuteActionSystem(GameContext context) : base(context)
        {
        }

        protected override bool IsInValidStates()
        {
            return _context.battleState.CurrentBattleState == BattleState.ExecuteAction &&
                   _context.gameState.CurrentGameState == GameState.Battle;
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
                _context.SetNewBattlestate(BattleState.Waiting);
            }
        }
    }
}