//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GrapplingHookStartingPointComponent grapplingHookStartingPoint { get { return (GrapplingHookStartingPointComponent)GetComponent(GameComponentsLookup.GrapplingHookStartingPoint); } }
    public bool hasGrapplingHookStartingPoint { get { return HasComponent(GameComponentsLookup.GrapplingHookStartingPoint); } }

    public void AddGrapplingHookStartingPoint(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.GrapplingHookStartingPoint;
        var component = (GrapplingHookStartingPointComponent)CreateComponent(index, typeof(GrapplingHookStartingPointComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGrapplingHookStartingPoint(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.GrapplingHookStartingPoint;
        var component = (GrapplingHookStartingPointComponent)CreateComponent(index, typeof(GrapplingHookStartingPointComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGrapplingHookStartingPoint() {
        RemoveComponent(GameComponentsLookup.GrapplingHookStartingPoint);
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

    static Entitas.IMatcher<GameEntity> _matcherGrapplingHookStartingPoint;

    public static Entitas.IMatcher<GameEntity> GrapplingHookStartingPoint {
        get {
            if (_matcherGrapplingHookStartingPoint == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GrapplingHookStartingPoint);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGrapplingHookStartingPoint = matcher;
            }

            return _matcherGrapplingHookStartingPoint;
        }
    }
}
