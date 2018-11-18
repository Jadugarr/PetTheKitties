using Entitas.Extensions;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ReturnToMainMenuBehaviour : AWidget
{
    private Button returnButton;
    private GameContext context;

    public override void Open()
    {
        returnButton = GetComponent<Button>();
        context = Contexts.sharedInstance.game;
        AddEventListener();
    }

    public override void Close()
    {
        RemoveEventListener();
    }

    public override string GetName()
    {
        return AssetTypes.ReturnButton;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    private void OnButtonClicked()
    {
        context.SetNewGamestate(GameState.MainMenu);
    }

    private void AddEventListener()
    {
        returnButton.onClick.AddListener(OnButtonClicked);
    }

    private void RemoveEventListener()
    {
        returnButton.onClick.RemoveListener(OnButtonClicked);
    }
}