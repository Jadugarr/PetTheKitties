using Entitas;

public class InitializeMainMenuSystem : IInitializeSystem, ITearDownSystem
{
    public void Initialize()
    {
        UIService.ShowWidget<AWidget>(UiAssetTypes.MainMenu, new MainMenuProperties());
    }

    public void TearDown()
    {
        UIService.HideWidget(UiAssetTypes.MainMenu);
    }
}