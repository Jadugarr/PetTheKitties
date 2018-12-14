using Entitas.CodeGeneration.Attributes;

namespace Entitas.Kitty.Components
{
    /// <summary>
    /// Component, that holds the reference to the entity, that is currently inside the player interaction trigger
    /// </summary>
    [Game, Unique]
    public class PlayerInteractionComponent : IComponent
    {
        public int InteractableEntityId;
    }
}