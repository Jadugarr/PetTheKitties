//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public RestartControllerComponent restartController { get { return (RestartControllerComponent)GetComponent(GameComponentsLookup.RestartController); } }
    public bool hasRestartController { get { return HasComponent(GameComponentsLookup.RestartController); } }

    public void AddRestartController(GameControllerType newValue) {
        var index = GameComponentsLookup.RestartController;
        var component = (RestartControllerComponent)CreateComponent(index, typeof(RestartControllerComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceRestartController(GameControllerType newValue) {
        var index = GameComponentsLookup.RestartController;
        var component = (RestartControllerComponent)CreateComponent(index, typeof(RestartControllerComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveRestartController() {
        RemoveComponent(GameComponentsLookup.RestartController);
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

    static Entitas.IMatcher<GameEntity> _matcherRestartController;

    public static Entitas.IMatcher<GameEntity> RestartController {
        get {
            if (_matcherRestartController == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.RestartController);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherRestartController = matcher;
            }

            return _matcherRestartController;
        }
    }
}
