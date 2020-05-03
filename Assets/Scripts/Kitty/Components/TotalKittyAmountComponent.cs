using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class TotalKittyAmountComponent : IComponent
{
    public int TotalKittyAmount;
}