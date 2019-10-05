using System.Collections.Generic;
using Entitas;

public class ProcessRaycastTestInputSystem : GameReactiveSystem, ICleanupSystem
{
    private IGroup<GameEntity> raycastTestEntities;
    
    public ProcessRaycastTestInputSystem(IContext<GameEntity> context) : base(context)
    {
        raycastTestEntities = context.GetGroup(GameMatcher.RaycastTest);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.input.InputCommand == InputCommand.RaycastTest;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _context.CreateEntity().isRaycastTest = true;
    }

    public void Cleanup()
    {
        foreach (GameEntity entity in raycastTestEntities.GetEntities())
        {
            entity.Destroy();
        }
    }
}