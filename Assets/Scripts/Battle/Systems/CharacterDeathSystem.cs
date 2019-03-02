using System.Collections.Generic;
using Entitas;
using Entitas.Unity;
using UnityEngine;

public class CharacterDeathSystem : GameReactiveSystem
{
    private IGroup<GameEntity> actionEntityGroup;

    public CharacterDeathSystem(GameContext context) : base(context)
    {
        actionEntityGroup = context.GetGroup(GameMatcher.BattleAction);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AllOf(GameMatcher.Health, GameMatcher.View));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasHealth && entity.health.Health <= 0;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            foreach (GameEntity actionEntity in actionEntityGroup.GetEntities())
            {
                if (actionEntity.battleAction.EntityId == gameEntity.id.Id)
                {
                    actionEntity.Destroy();
                }
            }
            
            gameEntity.view.View.Unlink();
            GameObject.Destroy(gameEntity.view.View);

            gameEntity.Destroy();

            Debug.Log("Enemy died!");
        }
    }
}