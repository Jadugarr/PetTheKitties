using Entitas.Utils;

namespace Entitas.Battle.Systems
{
    public class AddActionTimeSystem : IInitializeSystem
    {
        private GameContext context;

        public AddActionTimeSystem(GameContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            IGroup<GameEntity> actionEntities =
                context.GetGroup(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.Target));

            foreach (GameEntity actionEntity in actionEntities)
            {
                actionEntity.ReplaceBattleAction(actionEntity.battleAction.EntityId,
                    actionEntity.battleAction.ActionType, ActionATBType.Acting);
                float executionTime = BattleActionUtils.GetTimeForAction(actionEntity.battleAction.ActionType);
                actionEntity.ReplaceExecutionTime(executionTime, executionTime);
            }
        }
    }
}