using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class InitializeWorldStateSystem : GameInitializeSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.Undefined};
    protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.World};

    public InitializeWorldStateSystem(GameContext context) : base(context)
    {
    }

    protected override void ExecuteSystem()
    {
        //Create player entity
        GameEntity playerEntity = _context.CreateEntity();
        playerEntity.isPlayer = true;
        playerEntity.AddMovementSpeed(5f);
        _context.SetNewSubstate(SubState.WorldNavigation);
    }
}