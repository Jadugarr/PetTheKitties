using System.Collections.Generic;
using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class SyncVelocitySystem : GameExecuteSystem
{
    private IGroup<GameEntity> relevantEntities;

    public SyncVelocitySystem(GameContext context) : base(context)
    {
        relevantEntities = _context.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.View));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        var entities = relevantEntities.GetEntities();
        for (var i = 0; i < entities.Length; i++)
        {
            GameEntity gameEntity = entities[i];
            Vector2 viewVelocity = gameEntity.view.View.GetComponent<Rigidbody2D>().velocity;
            gameEntity.ReplaceCharacterVelocity(viewVelocity);
        }
    }
}