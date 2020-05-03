using System.Collections.Generic;
using Entitas;

public class UpdateKittyAmountDisplaySystem : GameReactiveSystem
{
    public UpdateKittyAmountDisplaySystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(GameMatcher.AnyOf(GameMatcher.TotalKittyAmount, GameMatcher.SavedKittyAmount)
            .Added());
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        KittyAmountDisplayComponent kittyAmountDisplayComponent = _context.kittyAmountDisplay;
        if (kittyAmountDisplayComponent == null) return;

        SavedKittyAmountComponent savedKittyAmountComponent = _context.savedKittyAmount;
        TotalKittyAmountComponent totalKittyAmountComponent = _context.totalKittyAmount;

        kittyAmountDisplayComponent.KittyAmountDisplayWidget.SetValueText($"{savedKittyAmountComponent.SavedKittyAmount} / {totalKittyAmountComponent.TotalKittyAmount}");
    }
}