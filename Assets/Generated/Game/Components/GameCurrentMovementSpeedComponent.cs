//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CurrentMovementSpeedComponent currentMovementSpeed { get { return (CurrentMovementSpeedComponent)GetComponent(GameComponentsLookup.CurrentMovementSpeed); } }
    public bool hasCurrentMovementSpeed { get { return HasComponent(GameComponentsLookup.CurrentMovementSpeed); } }

    public void AddCurrentMovementSpeed(float newCurrentMovementSpeed) {
        var index = GameComponentsLookup.CurrentMovementSpeed;
        var component = (CurrentMovementSpeedComponent)CreateComponent(index, typeof(CurrentMovementSpeedComponent));
        component.CurrentMovementSpeed = newCurrentMovementSpeed;
        AddComponent(index, component);
    }

    public void ReplaceCurrentMovementSpeed(float newCurrentMovementSpeed) {
        var index = GameComponentsLookup.CurrentMovementSpeed;
        var component = (CurrentMovementSpeedComponent)CreateComponent(index, typeof(CurrentMovementSpeedComponent));
        component.CurrentMovementSpeed = newCurrentMovementSpeed;
        ReplaceComponent(index, component);
    }

    public void RemoveCurrentMovementSpeed() {
        RemoveComponent(GameComponentsLookup.CurrentMovementSpeed);
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

    static Entitas.IMatcher<GameEntity> _matcherCurrentMovementSpeed;

    public static Entitas.IMatcher<GameEntity> CurrentMovementSpeed {
        get {
            if (_matcherCurrentMovementSpeed == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CurrentMovementSpeed);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCurrentMovementSpeed = matcher;
            }

            return _matcherCurrentMovementSpeed;
        }
    }
}
