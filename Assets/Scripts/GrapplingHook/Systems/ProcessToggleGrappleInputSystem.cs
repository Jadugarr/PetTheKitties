using System.Collections.Generic;
using Entitas;

public class ProcessToggleGrappleInputSystem : GameReactiveSystem
{
    private IGroup<GameEntity> _playerGroup;
    
    public ProcessToggleGrappleInputSystem(IContext<GameEntity> context) : base(context)
    {
        _playerGroup = context.GetGroup(GameMatcher.Player);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.ToggleGrapple && _context.isToggleGrappleInputAvailable;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (_playerGroup.count > 0)
        {
            GameEntity playerEntity = _playerGroup.GetSingleEntity();
            CharacterState currentCharacterState = playerEntity.characterState.State;
            if (currentCharacterState == CharacterState.Grappling)
            {
                playerEntity.ReplaceCharacterState(CharacterState.Idle);
            }
            else
            {
                playerEntity.ReplaceCharacterState(CharacterState.Grappling);
            }

            _context.isToggleGrappleInputAvailable = false;
        }
    }
}