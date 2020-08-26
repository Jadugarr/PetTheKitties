using System;
using System.Collections.Generic;
using Entitas;

public class RestartControllerSystem : GameReactiveSystem
{
    private IGroup<GameEntity> controllerGroup;

    public RestartControllerSystem(IContext<GameEntity> context) : base(context)
    {
        controllerGroup = context.GetGroup(GameMatcher.Controller);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.RestartController, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        GameEntity[] controllerEntities = controllerGroup.GetEntities();

        foreach (GameEntity gameEntity in entities)
        {
            if (gameEntity != null && gameEntity.restartController != null)
            {
                foreach (GameEntity controllerEntity in controllerEntities)
                {
                    AGameController controller = controllerEntity.controller.Value;

                    if (gameEntity.restartController.Value == controller.GetGameControllerType())
                    {
                        controller.RestartController();
                    }
                }

                gameEntity.Destroy();
            }
        }
    }
}