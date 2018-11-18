using System.Collections.Generic;
using Entitas;
using UnityEngine;

public static class GameSystemService
{
    private static List<Systems> activeSystems = new List<Systems>();
    private static List<Systems> systemsToAdd;
    private static List<Systems> systemsToRemove;

    private static Dictionary<GameState, Systems> stateSystemMap = new Dictionary<GameState, Systems>();
    private static Dictionary<SubState, Systems> subStateSystemMap = new Dictionary<SubState, Systems>();

    public static void AddActiveSystems(Systems systems)
    {
        if (systemsToAdd == null)
        {
            systemsToAdd = new List<Systems>();
        }

        if (!systemsToAdd.Contains(systems))
        {
            systemsToAdd.Add(systems);
        }
        else
        {
            Debug.LogWarning("Tried adding the same systems multiple times!");
        }
    }

    public static void RemoveActiveSystems(Systems systems)
    {
        if (systemsToRemove == null)
        {
            systemsToRemove = new List<Systems>();
        }

        if (!systemsToRemove.Contains(systems))
        {
            systems.ClearReactiveSystems();
            systems.DeactivateReactiveSystems();
            systems.TearDown();
            systemsToRemove.Add(systems);
        }
        else
        {
            Debug.LogWarning("Tried removing systems although they're not even active!");
        }
    }

    public static void RefreshActiveSystems()
    {
        HandleSystemsToAdd();
        HandleSystemsToRemove();
    }

    private static void HandleSystemsToAdd()
    {
        if (systemsToAdd != null && systemsToAdd.Count > 0)
        {
            Systems[] currentList = new Systems[systemsToAdd.Count];
            systemsToAdd.CopyTo(currentList);
            foreach (Systems systems in currentList)
            {
                systems.ActivateReactiveSystems();
                systems.Initialize();
                activeSystems.Add(systems);
                systemsToAdd.Remove(systems);
            }

            if (systemsToAdd.Count > 0)
            {
                HandleSystemsToAdd();
            }
        }
    }

    private static void HandleSystemsToRemove()
    {
        if (systemsToRemove != null)
        {
            foreach (Systems systems in systemsToRemove)
            {
                activeSystems.Remove(systems);
            }

            systemsToRemove.Clear();
            systemsToRemove = null;
        }
    }

    public static List<Systems> GetActiveSystems()
    {
        return activeSystems;
    }

    public static void AddSystemMapping(GameState state, Systems systems)
    {
        if (!stateSystemMap.ContainsKey(state))
        {
            stateSystemMap.Add(state, systems);
        }
        else
        {
            Debug.LogWarning("System map already contains systems for GameState: " + state);
        }
    }

    public static void RemoveSystemMapping(GameState state)
    {
        if (stateSystemMap.ContainsKey(state))
        {
            stateSystemMap.Remove(state);
        }
        else
        {
            Debug.LogWarning("System map was never added or has already been removed for GameState: " + state);
        }
    }

    public static void AddSubSystemMapping(SubState subState, Systems systems)
    {
        if (!subStateSystemMap.ContainsKey(subState))
        {
            systems.DeactivateReactiveSystems();
            subStateSystemMap.Add(subState, systems);
        }
        else
        {
            Debug.LogWarning("Subsystem map already contains system for SubState: " + subState);
        }
    }

    public static void RemoveSubSystemMapping(SubState subState)
    {
        if (subStateSystemMap.ContainsKey(subState))
        {
            subStateSystemMap.Remove(subState);
        }
        else
        {
            Debug.LogWarning("Subsystem map was never added or has already been removed for SubState: " + subState);
        }
    }

    public static Systems GetSystemMapping(GameState state)
    {
        Systems returnValue;

        if (stateSystemMap.TryGetValue(state, out returnValue))
        {
            return returnValue;
        }
        else
        {
            return null;
        }
    }

    public static Systems GetSubSystemMapping(SubState subState)
    {
        Systems returnValue;

        if (subStateSystemMap.TryGetValue(subState, out returnValue))
        {
            return returnValue;
        }
        else
        {
            return null;
        }
    }

    public static bool HasSystemMapping(GameState state)
    {
        return stateSystemMap.ContainsKey(state);
    }

    public static bool HasSubSystemMapping(SubState state)
    {
        return subStateSystemMap.ContainsKey(state);
    }
}