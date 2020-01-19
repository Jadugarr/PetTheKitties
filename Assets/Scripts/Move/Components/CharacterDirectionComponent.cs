using Entitas;

public enum CharacterDirection
{
    None,
    Forward,
    Backward
}

[Game]
public class CharacterDirectionComponent : IComponent
{
    public CharacterDirection CharacterDirection;
}