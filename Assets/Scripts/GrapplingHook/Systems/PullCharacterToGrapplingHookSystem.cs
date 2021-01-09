using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class PullCharacterToGrapplingHookSystem : GameExecuteSystem
{
    private IGroup<GameEntity> _playerGroup;
    private IGroup<GameEntity> _grapplingHookGroup;
    
    public PullCharacterToGrapplingHookSystem(GameContext context) : base(context)
    {
        _playerGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CharacterState, GameMatcher.Position));
        _grapplingHookGroup = context.GetGroup(GameMatcher.GrapplingHookCurrentPoint);
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        GameEntity playerEntity = _playerGroup.GetSingleEntity();

        if (playerEntity != null && playerEntity.characterState.State == CharacterState.Grappled)
        {
            GameEntity hookEntity = _grapplingHookGroup.GetSingleEntity();
            if (hookEntity != null)
            {
                Vector2 dir = (hookEntity.grapplingHookCurrentPoint.Value - (Vector2)playerEntity.position.position).normalized;
            
                playerEntity.ReplacePosition(playerEntity.position.position + (Vector3)(dir * 10f * Time.fixedDeltaTime));
            }
        }
    }
}