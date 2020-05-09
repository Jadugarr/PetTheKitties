using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class CountTimeSinceLastJumpInputSystem : GameExecuteSystem
{
    public CountTimeSinceLastJumpInputSystem(GameContext context) : base(context)
    {
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        float timeSinceLastJumpInput = _context.hasTimeSinceLastJumpInput ? _context.timeSinceLastJumpInput.Value : 0f;
        _context.ReplaceTimeSinceLastJumpInput(timeSinceLastJumpInput + Time.deltaTime);
    }
}