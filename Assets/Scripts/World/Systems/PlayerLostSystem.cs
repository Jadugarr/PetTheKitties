
    using System.Collections.Generic;
    using Configurations;
    using Entitas;

    public class PlayerLostSystem : GameReactiveSystem
    {
        public PlayerLostSystem(IContext<GameEntity> context) : base(context)
        {
        }
        
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.SubState, GroupEvent.Added));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.subState.CurrentSubState == SubState.PlayerLost;
        }

        protected override bool IsInValidState()
        {
            return true;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            UIService.ShowWidget<AWidget>(GameConfigurations.AssetReferenceConfiguration.BattleResultWidget, new BattleResultWidgetProperties("I lost!"));
        }

    }