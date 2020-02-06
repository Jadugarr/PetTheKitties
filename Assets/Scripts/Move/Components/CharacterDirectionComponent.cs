using Entitas;

public enum CharacterDirection
{
    None = 0,
    Forward = 1,
    Backward = -1
}

[Game]
public class CharacterDirectionComponent : IComponent
{
    public CharacterDirection CharacterDirection;
}