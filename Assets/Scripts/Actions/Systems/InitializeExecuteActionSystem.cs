using System.Collections.Generic;
using Entitas.Extensions;
using Entitas.Scripts.Common.Systems;

namespace Entitas.Actions.Systems
{
    public class InitializeExecuteActionSystem : GameInitializeSystem
    {
        protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.ExecuteAction};
        protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.Battle};

        public InitializeExecuteActionSystem(GameContext context) : base(context)
        {
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