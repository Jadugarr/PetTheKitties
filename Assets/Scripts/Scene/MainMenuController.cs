using Entitas.Controllers;

namespace Entitas.Scene
{
    public class MainMenuController : AGameController
    {
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