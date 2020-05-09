using Configurations;
using Entitas.Scripts.Common.Systems;

public class CheckJumpInputAvailableSystem : GameExecuteSystem
{
    public CheckJumpInputAvailableSystem(GameContext context) : base(context)
    {
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        _context.isJumpInputAvailable = !_context.hasTimeSinceLastJumpInput || _context.timeSinceLastJumpInput.Value >  GameConfigurations.MovementConstantsConfiguration.TimeUntilJumpInputAvailable;
    }
}