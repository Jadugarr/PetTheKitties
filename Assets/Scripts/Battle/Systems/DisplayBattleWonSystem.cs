using Entitas;

public class DisplayBattleWonSystem : IInitializeSystem, ITearDownSystem
{
    public void Initialize()
    {
        UIService.ShowWidget<AWidget>(UiAssetTypes.BattleResultText, new BattleResultWidgetProperties("I won!"));
    }

    public void TearDown()
    {
        UIService.HideWidget(UiAssetTypes.BattleResultText);
    }
}