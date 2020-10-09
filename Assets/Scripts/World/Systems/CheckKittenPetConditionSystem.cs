using System.Collections.Generic;

namespace Entitas.World.Systems
{
    public class CheckKittenPetConditionSystem : GameReactiveSystem
    {
        private IGroup<GameEntity> _kittenGroup;

        public CheckKittenPetConditionSystem(IContext<GameEntity> context) : base(context)
        {
            _kittenGroup = context.GetGroup(GameMatcher.Kitty);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(GameMatcher.HasBeenPet);
        }

        protected override bool Filter(GameEntity entity)
        {
            return true;
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.World;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            bool conditionFulfilled = true;

            foreach (GameEntity gameEntity in _kittenGroup.GetEntities())
            {
                if (!gameEntity.isHasBeenPet)
                {
                    conditionFulfilled = false;
                    break;
                }
            }

            if (conditionFulfilled)
            {
                WinConditionComponent winConditions = _context.winCondition;

                for (var i = 0; i < winConditions.WinConditions.Length; i++)
                {
                    WinConditionState currentWinCondition = winConditions.WinConditions[i];
                    if (currentWinCondition.WinCondition == WinCondition.KittenPet)
                    {
                        winConditions.WinConditions[i].IsFulfilled = true;
                        break;
                    }
                }

                _context.ReplaceWinCondition(_context.winCondition.ConditionModifier, winConditions.WinConditions);
            }
        }
    }
}