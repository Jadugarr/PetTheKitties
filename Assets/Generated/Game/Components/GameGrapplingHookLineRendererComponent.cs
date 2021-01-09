//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public GrapplingHookLineRendererComponent grapplingHookLineRenderer { get { return (GrapplingHookLineRendererComponent)GetComponent(GameComponentsLookup.GrapplingHookLineRenderer); } }
    public bool hasGrapplingHookLineRenderer { get { return HasComponent(GameComponentsLookup.GrapplingHookLineRenderer); } }

    public void AddGrapplingHookLineRenderer(UnityEngine.LineRenderer newValue) {
        var index = GameComponentsLookup.GrapplingHookLineRenderer;
        var component = (GrapplingHookLineRendererComponent)CreateComponent(index, typeof(GrapplingHookLineRendererComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceGrapplingHookLineRenderer(UnityEngine.LineRenderer newValue) {
        var index = GameComponentsLookup.GrapplingHookLineRenderer;
        var component = (GrapplingHookLineRendererComponent)CreateComponent(index, typeof(GrapplingHookLineRendererComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveGrapplingHookLineRenderer() {
        RemoveComponent(GameComponentsLookup.GrapplingHookLineRenderer);
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

    static Entitas.IMatcher<GameEntity> _matcherGrapplingHookLineRenderer;

    public static Entitas.IMatcher<GameEntity> GrapplingHookLineRenderer {
        get {
            if (_matcherGrapplingHookLineRenderer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.GrapplingHookLineRenderer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherGrapplingHookLineRenderer = matcher;
            }

            return _matcherGrapplingHookLineRenderer;
        }
    }
}