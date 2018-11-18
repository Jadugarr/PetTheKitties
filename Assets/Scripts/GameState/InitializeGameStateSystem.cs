using Entitas;
using Entitas.Extensions;

public class InitializeGameStateSystem : IInitializeSystem
{
    public void Initialize()
    {
        Contexts.sharedInstance.game.SetGameState(GameState.Undefined, GameState.MainMenu);
        Contexts.sharedInstance.game.SetSubState(SubState.Undefined, SubState.Undefined);
    }
}