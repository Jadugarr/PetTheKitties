using System.Collections.Generic;
using Cinemachine;
using Entitas;
using UnityEngine;

public class SetCameraFollowTargetSystem : GameReactiveSystem
{
    public SetCameraFollowTargetSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.AllOf(GameMatcher.Player, GameMatcher.View), GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World &&
               _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity playerEntity in entities)
        {
            CinemachineVirtualCamera virtualCamera = _context.camera.Camera.GetComponent<CinemachineVirtualCamera>();
            virtualCamera.Follow = playerEntity.view.View.transform;
        }
    }
}