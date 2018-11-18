//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public SceneLoadedComponent sceneLoaded { get { return (SceneLoadedComponent)GetComponent(GameComponentsLookup.SceneLoaded); } }
    public bool hasSceneLoaded { get { return HasComponent(GameComponentsLookup.SceneLoaded); } }

    public void AddSceneLoaded(string newLoadedSceneName) {
        var index = GameComponentsLookup.SceneLoaded;
        var component = (SceneLoadedComponent)CreateComponent(index, typeof(SceneLoadedComponent));
        component.LoadedSceneName = newLoadedSceneName;
        AddComponent(index, component);
    }

    public void ReplaceSceneLoaded(string newLoadedSceneName) {
        var index = GameComponentsLookup.SceneLoaded;
        var component = (SceneLoadedComponent)CreateComponent(index, typeof(SceneLoadedComponent));
        component.LoadedSceneName = newLoadedSceneName;
        ReplaceComponent(index, component);
    }

    public void RemoveSceneLoaded() {
        RemoveComponent(GameComponentsLookup.SceneLoaded);
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

    static Entitas.IMatcher<GameEntity> _matcherSceneLoaded;

    public static Entitas.IMatcher<GameEntity> SceneLoaded {
        get {
            if (_matcherSceneLoaded == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.SceneLoaded);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherSceneLoaded = matcher;
            }

            return _matcherSceneLoaded;
        }
    }
}