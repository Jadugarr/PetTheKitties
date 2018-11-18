using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game]
[Unique]
public class GameStateComponent : IComponent
{
    public GameState PreviousGameState;
    public GameState CurrentGameState;
}