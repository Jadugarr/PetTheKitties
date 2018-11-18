using Entitas.Battle.Enums;
using Entitas.World;
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

        public static void SetNewBattlestate(this GameContext context, BattleState newBattleState)
        {
            if (newBattleState == context.battleState.CurrentBattleState)
            {
                return;
            }
            context.ReplaceBattleState(context.battleState.CurrentBattleState, newBattleState);
            Debug.Log("Set new battlestate: " + newBattleState);
        }

        public static void SetNewWorldState(this GameContext context, WorldState newWorldState)
        {
            if (newWorldState == context.worldState.CurrentWorldState)
            {
                return;
            }
            context.ReplaceWorldState(context.worldState.CurrentWorldState, newWorldState);
            Debug.Log("Set new worldstate: " + newWorldState);
        }
        
        
    }
#pragma warning restore 618
}