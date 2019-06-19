using System.Collections.Generic;
using Cinemachine;
using Entitas;
using Entitas.Extensions;
using Entitas.Scripts.Common.Systems;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
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
        CreatePlayer();
        CreateKitten();
        CreateKitten();
        CreateWinLoseConditions();

        _context.SetNewSubstate(SubState.WorldNavigation);
    }

    private void CreatePlayer()
    {
        //Create player entity
        GameEntity playerEntity = _context.CreateEntity();
        playerEntity.isPlayer = true;
        playerEntity.AddMovementSpeed(5f);
        playerEntity.AddJumpForce(12f);
        playerEntity.AddAcceleration(20f);
        playerEntity.AddHealth(666);
        playerEntity.AddCharacterState(CharacterState.Idle);
    }

    private void CreateKitten()
    {
        //Create test kitty
        GameEntity kittyEntity = _context.CreateEntity();
        kittyEntity.isKitty = true;
        kittyEntity.isInteractable = true;
        kittyEntity.AddMovementSpeed(5f);
        kittyEntity.AddJumpForce(12f);
        kittyEntity.AddAcceleration(20f);
        kittyEntity.AddCharacterState(CharacterState.Idle);
    }

    private void CreateWinLoseConditions()
    {
        _context.CreateEntity()
            .AddWinCondition(ConditionModifier.All,
                new[] {new WinConditionState {IsFulfilled = false, WinCondition = WinCondition.KittensReachedGoal}});
        _context.CreateEntity()
            .AddLoseCondition(ConditionModifier.All,
                new[] {new LoseConditionState {IsFulfilled = false, LoseCondition = LoseCondition.PlayerDead}});
    }

    public void TearDown()
    {
        foreach (GameEntity entity in _kittyGroup.GetEntities())
        {
            if (entity.view != null)
            {
                entity.view.View.Unlink();
                entity.view.View.DestroyGameObject();
            }

            entity.Destroy();
        }

        foreach (GameEntity entity in _playerGroup.GetEntities())
        {
            if (entity.view != null)
            {
                entity.view.View.Unlink();
                entity.view.View.DestroyGameObject();
            }

            entity.Destroy();
        }

        _context.RemoveWinCondition();
        _context.RemoveLoseCondition();
    }
}