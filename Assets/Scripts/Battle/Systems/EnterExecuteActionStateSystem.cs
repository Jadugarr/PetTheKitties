using System.Collections.Generic;
using Entitas;
using Entitas.Actions.Systems;
using Entitas.Common;
using UnityEngine;

public class EnterExecuteActionStateSystem : GameReactiveSystem
{
    public EnterExecuteActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ExecuteAction;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle &&
               _context.subState.CurrentSubState == SubState.ExecuteAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSystemMapping(GameSystemType.ExecuteAction))
        {
            CreateExecuteActionSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSystemMapping(GameSystemType.ExecuteAction));
    }

    private void CreateExecuteActionSystems()
    {
        Systems executeActionSystems = new Feature("ExecuteActionSystems")
            //Actions
            .Add(new InitializeExecuteActionSystem(_context))
            .Add(new ExecutePlayerAttackActionSystem(_context))
            .Add(new ExecuteDefenseActionSystem(_context))
            .Add(new ReleaseDefenseActionSystem(_context))
            .Add(new ActionFinishedSystem(_context));

        GameSystemService.AddSystemMapping(GameSystemType.ExecuteAction, executeActionSystems);
    }
}