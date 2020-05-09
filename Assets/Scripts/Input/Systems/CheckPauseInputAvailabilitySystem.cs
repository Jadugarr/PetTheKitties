using Configurations;
using Entitas;
using Entitas.Scripts.Common.Systems;

public class CheckPauseInputAvailabilitySystem : GameExecuteSystem, IInitializeSystem
{
    public CheckPauseInputAvailabilitySystem(GameContext context) : base(context)
    {
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        _context.isPauseInputAvailable = !_context.hasTimeSinceLastPauseInput ||
                                         _context.timeSinceLastPauseInput.Value > GameConfigurations
                                             .MovementConstantsConfiguration.TimeUntilPauseInputAvailable;
    }

    public void Initialize()
    {
        _context.isPauseInputAvailable = true;
    }
}