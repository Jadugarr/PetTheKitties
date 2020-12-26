using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class GrapplingHookExtendLineSystem : GameExecuteSystem
{
    private IGroup<GameEntity> _grapplingHookLineGroup;

    public GrapplingHookExtendLineSystem(GameContext context) : base(context)
    {
        _grapplingHookLineGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.GrapplingHookLine,
            GameMatcher.GrapplingHookSpeed, GameMatcher.GrapplingHookStartingPoint,
            GameMatcher.GrapplingHookEndPoint, GameMatcher.GrapplingHookCurrentPoint, GameMatcher.GrapplingHookLineRenderer));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity entity in _grapplingHookLineGroup.GetEntities())
        {
            Vector3 startPos = entity.grapplingHookStartingPoint.Value;
            Vector3 endPos = entity.grapplingHookEndPoint.Value;
            Vector3 currentPos = entity.grapplingHookCurrentPoint.Value;

            if (Vector3.Distance(currentPos, endPos) > 0.01f)
            {
                Vector3 dir = (endPos - currentPos).normalized;

                Vector3 newPos = currentPos + dir * (entity.grapplingHookSpeed.Value * Time.deltaTime);

                entity.ReplaceGrapplingHookCurrentPoint(newPos);
                LineRenderer lineRenderer = entity.grapplingHookLineRenderer.Value;
                lineRenderer.positionCount = 2;
                lineRenderer.SetPositions(new []{startPos, newPos});
            }
        }
    }
}