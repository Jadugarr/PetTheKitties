using Configurations;
using Entitas;
using Entitas.Scripts.Common.Systems;

public class CheckJumpInputAvailableSystem : GameExecuteSystem, IInitializeSystem
{
    public CheckJumpInputAvailableSystem(GameContext context) : base(context)
    {
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    public void Initialize()
    {
        _context.isJumpInputAvailable = true;
    }

    protected override void ExecuteSystem()
    {
        _context.isJumpInputAvailable = !_context.hasTimeSinceLastJumpInput || _context.timeSinceLastJumpInput.Value >  GameConfigurations.MovementConstantsConfiguration.TimeUntilJumpInputAvailable;
    }
}