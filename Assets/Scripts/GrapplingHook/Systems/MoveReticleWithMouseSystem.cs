using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class MoveReticleWithMouseSystem : GameExecuteSystem
{
    private IGroup<GameEntity> _reticleGroup;
    private IGroup<GameEntity> _playerGroup;
    
    public MoveReticleWithMouseSystem(GameContext context) : base(context)
    {
        _reticleGroup = context.GetGroup(GameMatcher.GrapplingHookReticle);
        _playerGroup = context.GetGroup(GameMatcher.Player);
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        if (_reticleGroup != null && _reticleGroup.count > 0
        && _playerGroup != null && _playerGroup.count > 0)
        {
            GameEntity reticleEntity = _reticleGroup.GetSingleEntity();
            GameEntity playerEntity = _playerGroup.GetSingleEntity();
            CameraComponent cameraEntity = _context.camera;
            
            Vector2 mousePosition = cameraEntity.Camera.ScreenToWorldPoint(Input.mousePosition);
            Vector2 playerPosition = playerEntity.position.position;
            Vector2 direction = mousePosition - playerPosition;
            float playerToMouseDistance = Vector2.Distance(mousePosition, playerPosition);
            direction = direction.normalized;
            
            Vector2 reticlePosition = playerPosition + direction * Mathf.Min(playerEntity.grapplingDistance.Value, playerToMouseDistance);
            
            reticleEntity.ReplacePosition(reticlePosition);
        }
    }
}