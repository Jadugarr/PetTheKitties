using System.Collections.Generic;
using Entitas;
using Entitas.Battle.Systems;
using UnityEngine;

public class EnterBattleLostStateSystem : GameReactiveSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1){SubState.PlayerLost};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1){GameState.Battle};
    
    public EnterBattleLostStateSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.SubState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.subState.CurrentSubState == SubState.PlayerLost;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        if (!GameSystemService.HasSubSystemMapping(SubState.PlayerLost))
        {
            CreatePlayerLostSystems();
        }

        Systems playerLostSystems = GameSystemService.GetSubSystemMapping(SubState.PlayerLost);
        GameSystemService.AddActiveSystems(playerLostSystems);
    }

    private void CreatePlayerLostSystems()
    {
    }
}