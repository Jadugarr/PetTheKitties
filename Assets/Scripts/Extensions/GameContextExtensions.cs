using UnityEngine;

namespace Entitas.Extensions
{
#pragma warning disable 618
    public static class GameContextExtensions
    {
        public static void SetNewSubstate(this GameContext context, SubState newSubstate)
        {
            if (newSubstate == context.subState.CurrentSubState)
            {
                return;
            }
            context.ReplaceSubState(context.subState.CurrentSubState, newSubstate);
            Debug.Log("Set new substate: " + newSubstate);
        }

        public static void SetNewGamestate(this GameContext context, GameState newGameState)
        {
            if (newGameState == context.gameState.CurrentGameState)
            {
                return;
            }
            context.ReplaceGameState(context.gameState.CurrentGameState, newGameState);
            Debug.Log("Set new gamestate: " + newGameState);
        }
    }
#pragma warning restore 618
}