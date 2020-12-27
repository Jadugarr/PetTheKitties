using Entitas;
using Entitas.Scripts.Common.Systems;

public class KeepGrapplingHookStartingPointAtPlayerPositionSystem : GameExecuteSystem
{
    private IGroup<GameEntity> _grapplingHookGroup;
    
    public KeepGrapplingHookStartingPointAtPlayerPositionSystem(GameContext context) : base(context)
    {
        _grapplingHookGroup =
            context.GetGroup(GameMatcher.AllOf(GameMatcher.GrapplingHookLine, GameMatcher.GrapplingHookStartingPoint, GameMatcher.GrapplingHookUserId));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        GameEntity grapplingHookEntity = _grapplingHookGroup.GetSingleEntity();

        if (grapplingHookEntity != null)
        {
            GameEntity userEntity = _context.GetEntityWithId(grapplingHookEntity.grapplingHookUserId.Value);
            if (userEntity.hasPosition)
            {
                grapplingHookEntity.grapplingHookLineRenderer.Value.SetPosition(0, userEntity.position.position);
            }
        }
    }
}