using System;
using System.Collections.Generic;
using Entitas;

public static class WinLoseConditionConfiguration
{
    private static Dictionary<WinCondition, Type> winConditionSystemMap = new Dictionary<WinCondition, Type>
    {
        {WinCondition.KillEnemies, typeof(CheckKillEnemiesConditionSystem)}
    };

    private static Dictionary<LoseCondition, Type> loseConditionSystemMap = new Dictionary<LoseCondition, Type>
    {
        {LoseCondition.PlayerDead, typeof(CheckPlayerDeadConditionSystem)}
    };

    public static ISystem GetSystemForWinCondition(WinCondition winCondition, GameContext context)
    {
        Type winConditionCheckClassType;

        if (winConditionSystemMap.TryGetValue(winCondition, out winConditionCheckClassType))
        {
            ISystem system = (ISystem) Activator.CreateInstance(winConditionCheckClassType, context);

            return system;
        }

        return null;
    }

    public static ISystem GetSystemForLoseCondition(LoseCondition loseCondition, GameContext context)
    {
        Type loseConditionCheckClassType;

        if (loseConditionSystemMap.TryGetValue(loseCondition, out loseConditionCheckClassType))
        {
            ISystem system = (ISystem) Activator.CreateInstance(loseConditionCheckClassType, context);

            return system;
        }

        return null;
    }
}