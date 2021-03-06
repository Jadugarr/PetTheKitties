//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GrapplingHookEndPointComponent grapplingHookEndPoint { get { return (GrapplingHookEndPointComponent)GetComponent(GameComponentsLookup.GrapplingHookEndPoint); } }
    public bool hasGrapplingHookEndPoint { get { return HasComponent(GameComponentsLookup.GrapplingHookEndPoint); } }

    public void AddGrapplingHookEndPoint(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.GrapplingHookEndPoint;
        var component = (GrapplingHookEndPointComponent)CreateComponent(index, typeof(GrapplingHookEndPointComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGrapplingHookEndPoint(UnityEngine.Vector2 newValue) {
        var index = GameComponentsLookup.GrapplingHookEndPoint;
        var component = (GrapplingHookEndPointComponent)CreateComponent(index, typeof(GrapplingHookEndPointComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGrapplingHookEndPoint() {
        RemoveComponent(GameComponentsLookup.GrapplingHookEndPoint);
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

    static Entitas.IMatcher<GameEntity> _matcherGrapplingHookEndPoint;

    public static Entitas.IMatcher<GameEntity> GrapplingHookEndPoint {
        get {
            if (_matcherGrapplingHookEndPoint == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GrapplingHookEndPoint);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGrapplingHookEndPoint = matcher;
            }

            return _matcherGrapplingHookEndPoint;
        }
    }
}
