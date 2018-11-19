using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class InitializeWorldStateSystem : GameInitializeSystem
{
    public InitializeWorldStateSystem(GameContext context) : base(context)
    {
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World;
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