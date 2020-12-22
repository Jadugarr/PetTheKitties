//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity toggleGrappleInputAvailableEntity { get { return GetGroup(GameMatcher.ToggleGrappleInputAvailable).GetSingleEntity(); } }

    public bool isToggleGrappleInputAvailable {
        get { return toggleGrappleInputAvailableEntity != null; }
        set {
            var entity = toggleGrappleInputAvailableEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isToggleGrappleInputAvailable = true;
                } else {
                    entity.Destroy();
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly ToggleGrappleInputAvailable toggleGrappleInputAvailableComponent = new ToggleGrappleInputAvailable();

    public bool isToggleGrappleInputAvailable {
        get { return HasComponent(GameComponentsLookup.ToggleGrappleInputAvailable); }
        set {
            if (value != isToggleGrappleInputAvailable) {
                var index = GameComponentsLookup.ToggleGrappleInputAvailable;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : toggleGrappleInputAvailableComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherToggleGrappleInputAvailable;

    public static Entitas.IMatcher<GameEntity> ToggleGrappleInputAvailable {
        get {
            if (_matcherToggleGrappleInputAvailable == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.ToggleGrappleInputAvailable);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherToggleGrappleInputAvailable = matcher;
            }

            return _matcherToggleGrappleInputAvailable;
        }
    }
}
