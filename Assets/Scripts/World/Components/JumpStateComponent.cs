using Entitas;

public enum JumpState
{
    Grounded,
    Jumping,
    Falling
}

[Game]
public class JumpStateComponent : IComponent
{
    public JumpState JumpState;
}