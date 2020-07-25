//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity currentSceneEntity { get { return GetGroup(GameMatcher.CurrentScene).GetSingleEntity(); } }
    public CurrentSceneComponent currentScene { get { return currentSceneEntity.currentScene; } }
    public bool hasCurrentScene { get { return currentSceneEntity != null; } }

    public GameEntity SetCurrentScene(string newValue) {
        if (hasCurrentScene) {
            throw new Entitas.EntitasException("Could not set CurrentScene!\n" + this + " already has an entity with CurrentSceneComponent!",
                "You should check if the context already has a currentSceneEntity before setting it or use context.ReplaceCurrentScene().");
        }
        var entity = CreateEntity();
        entity.AddCurrentScene(newValue);
        return entity;
    }

    public void ReplaceCurrentScene(string newValue) {
        var entity = currentSceneEntity;
        if (entity == null) {
            entity = SetCurrentScene(newValue);
        } else {
            entity.ReplaceCurrentScene(newValue);
        }
    }

    public void RemoveCurrentScene() {
        currentSceneEntity.Destroy();
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

    public CurrentSceneComponent currentScene { get { return (CurrentSceneComponent)GetComponent(GameComponentsLookup.CurrentScene); } }
    public bool hasCurrentScene { get { return HasComponent(GameComponentsLookup.CurrentScene); } }

    public void AddCurrentScene(string newValue) {
        var index = GameComponentsLookup.CurrentScene;
        var component = (CurrentSceneComponent)CreateComponent(index, typeof(CurrentSceneComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceCurrentScene(string newValue) {
        var index = GameComponentsLookup.CurrentScene;
        var component = (CurrentSceneComponent)CreateComponent(index, typeof(CurrentSceneComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveCurrentScene() {
        RemoveComponent(GameComponentsLookup.CurrentScene);
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

    static Entitas.IMatcher<GameEntity> _matcherCurrentScene;

    public static Entitas.IMatcher<GameEntity> CurrentScene {
        get {
            if (_matcherCurrentScene == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CurrentScene);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCurrentScene = matcher;
            }

            return _matcherCurrentScene;
        }
    }
}