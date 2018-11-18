using System;

namespace Entitas.Actions
{
    [Flags]
    public enum TargetType
    {
        None = 0,
        Self = 1,
        Allies = 2,
        Enemies = 4
    }
}