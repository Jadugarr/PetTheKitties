using System.Collections.Generic;
using Cinemachine;
using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class SetCameraConfinerSystem : GameInitializeSystem
{
    public SetCameraConfinerSystem(GameContext context) : base(context)
    {
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        if (_context.camera != null)
        {
            Collider2D cameraCollider =
                GameObject.FindGameObjectWithTag(Tags.CameraConfiner)?.GetComponent<Collider2D>();

            if (cameraCollider)
            {
                CinemachineConfiner cameraConfiner = _context.camera.Camera.GetComponent<CinemachineConfiner>();
                cameraConfiner.m_BoundingShape2D = cameraCollider;
            }
            
        }
    }
}