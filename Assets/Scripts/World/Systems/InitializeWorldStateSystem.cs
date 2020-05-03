using Entitas;
using Entitas.Extensions;
using Entitas.Scripts.Common.Systems;
using Entitas.Unity;
using Entitas.VisualDebugging.Unity;
using Entitas.World;
using UnityEngine;

public class InitializeWorldStateSystem : GameInitializeSystem, ITearDownSystem
{
    private IGroup<GameEntity> _playerGroup;
    private IGroup<GameEntity> _kittyGroup;
    private GameEntity kittyAmountDisplayEntity;
    private GameEntity totalKittyAmountEntity;
    private GameEntity savedKittyAmountEntity;

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
        int spawnPointAmount = GameObject.FindGameObjectsWithTag(Tags.KittySpawnPoint).Length;
        for (int i = 0; i < spawnPointAmount; i++)
        {
            CreateKitten();
        }
        CreateWinLoseConditions();
        CreateUiElements();
        
        totalKittyAmountEntity = _context.CreateEntity();
        totalKittyAmountEntity.AddTotalKittyAmount(spawnPointAmount);

        savedKittyAmountEntity = _context.CreateEntity();
        savedKittyAmountEntity.AddSavedKittyAmount(0);

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
        playerEntity.AddCharacterGroundState(CharacterGroundState.Undefined, Vector2.zero, 0);
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
        kittyEntity.AddCharacterGroundState(CharacterGroundState.Undefined, Vector2.zero, 0);
        kittyEntity.AddCharacterDirection(CharacterDirection.None);
        kittyEntity.AddStepSize(0.3f);
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

    private void CreateUiElements()
    {
        ValueDisplayWidget valueDisplayWidget =
            UIService.ShowWidget<ValueDisplayWidget>(UiAssetTypes.KittyAmountDisplay, null);
        kittyAmountDisplayEntity = _context.CreateEntity();
        kittyAmountDisplayEntity.AddKittyAmountDisplay(valueDisplayWidget);
        valueDisplayWidget.gameObject.Link(kittyAmountDisplayEntity);
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
        
        UIService.HideWidget(UiAssetTypes.KittyAmountDisplay);
        kittyAmountDisplayEntity.kittyAmountDisplay.KittyAmountDisplayWidget.gameObject.Unlink();
        kittyAmountDisplayEntity.Destroy();
        
        totalKittyAmountEntity.Destroy();
        savedKittyAmountEntity.Destroy();

        _context.RemoveWinCondition();
        _context.RemoveLoseCondition();
    }
}