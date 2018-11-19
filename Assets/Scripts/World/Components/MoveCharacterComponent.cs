using Entitas;
using UnityEngine;

[Game]
public class MoveCharacterComponent : IComponent
{
    public int EntityToMoveId;
    public Vector2 MoveDirection;
}