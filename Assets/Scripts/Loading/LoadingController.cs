namespace Entitas.Loading
{
    public class LoadingController : AGameController
    {
        public override GameControllerType GetGameControllerType()
        {
            return GameControllerType.Loading;
        }

        protected override IContext GetContext()
        {
            return Contexts.sharedInstance.game;
        }

        protected override void CreateSystems(IContext _context)
        {
            GameContext context = (GameContext) _context;
            updateSystems.Add(new AllLoadingComponentsRemovedSystem(context));
        }
    }
}