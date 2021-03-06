using System.Collections.Generic;
using Entitas;

public class FreeUpUseGrapplingHookInputSystem : GameReactiveSystem, IInitializeSystem
{
    private IGroup<GameEntity> _inputGroup;
    
    public FreeUpUseGrapplingHookInputSystem(GameContext context) : base(context)
    {
        _inputGroup = context.GetGroup(GameMatcher.Input);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.Input);
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    public void Initialize()
    {
        _context.isUseGrapplingHookInputAvailable = true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (_inputGroup != null && _inputGroup.count > 0)
        {
            foreach (GameEntity inputEntity in _inputGroup.GetEntities())
            {
                if (inputEntity.input.InputCommand == InputCommand.UseGrapple)
                {
                    return;
                }
            }
        }

        _context.isUseGrapplingHookInputAvailable = true;
    }
}