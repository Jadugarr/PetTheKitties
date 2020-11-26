using Configurations;
using Entitas;

public class DisplayBattleWonSystem : IInitializeSystem, ITearDownSystem
{
    public void Initialize()
    {
#pragma warning disable 4014
        UIService.ShowWidget<AWidget>(GameConfigurations.AssetReferenceConfiguration.BattleResultWidget, new BattleResultWidgetProperties("I won!"));
#pragma warning restore 4014
    }

    public void TearDown()
    {
        UIService.HideWidget(GameConfigurations.AssetReferenceConfiguration.BattleResultWidget);
    }
}