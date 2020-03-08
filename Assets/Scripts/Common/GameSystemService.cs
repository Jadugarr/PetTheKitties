using System.Collections.Generic;
using Entitas;
using Entitas.Common;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public static class GameSystemService
{
    private static List<Systems> activeUpdateSystems = new List<Systems>();
    private static List<Systems> activeFixedUpdateSystems = new List<Systems>();
    private static List<Systems> activeLateUpdateSystems = new List<Systems>();
    private static List<Systems> systemsToAddToUpdate;
    private static List<Systems> systemsToAddToFixedUpdate;
    private static List<Systems> systemsToAddToLateUpdate;
    private static List<Systems> systemsToRemoveFromUpdate;
    private static List<Systems> systemsToRemoveFromFixedUpdate;
    private static List<Systems> systemsToRemoveFromLateUpdate;

    private static Dictionary<GameSystemType, Systems> systemTypeMap = new Dictionary<GameSystemType, Systems>();

    public static void AddActiveSystems(Systems systems)
    {
        if (systemsToAddToUpdate == null)
        {
            systemsToAddToUpdate = new List<Systems>();
        }

        if (!systemsToAddToUpdate.Contains(systems))
        {
            systemsToAddToUpdate.Add(systems);
        }
        else
        {
            Debug.LogWarning("Tried adding the same systems multiple times!");
        }
    }

    public static void RemoveActiveSystems(Systems systems)
    {
        if (systemsToRemoveFromUpdate == null)
        {
            systemsToRemoveFromUpdate = new List<Systems>();
        }

        if (!systemsToRemoveFromUpdate.Contains(systems))
        {
            systems.ClearReactiveSystems();
            systems.DeactivateReactiveSystems();
            systems.TearDown();
            systemsToRemoveFromUpdate.Add(systems);
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
        if (systemsToAddToUpdate != null && systemsToAddToUpdate.Count > 0)
        {
            Systems[] currentList = new Systems[systemsToAddToUpdate.Count];
            systemsToAddToUpdate.CopyTo(currentList);
            foreach (Systems systems in currentList)
            {
                systems.ActivateReactiveSystems();
                systems.Initialize();
                activeUpdateSystems.Add(systems);
                systemsToAddToUpdate.Remove(systems);
            }

            if (systemsToAddToUpdate.Count > 0)
            {
                HandleSystemsToAdd();
            }
        }
    }

    private static void HandleSystemsToRemove()
    {
        if (systemsToRemoveFromUpdate != null)
        {
            foreach (Systems systems in systemsToRemoveFromUpdate)
            {
                activeUpdateSystems.Remove(systems);
            }

            systemsToRemoveFromUpdate.Clear();
            systemsToRemoveFromUpdate = null;
        }
    }

    public static List<Systems> GetActiveSystems()
    {
        return activeUpdateSystems;
    }

    public static void AddSystemMapping(GameSystemType systemType, Systems systems)
    {
        if (!systemTypeMap.ContainsKey(systemType))
        {
            systems.DeactivateReactiveSystems();
            systemTypeMap.Add(systemType, systems);
        }
        else
        {
            Debug.LogWarning("System map already contains systems for GameState: " + systemType);
        }
    }

    public static Systems GetSystemMapping(GameSystemType systemType)
    {
        Systems returnValue;

        if (systemTypeMap.TryGetValue(systemType, out returnValue))
        {
            return returnValue;
        }
        else
        {
            return null;
        }
    }

    public static bool HasSystemMapping(GameSystemType systemType)
    {
        return systemTypeMap.ContainsKey(systemType);
    }
}