using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class UpAndRunningSystems {
    public static List<RocketSystem> InGameSystems = new();

    public static void RegisterSystem(RocketSystem system) {
        Debug.Log($"Registering system in persistent data: {system.GetSystemName()}");
        if (!InGameSystems.Contains(system)) {
            InGameSystems.Add(system);
        }
    }

    public static void ResetSystems() {
        Debug.Log("Resetting systems in persistent data");
        InGameSystems.Clear();
    }

    public static List<RocketSystem> GetPermanentlyBrokenSystemsByPriority() {
        List<RocketSystem> brokenSystems = new();
        foreach (var system in InGameSystems) {
            if (system.IsPermanentlyBroken) {
                brokenSystems.Add(system);
            }
        }
        return brokenSystems.OrderBy(system => system.Priority).ToList();
    }
}
