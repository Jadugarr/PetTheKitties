using Entitas;

public class InitializeMainMenuSystem : IInitializeSystem
{
    public void Initialize()
    {
        UIService.ShowWidget(UiAssetTypes.MainMenu, new MainMenuProperties());
    }
}