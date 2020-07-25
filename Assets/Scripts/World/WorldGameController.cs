using Entitas.Animations.Systems;
using Entitas.Controllers;
using Entitas.Input.Systems;
using Entitas.Kitty.Systems;
using Entitas.Position;
using Entitas.World.Systems;

namespace Entitas.World
{
    public class WorldGameController : AGameController
    {
        protected override IContext GetContext()
        {
            return Contexts.sharedInstance.game;
        }

        protected override void CreateSystems(IContext _context)
        {
            GameContext context = (GameContext) _context;

            #region WinConditionSystems

            updateSystems
                .Add(new CheckKillEnemiesConditionSystem(context))
                .Add(new CheckKittensReachedGoalConditionSystem(context));

            #endregion

            #region LoseConditionSystems

            updateSystems
                .Add(new CheckPlayerDeadConditionSystem(context));

            #endregion

            #region FallingStateSystems

            updateSystems
                .Add(new HandleGroundedJumpStateSystem(context));

            #endregion
            
            #region WorldUpdateSystems

            updateSystems
                //Input
                .Add(new ProcessPauseInputSystem(context))
                .Add(new ProcessUnpauseInputSystem(context))
                .Add(new ProcessWorldMoveInputSystem(context))
                .Add(new ProcessJumpInputSystem(context))
                .Add(new CountTimeSinceLastJumpInputSystem(context))
                .Add(new ResetTimeSinceLastJumpInputSystem(context))
                .Add(new CheckJumpInputAvailableSystem(context))
                .Add(new CountTimeSinceLastPauseInputSystem(context))
                .Add(new ResetTimeSinceLastPauseInputSystem(context))
                .Add(new CheckPauseInputAvailabilitySystem(context))
                .Add(new ProcessInteractionInputSystem(context))
                .Add(new SetCameraFollowTargetSystem(context))
                .Add(new WorldSceneLoadedSystem(context))
                .Add(new SetCameraConfinerSystem(context))
                .Add(new WorldPlayerAddedSystem(context))
                .Add(new KittyAddedSystem(context))
                .Add(new CharacterDeathSystem(context))
                .Add(new CheckInteractInputAvailableSystem(context))
                .Add(new KittyInteractionSystem(context))
                .Add(new CharacterStartFollowSystem(context))
                .Add(new CharacterDirectionSystem(context))
                .Add(new CharacterFollowSystem(context))
                .Add(new CharacterScaredSystem(context))
                .Add(new CharacterReachedGoalSystem(context))
                .Add(new HandleJumpEndingStateSystem(context))
                .Add(new HandleCharacterMovementStateSystem(context))
                .Add(new HandleFallingStateSystem(context))
                .Add(new UpdateKittyAmountDisplaySystem(context))
                .Add(new KittySavedSystem(context))
                //WinConditions
                .Add(new WinConditionControllerSystem(context))
                .Add(new LoseConditionControllerSystem(context))
                .Add(new PlayerWonSystem(context))
                .Add(new ExitPlayerWonStateSystem(context));

            #endregion

            #region WorldFixedUpdateSystems

            fixedUpdateSystems
                .Add(new SyncPositionAndViewSystem(context))
                .Add(new SyncVelocitySystem(context))
                .Add(new SyncMovementAnimationSystem(context))
                .Add(new CheckCharacterGroundStateSystem(context))
                .Add(new CharacterOnGroundSystem(context))
                .Add(new SetGravityScaleSystem(context))
                .Add(new MoveCharacterSystem(context))
                .Add(new AdjustMoveEndingVelocitySystem(context))
                .Add(new StartJumpCharacterSystem(context))
                .Add(new AdjustCharacterMovementToSlopeSystem(context))
                .Add(new AdjustEndingJumpVelocitySystem(context))
                .Add(new CharacterOnGroundMovementVelocitySystem(context))
                .Add(new CharacterAirborneMovementVelocitySystem(context))
                // Gravity
                .Add(new CharacterGravitySystem(context))
                //Position
                .Add(new RenderPositionSystem(context))
                //Velocity
                .Add(new RenderVelocitySystem(context))
                //Animations
                .Add(new RenderVelocityAnimationsSystem(context))
                .Add(new RenderCharacterStateAnimationsSystem(context));

            #endregion
        }
    }
}