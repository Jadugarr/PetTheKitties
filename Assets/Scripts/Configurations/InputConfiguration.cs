using System.Collections.Generic;
using UnityEngine;

public static class InputConfiguration
{
    // "string" is the name of the defined axis
    private static Dictionary<GameState, Dictionary<string, InputCommand>> GameStateInputMaps;

    private static Dictionary<SubState, Dictionary<string, InputCommand>> SubStateInputMaps;

    //The defined inputs of sub states override game state inputs
    private static Dictionary<string, InputCommand> activeGameStateInputMap;

    private static Dictionary<string, InputCommand> activeSubStateInputMap;

    static InputConfiguration()
    {
        GameStateInputMaps = new Dictionary<GameState, Dictionary<string, InputCommand>>()
        {
            {
                GameState.Battle, new Dictionary<string, InputCommand>()
                {
                    {InputAxis.Pause, InputCommand.Pause}
                }
            },
            {
                GameState.MainMenu, new Dictionary<string, InputCommand>()
                {
                    {InputAxis.Cancel, InputCommand.ExitMenu}
                }
            }
        };

        SubStateInputMaps = new Dictionary<SubState, Dictionary<string, InputCommand>>()
        {
            {
                SubState.Paused, new Dictionary<string, InputCommand>()
                {
                    {InputAxis.Pause, InputCommand.Unpause}
                }
            },
            {
                SubState.ChooseAction, new Dictionary<string, InputCommand>()
                {
                    {InputAxis.Cancel, InputCommand.CancelAction},
                    {InputAxis.Pause, InputCommand.Pause}
                }
            }
        };
    }

    public static InputCommand GetCommandByAxisName(string axisName)
    {
        var returnValue = InputCommand.Undefined;

        if (activeSubStateInputMap != null)
        {
            if (activeSubStateInputMap.TryGetValue(axisName, out returnValue))
            {
                return returnValue;
            }

            return InputCommand.Undefined;
        }

        if (activeGameStateInputMap != null)
        {
            if (activeGameStateInputMap.TryGetValue(axisName, out returnValue))
            {
                return returnValue;
            }

            return InputCommand.Undefined;
        }

        //Debug.LogWarning("No active input found for axis: " + axisName);
        return returnValue;
    }

    public static void ChangeActiveGameStateInputMap(GameState state)
    {
        Dictionary<string, InputCommand> newMap;

        if (GameStateInputMaps.TryGetValue(state, out newMap))
        {
            activeGameStateInputMap = newMap;
            //Debug.Log("Switched active input map for game state: " + state);
        }
        else
        {
            activeGameStateInputMap = null;
            //Debug.LogWarning("No input map defined for game state: " + state);
        }
    }

    public static void ChangeActiveSubStateInputMap(SubState state)
    {
        Dictionary<string, InputCommand> newMap;

        if (SubStateInputMaps.TryGetValue(state, out newMap))
        {
            activeSubStateInputMap = newMap;
            //Debug.Log("Switched active sub state input map for state: " + state);
        }
        else
        {
            activeSubStateInputMap = null;
            //Debug.LogWarning("No input map defined for sub state: " + state);
        }
    }
}