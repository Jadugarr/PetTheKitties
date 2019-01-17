using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class KittyInteractionSystem : GameReactiveSystem
{
    private GameEntity _interactedEntity;
    
    public KittyInteractionSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.InteractionTriggered, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        _interactedEntity = _context.GetEntityWithId(entity.interactionTriggered.InteractedEntityId);
        
        return _interactedEntity != null && _interactedEntity.isKitty;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World &&
               _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _interactedEntity?.view.View.GetComponent<AudioSource>().Play();
    }
}