using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CharacterStartFollowSystem : GameReactiveSystem
{
    private GameEntity _interactedEntity;
    private IGroup<GameEntity> _playerGroup;

    public CharacterStartFollowSystem(IContext<GameEntity> context) : base(context)
    {
        _playerGroup = _context.GetGroup(GameMatcher.Player);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.InteractionTriggered,
            GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        _interactedEntity = _context.GetEntityWithId(entity.interactionTriggered.InteractedEntityId);

        return _interactedEntity != null;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World &&
               _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        GameEntity playerEntity = _playerGroup.GetSingleEntity();

        if (playerEntity != null)
        {
            _interactedEntity.ReplaceFollowCharacter(playerEntity.id.Id);
        }
    }
}