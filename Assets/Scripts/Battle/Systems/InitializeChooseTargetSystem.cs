using Entitas.Extensions;
using Entitas.Utils;

namespace Entitas.Battle.Systems
{
    public class InitializeChooseTargetSystem : IInitializeSystem
    {
        private GameContext context;

        public InitializeChooseTargetSystem(GameContext context)
        {
            this.context = context;
        }

        public void Initialize()
        {
            IGroup<GameEntity> choosingEntities =
                context.GetGroup(GameMatcher.AllOf(GameMatcher.BattleAction, GameMatcher.ExecutionTime));
            GameEntity currentEntity = null;
            foreach (GameEntity choosingEntity in choosingEntities)
            {
                if (choosingEntity.executionTime.RemainingTime <= 0f && choosingEntity.battleAction.ActionAtbType == ActionATBType.Waiting)
                {
                    currentEntity = choosingEntity;
                    break;
                }
            }

            if (currentEntity != null)
            {
                GameEntity characterEntity = context.GetEntityWithId(currentEntity.battleAction.EntityId);
                int[] possibleTargetIds =
                    BattleActionUtils.GetTargetEntitiesByActionType(currentEntity.battleAction.ActionType,
                        characterEntity, context);

                if (possibleTargetIds.Length == 1 && possibleTargetIds[0] == characterEntity.id.Id)
                {
                    currentEntity.AddTarget(characterEntity.id.Id);
                }
                else
                {
                    UIService.ShowWidget(AssetTypes.CharacterChooser,
                        new CharacterChooserProperties(possibleTargetIds,
                            context, currentEntity));
                }
            }
            else
            {
                context.SetNewSubstate(SubState.Waiting);
            }
        }
    }
}