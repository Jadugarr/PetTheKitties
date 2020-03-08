using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;

public class EnterChooseTargetStateSystem : GameReactiveSystem
{
    private Systems chooseTargetSystems = null;

    public EnterChooseTargetStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.ChooseTarget;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.Battle &&
               _context.subState.CurrentSubState == SubState.ChooseTarget;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (chooseTargetSystems == null)
        {
            CreateChooseTargetSystems();
        }

        GameSystemService.AddActiveSystems(chooseTargetSystems);
    }

    private void CreateChooseTargetSystems()
    {
        chooseTargetSystems = new Feature("ChooseTargetSystems")
            .Add(new InitializeChooseTargetSystem(_context))
            .Add(new ActionTargetChosenSystem(_context));
    }
}