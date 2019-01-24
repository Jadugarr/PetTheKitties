using System.Collections.Generic;
using Cinemachine;
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
        playerEntity.AddJumpForce(5f);
        
        //Create test kitty
        GameEntity kittyEntity = _context.CreateEntity();
        kittyEntity.isKitty = true;
        kittyEntity.isInteractable = true;
        kittyEntity.AddMovementSpeed(5f);
        kittyEntity.AddJumpForce(1f);
        
        _context.SetNewSubstate(SubState.WorldNavigation);
    }
}