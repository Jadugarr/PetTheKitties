using System.Collections.Generic;
using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class PlayerReachedGrapplingHookTargetSystem : GameExecuteSystem
{
    private IGroup<GameEntity> _playerGroup;
    private IGroup<GameEntity> _grapplingHookCurrentPointGroup;

    public PlayerReachedGrapplingHookTargetSystem(GameContext context) : base(context)
    {
        _playerGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.CharacterState, GameMatcher.Position));
        _grapplingHookCurrentPointGroup = context.GetGroup(GameMatcher.GrapplingHookCurrentPoint);
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
            GameEntity grapplingHookEntity = _grapplingHookCurrentPointGroup.GetSingleEntity();
            BoxCollider2D playerCollider = playerEntity.collider.Collider;

            if (grapplingHookEntity.hasGrapplingHookCurrentPoint)
            {
                Vector2 grapplingEndPoint = grapplingHookEntity.grapplingHookCurrentPoint.Value;
                Vector2 closestPointToEndPoint = playerCollider.ClosestPoint(grapplingEndPoint);
                float distanceBetweenPoints = Vector2.Distance(grapplingEndPoint, closestPointToEndPoint);
                
                Debug.DrawLine(closestPointToEndPoint, grapplingEndPoint, Color.blue);
                
                if (distanceBetweenPoints <= 0.1f)
                {
                    playerEntity.ReplaceCharacterState(CharacterState.Planted);
                    playerEntity.RemoveUsedGrapplingHookId();
                    playerEntity.isUseGrapplingHook = false;
                
                    grapplingHookEntity.DestroyEntity();
                }
            }
        }
    }
}