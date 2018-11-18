using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Unique]
public class EnemySpawnCooldownComponent : IComponent
{
    public float cooldown;
}