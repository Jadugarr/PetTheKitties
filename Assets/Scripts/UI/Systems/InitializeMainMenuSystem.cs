using Configurations;
using Entitas;

public class InitializeMainMenuSystem : IInitializeSystem, ITearDownSystem
{
    public void Initialize()
    {
#pragma warning disable 4014
        UIService.ShowWidget<AWidget>(GameConfigurations.AssetReferenceConfiguration.MainMenu, new MainMenuProperties());
#pragma warning restore 4014
    }

    public void TearDown()
    {
        UIService.HideWidget(GameConfigurations.AssetReferenceConfiguration.MainMenu);
    }
}