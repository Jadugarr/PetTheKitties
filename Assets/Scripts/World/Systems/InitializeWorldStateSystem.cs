using System.Collections.Generic;
using Cinemachine;
using Entitas;
using Entitas.Extensions;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class InitializeWorldStateSystem : GameInitializeSystem, ITearDownSystem
{
    private IGroup<GameEntity> _playerGroup;
    private IGroup<GameEntity> _kittyGroup;
    public InitializeWorldStateSystem(GameContext context) : base(context)
    {
        _playerGroup = context.GetGroup(GameMatcher.Player);
        _kittyGroup = context.GetGroup(GameMatcher.Kitty);
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
        playerEntity.AddJumpForce(10f);
        playerEntity.AddAcceleration(20f);
        
        //Create test kitty
        GameEntity kittyEntity = _context.CreateEntity();
        kittyEntity.isKitty = true;
        kittyEntity.isInteractable = true;
        kittyEntity.AddMovementSpeed(5f);
        kittyEntity.AddJumpForce(10f);
        kittyEntity.AddAcceleration(20f);
        
        _context.SetNewSubstate(SubState.WorldNavigation);
    }

    public void TearDown()
    {
        foreach (GameEntity entity in _kittyGroup.GetEntities())
        {
            entity.Destroy();
        }
        
        foreach (GameEntity entity in _playerGroup.GetEntities())
        {
            entity.Destroy();
        }
    }
}