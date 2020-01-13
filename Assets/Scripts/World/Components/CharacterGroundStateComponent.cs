using Entitas;
using Entitas.World;
using UnityEngine;

[Game]
public class CharacterGroundStateComponent : IComponent
{
    public CharacterGroundState CharacterGroundState;
    public Vector2 GroundNormal;
    public float DistanceToGround;
}