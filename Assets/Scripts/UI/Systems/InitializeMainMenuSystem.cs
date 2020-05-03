using Entitas;

public class InitializeMainMenuSystem : IInitializeSystem
{
    public void Initialize()
    {
        UIService.ShowWidget<AWidget>(UiAssetTypes.MainMenu, new MainMenuProperties());
    }
}