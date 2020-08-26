namespace Entitas.Scene
{
    public class MainMenuController : AGameController
    {
        public override GameControllerType GetGameControllerType()
        {
            return GameControllerType.MainMenu;
        }

        protected override IContext GetContext()
        {
            return Contexts.sharedInstance.game;
        }

        protected override void CreateSystems(IContext context)
        {
            #region MainMenuSystems

            updateSystems
                .Add(new InitializeMainMenuSystem());

            #endregion
        }
    }
}