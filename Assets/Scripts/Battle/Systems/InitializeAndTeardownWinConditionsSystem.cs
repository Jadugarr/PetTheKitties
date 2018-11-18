using Entitas;
using UnityEngine;

public class InitializeAndTeardownWinConditionsSystem : IInitializeSystem, ITearDownSystem
{
    private GameContext context;
    private Feature winConditionSystems;

    public InitializeAndTeardownWinConditionsSystem(GameContext context)
    {
        this.context = context;
    }

    public void Initialize()
    {
        if (context.hasWinCondition)
        {
            if (winConditionSystems == null)
            {
                winConditionSystems = new Feature("WinConditionSystems");

                foreach (WinConditionState currentWinCondition in context.winCondition.WinConditions)
                {
                    winConditionSystems.Add(
                        WinLoseConditionConfiguration.GetSystemForWinCondition(currentWinCondition.WinCondition, context));
                }
            }

            GameSystemService.AddActiveSystems(winConditionSystems);
        }
        else
        {
            Debug.LogError("The context has no win condition entity yet!");
        }
    }

    public void TearDown()
    {
        if (winConditionSystems != null)
        {
            GameSystemService.RemoveActiveSystems(winConditionSystems);
        }
    }
}