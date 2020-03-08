using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;
using Entitas.Common;
using UnityEngine;

public class EnterChooseActionStateSystem : GameReactiveSystem
{
    public EnterChooseActionStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ChooseAction;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle &&
               _context.subState.CurrentSubState == SubState.ChooseAction;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSystemMapping(GameSystemType.ChooseAction))
        {
            CreateChooseActionSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSystemMapping(GameSystemType.ChooseAction));
    }

    private void CreateChooseActionSystems()
    {
        Systems chooseActionSystems = new Feature("ChooseActionSystems")
            .Add(new InitializeChooseActionSystem(_context))
            .Add(new ActionChosenSystem(_context));
        
        GameSystemService.AddSystemMapping(GameSystemType.ChooseAction, chooseActionSystems);
    }
}