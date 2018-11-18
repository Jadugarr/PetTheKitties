using Entitas;

public class InitializeMainMenuSystem : IInitializeSystem
{
    public void Initialize()
    {
        UIService.ShowWidget(AssetTypes.MainMenu, new MainMenuProperties());
    }
}