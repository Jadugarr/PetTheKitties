using System.Collections.Generic;
using Entitas;
using Entitas.Common;
using Entitas.Scripts.Common.Systems;
using UnityEngine;

public enum SystemsUpdateType
{
    Update,
    FixedUpdate,
    LateUpdate
}

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

    private static void AddActiveSystems(Systems systems, ref List<Systems> listToAddTo)
    {
        if (listToAddTo == null)
        {
            listToAddTo = new List<Systems>();
        }

        if (!listToAddTo.Contains(systems))
        {
            listToAddTo.Add(systems);
        }
        else
        {
            Debug.LogWarning("Tried adding the same systems multiple times!");
        }
    }

    public static void AddActiveSystems(Systems systems, SystemsUpdateType systemsUpdateType = SystemsUpdateType.Update)
    {
        switch (systemsUpdateType)
        {
            case SystemsUpdateType.Update:
                AddActiveSystems(systems, ref systemsToAddToUpdate);
                break;
            case SystemsUpdateType.FixedUpdate:
                AddActiveSystems(systems, ref systemsToAddToFixedUpdate);
                break;
            case SystemsUpdateType.LateUpdate:
                AddActiveSystems(systems, ref systemsToAddToLateUpdate);
                break;
            default:
                Debug.LogError("Could not add active systems, because of an unknown type: " + systemsUpdateType);
                break;
        }
    }

    private static void RemoveActiveSystems(Systems systems, ref List<Systems> listToRemoveSystemsFrom)
    {
        if (listToRemoveSystemsFrom == null)
        {
            listToRemoveSystemsFrom = new List<Systems>();
        }

        if (!listToRemoveSystemsFrom.Contains(systems))
        {
            systems.ClearReactiveSystems();
            systems.DeactivateReactiveSystems();
            systems.TearDown();
            listToRemoveSystemsFrom.Add(systems);
        }
        else
        {
            Debug.LogWarning("Tried removing systems although they're not even active!");
        }
    }

    public static void RemoveActiveSystems(Systems systems, SystemsUpdateType systemsUpdateType = SystemsUpdateType.Update)
    {
        switch (systemsUpdateType)
        {
            case SystemsUpdateType.Update:
                RemoveActiveSystems(systems, ref systemsToRemoveFromUpdate);
                break;
            case SystemsUpdateType.FixedUpdate:
                RemoveActiveSystems(systems, ref systemsToRemoveFromFixedUpdate);
                break;
            case SystemsUpdateType.LateUpdate:
                RemoveActiveSystems(systems, ref systemsToRemoveFromLateUpdate);
                break;
            default:
                Debug.LogError("Could not remove active systems, because of an unknown type: " + systemsUpdateType);
                break;
        }
    }

    public static void RefreshActiveSystems()
    {
        HandleSystemsToAdd(ref systemsToAddToUpdate, ref activeUpdateSystems);
        HandleSystemsToAdd(ref systemsToAddToFixedUpdate, ref activeFixedUpdateSystems);
        HandleSystemsToAdd(ref systemsToAddToLateUpdate, ref activeLateUpdateSystems);
        HandleSystemsToRemove(ref systemsToRemoveFromUpdate, ref activeUpdateSystems);
        HandleSystemsToRemove(ref systemsToRemoveFromFixedUpdate, ref activeFixedUpdateSystems);
        HandleSystemsToRemove(ref systemsToRemoveFromLateUpdate, ref activeLateUpdateSystems);
    }

    private static void HandleSystemsToAdd(ref List<Systems> listOfSystemsToAdd, ref List<Systems> listToAddTo)
    {
        if (listOfSystemsToAdd != null && listOfSystemsToAdd.Count > 0)
        {
            Systems[] currentList = new Systems[listOfSystemsToAdd.Count];
            listOfSystemsToAdd.CopyTo(currentList);
            foreach (Systems systems in currentList)
            {
                systems.ActivateReactiveSystems();
                systems.Initialize();
                listToAddTo.Add(systems);
                listOfSystemsToAdd.Remove(systems);
            }

            if (listOfSystemsToAdd.Count > 0)
            {
                HandleSystemsToAdd(ref listOfSystemsToAdd, ref listToAddTo);
            }
        }
    }

    private static void HandleSystemsToRemove(ref List<Systems> listOfSystemsToRemove, ref List<Systems> listToRemoveFrom)
    {
        if (listOfSystemsToRemove != null)
        {
            foreach (Systems systems in listOfSystemsToRemove)
            {
                listToRemoveFrom.Remove(systems);
            }

            listOfSystemsToRemove.Clear();
            listOfSystemsToRemove = null;
        }
    }

    public static List<Systems> GetActiveSystems()
    {
        return activeUpdateSystems;
    }

    public static List<Systems> GetActiveFixedUpdateSystems()
    {
        return activeFixedUpdateSystems;
    }

    public static List<Systems> GetActiveLateUpdateSystems()
    {
        return activeLateUpdateSystems;
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