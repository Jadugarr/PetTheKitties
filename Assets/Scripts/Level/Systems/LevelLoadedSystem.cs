using System.Collections.Generic;
using Entitas;
using Entitas.Extensions;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using Entitas.World;
using UnityEngine;

public class LevelLoadedSystem : GameReactiveSystem, ITearDownSystem
{
    private IGroup<GameEntity> _playerGroup;
    private IGroup<GameEntity> _kittyGroup;

    public LevelLoadedSystem(IContext<GameEntity> context) : base(context)
    {
        _playerGroup = context.GetGroup(GameMatcher.Player);
        _kittyGroup = context.GetGroup(GameMatcher.Kitty);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(
            new TriggerOnEvent<GameEntity>(GameMatcher.AllOf(GameMatcher.Level, GameMatcher.View), GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.isLevel && entity.hasView && entity.view.View != null;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        CreatePlayer();
        int spawnPointAmount = GameObject.FindGameObjectsWithTag(Tags.KittySpawnPoint).Length;
        for (int i = 0; i < spawnPointAmount; i++)
        {
            CreateKitten();
        }

        CreateWinLoseConditions();
        AddCameraConfinerEntity();

        _context.SetNewSubstate(SubState.WorldNavigation);
    }

    private void CreatePlayer()
    {
        //Create player entity
        GameEntity playerEntity = _context.CreateEntity();
        playerEntity.isPlayer = true;
        playerEntity.AddMovementSpeed(5f);
        playerEntity.AddJumpForce(15f);
        playerEntity.AddAcceleration(20f);
        playerEntity.AddHealth(666);
        playerEntity.AddCharacterState(CharacterState.Idle);
        playerEntity.AddCurrentMovementSpeed(0f);
        playerEntity.AddCharacterGroundState(CharacterGroundState.Undefined);
        playerEntity.AddPreviousCharacterGroundState(CharacterGroundState.Undefined);
        playerEntity.AddDistanceToGround(0f);
        playerEntity.AddGroundHitNormal(Vector2.zero);
        playerEntity.AddCharacterDirection(CharacterDirection.None);
        playerEntity.AddStepSize(0.5f);
    }

    private void CreateKitten()
    {
        //Create test kitty
        GameEntity kittyEntity = _context.CreateEntity();
        kittyEntity.isKitty = true;
        kittyEntity.isInteractable = true;
        kittyEntity.AddMovementSpeed(5f);
        kittyEntity.AddJumpForce(15f);
        kittyEntity.AddAcceleration(20f);
        kittyEntity.AddCharacterState(CharacterState.Idle);
        kittyEntity.AddCurrentMovementSpeed(0f);
        kittyEntity.AddCharacterGroundState(CharacterGroundState.Undefined);
        kittyEntity.AddPreviousCharacterGroundState(CharacterGroundState.Undefined);
        kittyEntity.AddDistanceToGround(0f);
        kittyEntity.AddGroundHitNormal(Vector2.zero);
        kittyEntity.AddCharacterDirection(CharacterDirection.None);
        kittyEntity.AddStepSize(0.3f);
    }

    private void CreateWinLoseConditions()
    {
        _context.isWinConditionsFulfilled = false;
        _context.isLoseConditionsFulfilled = false;
        
        _context.CreateEntity()
            .AddWinCondition(ConditionModifier.All,
                new[] {new WinConditionState {IsFulfilled = false, WinCondition = WinCondition.KittenPet}});
        _context.CreateEntity()
            .AddLoseCondition(ConditionModifier.All,
                new[] {new LoseConditionState {IsFulfilled = false, LoseCondition = LoseCondition.PlayerDead}});
    }

    private void AddCameraConfinerEntity()
    {
        Collider2D cameraCollider =
            GameObject.FindGameObjectWithTag(Tags.CameraConfiner)?.GetComponent<Collider2D>();

        if (cameraCollider != null)
        {
            _context.ReplaceCameraConfiner(cameraCollider);
        }
    }

    public void TearDown()
    {
        foreach (GameEntity entity in _kittyGroup.GetEntities())
        {
            if (entity.view != null && entity.view.View != null)
            {
                entity.view.View.Unlink();
                entity.view.View.DestroyGameObject();
            }

            entity.Destroy();
        }

        foreach (GameEntity entity in _playerGroup.GetEntities())
        {
            if (entity.view != null && entity.view.View != null)
            {
                entity.view.View.Unlink();
                entity.view.View.DestroyGameObject();
            }

            entity.Destroy();
        }

        if (_context.hasWinCondition)
        {
            _context.RemoveWinCondition();
        }

        if (_context.hasLoseCondition)
        {
            _context.RemoveLoseCondition();
        }

        if (_context.hasCameraConfiner)
        {
            _context.RemoveCameraConfiner();
        }
    }
}