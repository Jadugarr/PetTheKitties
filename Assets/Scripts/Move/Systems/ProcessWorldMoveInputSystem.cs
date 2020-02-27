using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using UnityEngine;

public class ProcessWorldMoveInputSystem : GameReactiveSystem
{
    private IGroup<GameEntity> playerGroup;

    public ProcessWorldMoveInputSystem(IContext<GameEntity> context) : base(context)
    {
        playerGroup = _context.GetGroup(GameMatcher.Player);
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
        GameEntity playerEntity = playerGroup.GetSingleEntity();
        if (playerEntity != null)
        {
            for (var i = 0; i < entities.Count; i++)
            {
                GameEntity gameEntity = entities[i];
                float inputValue = gameEntity.input.InputValue;
                // _context.CreateEntity()
                //     .AddMoveCharacter(playerEntity.id.Id, new Vector2(inputValue, 0));
                playerEntity.ReplaceMoveCharacter(playerEntity.id.Id, new Vector2(inputValue, 0));
                playerEntity.ReplaceCharacterDirection(inputValue < 0
                    ? CharacterDirection.Backward
                    : CharacterDirection.Forward);
            }
        }
    }
}