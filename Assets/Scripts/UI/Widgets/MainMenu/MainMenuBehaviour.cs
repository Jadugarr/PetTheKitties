using Entitas.Extensions;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehaviour : AWidget
{
    [SerializeField] private Button battlePrototypeButton;

    [SerializeField] private Button exitGameButton;

    [SerializeField] private Button worldNavigationButton;

    private GameContext context;

    public override void Open()
    {
        context = Contexts.sharedInstance.game;
        AddEventListeners();
    }

    public override void Close()
    {
        RemoveEventListeners();
    }

    public override string GetName()
    {
        return UiAssetTypes.MainMenu;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    private void OnBattleButtonClicked()
    {
        context.SetNewGamestate(GameState.Battle);
    }

    private void OnWorldNavigationButtonClicked()
    {
        context.SetNewGamestate(GameState.World);
    }

    private void OnExitGameButtonClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void AddEventListeners()
    {
        battlePrototypeButton.onClick.AddListener(OnBattleButtonClicked);
        exitGameButton.onClick.AddListener(OnExitGameButtonClicked);
        worldNavigationButton.onClick.AddListener(OnWorldNavigationButtonClicked);
    }

    private void RemoveEventListeners()
    {
        battlePrototypeButton.onClick.RemoveListener(OnBattleButtonClicked);
        exitGameButton.onClick.RemoveListener(OnExitGameButtonClicked);
    }
}