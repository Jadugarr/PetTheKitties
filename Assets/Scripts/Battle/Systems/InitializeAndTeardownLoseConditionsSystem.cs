using Entitas;
using UnityEngine;

public class InitializeAndTeardownLoseConditionsSystem : IInitializeSystem, ITearDownSystem
{
    private GameContext context;
    private Feature loseConditionSystems;

    public InitializeAndTeardownLoseConditionsSystem(GameContext context)
    {
        this.context = context;
    }

    public void Initialize()
    {
        if (context.hasLoseCondition)
        {
            if (loseConditionSystems == null)
            {
                loseConditionSystems = new Feature("LoseConditionSystems");

                foreach (LoseConditionState currentLoseCondition in context.loseCondition.LoseConditions)
                {
                    loseConditionSystems.Add(
                        WinLoseConditionConfiguration.GetSystemForLoseCondition(currentLoseCondition.LoseCondition, context));
                }
            }

            GameSystemService.AddActiveSystems(loseConditionSystems);
        }
        else
        {
            Debug.LogError("The context has no lose condition entity yet!");
        }
    }

    public void TearDown()
    {
        if (loseConditionSystems != null)
        {
            GameSystemService.RemoveActiveSystems(loseConditionSystems);
        }
    }
}