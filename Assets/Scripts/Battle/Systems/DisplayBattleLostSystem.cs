namespace Entitas.Battle.Systems
{
    public class DisplayBattleLostSystem : IInitializeSystem, ITearDownSystem
    {
        public void Initialize()
        {
            UIService.ShowWidget<AWidget>(UiAssetTypes.BattleResultText, new BattleResultWidgetProperties("I lost!"));
        }

        public void TearDown()
        {
            UIService.HideWidget(UiAssetTypes.BattleResultText);
        }
    }
}