namespace Entitas.Battle.Systems
{
    public class DisplayBattleLostSystem : IInitializeSystem, ITearDownSystem
    {
        public void Initialize()
        {
            UIService.ShowWidget(AssetTypes.BattleResultText, new BattleResultWidgetProperties("I won!"));
        }

        public void TearDown()
        {
            UIService.HideWidget(AssetTypes.BattleResultText);
        }
    }
}