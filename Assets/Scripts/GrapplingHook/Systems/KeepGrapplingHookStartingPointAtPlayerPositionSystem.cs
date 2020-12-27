using Entitas;
using Entitas.Scripts.Common.Systems;

public class KeepGrapplingHookStartingPointAtPlayerPositionSystem : GameExecuteSystem
{
    private IGroup<GameEntity> _playerGroup;
    private IGroup<GameEntity> _grapplingHookGroup;
    
    public KeepGrapplingHookStartingPointAtPlayerPositionSystem(GameContext context) : base(context)
    {
        _playerGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.Position));
        _grapplingHookGroup =
            context.GetGroup(GameMatcher.AllOf(GameMatcher.GrapplingHookLine, GameMatcher.GrapplingHookStartingPoint));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        GameEntity playerEntity = _playerGroup.GetSingleEntity();
        GameEntity grapplingHookEntity = _grapplingHookGroup.GetSingleEntity();

        if (playerEntity != null)
        {
            grapplingHookEntity?.grapplingHookLineRenderer.Value.SetPosition(0, playerEntity.position.position);
        }
    }
}