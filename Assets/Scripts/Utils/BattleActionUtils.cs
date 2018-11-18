using System.Collections.Generic;
using Entitas.Actions;

namespace Entitas.Utils
{
    public static class BattleActionUtils
    {
        // TODO: Create a system where I can define things like this. Scriptable Objects?
        private static Dictionary<ActionType, TargetType> actionTargetsDefinition =
            new Dictionary<ActionType, TargetType>
            {
                {ActionType.None, TargetType.None},
                {ActionType.Defend, TargetType.Self},
                {ActionType.AttackCharacter, TargetType.Enemies | TargetType.Allies}
            };

        private static Dictionary<ActionType, float> actionTimesDefinition = new Dictionary<ActionType, float>
        {
            {ActionType.Defend, 0.1f},
            {ActionType.AttackCharacter, 5f},
            {ActionType.None, 0f},
            {ActionType.ChooseAction, 10f}
        };

        public static int[] GetTargetEntitiesByActionType(ActionType actionType, GameEntity choosingEntity,
            GameContext context)
        {
            List<int> targetEntityIds = new List<int>();
            TargetType actionTargetType;

            if (actionTargetsDefinition.TryGetValue(actionType, out actionTargetType))
            {
                if (actionTargetType.HasFlag(TargetType.Self))
                {
                    targetEntityIds.Add(choosingEntity.id.Id);
                }

                if (actionTargetType.HasFlag(TargetType.Allies))
                {
                    IGroup<GameEntity> playerEntities =
                        context.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Battle));

                    foreach (GameEntity gameEntity in playerEntities.GetEntities())
                    {
                        if (gameEntity.id.Id != choosingEntity.id.Id)
                        {
                            targetEntityIds.Add(gameEntity.id.Id);
                        }
                    }
                }

                if (actionTargetType.HasFlag(TargetType.Enemies))
                {
                    IGroup<GameEntity> enemyEntities =
                        context.GetGroup(GameMatcher.AllOf(GameMatcher.Enemy, GameMatcher.Battle));

                    foreach (GameEntity gameEntity in enemyEntities.GetEntities())
                    {
                        targetEntityIds.Add(gameEntity.id.Id);
                    }
                }
            }

            return targetEntityIds.ToArray();
        }

        /// <summary>
        /// Returns how much you can deduct from the remaining action time per second
        /// </summary>
        /// <param name="actionType">Type of action the character is performing</param>
        /// <param name="characterSpeed">Speed of the performing character</param>
        public static float GetActionTimeStep(ActionType actionType, GameEntity executingCharacter)
        {
            // TODO: Think of a formula that returns reasonable values
            return executingCharacter.speed.SpeedValue;
        }

        public static float GetTimeForAction(ActionType actionType)
        {
            float actionTime = 0f;

            actionTimesDefinition.TryGetValue(actionType, out actionTime);

            return actionTime;
        }
    }
}