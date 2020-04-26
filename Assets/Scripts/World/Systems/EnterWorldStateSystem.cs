using System.Collections.Generic;
using Entitas;
using Entitas.Animations.Systems;
using Entitas.Common;
using Entitas.Input.Systems;
using Entitas.Kitty.Systems;
using Entitas.Position;
using Entitas.World.Systems;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterWorldStateSystem : GameReactiveSystem
{
    private IGroup<GameEntity> _sceneLoaded;

    public EnterWorldStateSystem(IContext<GameEntity> context) : base(context)
    {
        _sceneLoaded = _context.GetGroup(GameMatcher.SceneLoaded);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.GameState);
    }

    protected override bool Filter(GameEntity entity)
    {
        return _context.gameState.CurrentGameState == GameState.World;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        _sceneLoaded.OnEntityAdded += OnWorldSceneLoaded;

        _context.CreateEntity().AddChangeScene(GameSceneConstants.WorldScene, LoadSceneMode.Additive);
    }

    private void OnWorldSceneLoaded(IGroup<GameEntity> @group, GameEntity entity, int index, IComponent component)
    {
        _sceneLoaded.OnEntityAdded -= OnWorldSceneLoaded;
        
        if (!GameSystemService.HasSystemMapping(GameSystemType.World))
        {
            CreateWorldSystems();
        }

        GameSystemService.AddActiveSystems(GameSystemService.GetSystemMapping(GameSystemType.World));
        GameSystemService.AddActiveSystems(GameSystemService.GetSystemMapping(GameSystemType.WorldFixedUpdate), SystemsUpdateType.FixedUpdate);
    }

    private void CreateWorldSystems()
    {
        Systems worldSystems = new Feature("WorldSystemsUpdate")
            .Add(new ProcessInteractionInputSystem(_context))
            .Add(new SetCameraFollowTargetSystem(_context))
            .Add(new InitializeWorldStateSystem(_context))
            .Add(new SetCameraConfinerSystem(_context))
            .Add(new WorldPlayerAddedSystem(_context))
            .Add(new KittyAddedSystem(_context))
            .Add(new CharacterDeathSystem(_context))
            .Add(new CheckInteractInputAvailableSystem(_context))
            .Add(new KittyInteractionSystem(_context))
            .Add(new CharacterStartFollowSystem(_context))
            .Add(new CharacterDirectionSystem(_context))
            .Add(new CharacterFollowSystem(_context))
            .Add(new CharacterScaredSystem(_context))
            .Add(new CharacterReachedGoalSystem(_context))
            .Add(new HandleCharacterMovementStateSystem(_context))
            .Add(new ManageJumpingStateHandlingSystem(_context))
            .Add(new ManageFallingStateHandlingSystem(_context))
            .Add(new HandleFallingStateSystem(_context))
            //WinConditions
            .Add(new InitializeAndTeardownWinConditionsSystem(_context))
            .Add(new InitializeAndTeardownLoseConditionsSystem(_context))
            .Add(new WinConditionControllerSystem(_context))
            .Add(new LoseConditionControllerSystem(_context));
        
        Systems worldSystemsFixedUpdate = new Feature("WorldSystemsFixedUpdate")
            .Add(new SyncPositionAndViewSystem(_context))
            .Add(new SyncVelocitySystem(_context))
            .Add(new SyncMovementAnimationSystem(_context))
            .Add(new CheckCharacterGroundStateSystem(_context))
            .Add(new CharacterOnGroundSystem(_context))
            .Add(new SetGravityScaleSystem(_context))
            .Add(new MoveCharacterSystem(_context))
            .Add(new AdjustMoveEndingVelocitySystem(_context))
            .Add(new StartJumpCharacterSystem(_context))
            .Add(new AdjustCharacterMovementToSlopeSystem(_context))
            .Add(new CharacterOnGroundMovementVelocitySystem(_context))
            .Add(new CharacterAirborneMovementVelocitySystem(_context))
            // Gravity
            .Add(new CharacterGravitySystem(_context))
            //Position
            .Add(new RenderPositionSystem(_context))
            //Velocity
            .Add(new RenderVelocitySystem(_context))
            //Animations
            .Add(new RenderVelocityAnimationsSystem(_context))
            .Add(new RenderCharacterStateAnimationsSystem(_context));
        GameSystemService.AddSystemMapping(GameSystemType.World, worldSystems);
        GameSystemService.AddSystemMapping(GameSystemType.WorldFixedUpdate, worldSystemsFixedUpdate);
    }
}