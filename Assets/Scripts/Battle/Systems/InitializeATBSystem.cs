using Entitas;

public class InitializeATBSystem : IInitializeSystem
{
    private GameContext context;

    public InitializeATBSystem(GameContext context)
    {
        this.context = context;
    }

    public void Initialize()
    {
        //UIService.ShowWidget<AWidget>(UiAssetTypes.Atb, new ATBBarProperties(context));
    }
}