
using Entitas;
using Entitas.CodeGeneration.Attributes;

[Game, Unique]
public class KittyAmountDisplayComponent : IComponent
{
    public ValueDisplayWidget KittyAmountDisplayWidget;
}