using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ProcessWorldMoveInputSystem : GameReactiveSystem
{
    private GameEntity playerEntity;

    public ProcessWorldMoveInputSystem(IContext<GameEntity> context) : base(context)
    {
        
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.Move;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (playerEntity == null)
        {
            playerEntity = _context.GetGroup(GameMatcher.Player).GetSingleEntity();
        }
        
        for (var i = 0; i < entities.Count; i++)
        {
            GameEntity gameEntity = entities[i];

            _context.CreateEntity()
                .AddMoveCharacter(playerEntity.id.Id, new Vector2(gameEntity.input.InputValue, 0).normalized);
        }
    }
}