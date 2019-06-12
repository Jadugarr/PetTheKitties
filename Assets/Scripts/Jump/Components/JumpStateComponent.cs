using Entitas;

public enum JumpState
{
    Grounded,
    Jumping,
    JumpEnding,
    Falling
}

[Game]
public class JumpStateComponent : IComponent
{
    public JumpState JumpState;
}