using System.Collections.Generic;
using Cinemachine;
using Entitas;
using UnityEngine;

public class SetCameraConfinerSystem : GameReactiveSystem
{
    public SetCameraConfinerSystem(GameContext context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CameraConfiner, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.cameraConfiner.Value != null;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (entities.Count != 1)
        {
            Debug.LogWarning($"Not the right of camera confiner components available: {entities.Count}");
            return;
        }
        
        CinemachineConfiner cameraConfiner = _context.camera.Camera.GetComponent<CinemachineConfiner>();
        cameraConfiner.m_BoundingShape2D = entities[0].cameraConfiner.Value;
    }
}