using Entitas.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleResultWidget : AWidget
{
    [SerializeField] private TMP_Text resultTextfield;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button quitGameButton;
    [SerializeField] private Button nextLevelButton;

    private GameContext context;

    public override void Open()
    {
        context = Contexts.sharedInstance.game;
        
        restartButton.onClick.AddListener(OnRestartClicked);
        mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        quitGameButton.onClick.AddListener(OnQuitClicked);
        nextLevelButton.onClick.AddListener(OnNextLevelClicked);
    }

    public override void Close()
    {
        restartButton.onClick.RemoveListener(OnRestartClicked);
        mainMenuButton.onClick.RemoveListener(OnMainMenuClicked);
        quitGameButton.onClick.RemoveListener(OnQuitClicked);
        nextLevelButton.onClick.RemoveListener(OnNextLevelClicked);
    }

    public override string GetName()
    {
        return UiAssetTypes.BattleResultText;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    protected override void OnNewProperties()
    {
        var props = (BattleResultWidgetProperties) properties;

        resultTextfield.text = props.TextToDisplay;
    }

    private void OnQuitClicked()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    private void OnMainMenuClicked()
    {
        context.SetNewSubstate(SubState.Undefined);
        context.SetNewGamestate(GameState.MainMenu);
    }

    private void OnRestartClicked()
    {
        context.CreateEntity().AddRestartController(GameControllerType.World);
    }

    private void OnNextLevelClicked()
    {
        context.isLoadNextLevel = true;
    }
}