using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public class InputSystem : GameExecuteSystem, ICleanupSystem
{
    protected override IList<SubState> ValidSubStates => new List<SubState>(1) {SubState.Undefined};

    protected override IList<GameState> ValidGameStates => new List<GameState>(1) {GameState.Undefined};

    private IGroup<GameEntity> inputComponents;

    public InputSystem(GameContext context) : base(context)
    {
        inputComponents = _context.GetGroup(GameMatcher.Input);
    }

    protected override void ExecuteSystem()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if (Input.anyKeyDown)
        {
            foreach (string currentAxis in InputAxis.AxisList)
            {
                float axisValue = Input.GetAxis(currentAxis);

                if (axisValue != 0)
                {
                    InputCommand commandToExecute =
                        InputConfiguration.GetCommandByAxisName(currentAxis);
                    if (commandToExecute != InputCommand.Undefined)
                    {
                        GameEntity inputEntity = _context.CreateEntity();
                        inputEntity.AddInput(commandToExecute, axisValue);
                    }
                }
            }
        }
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in inputComponents.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}