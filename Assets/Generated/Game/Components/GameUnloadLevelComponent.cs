//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity unloadLevelEntity { get { return GetGroup(GameMatcher.UnloadLevel).GetSingleEntity(); } }

    public bool isUnloadLevel {
        get { return unloadLevelEntity != null; }
        set {
            var entity = unloadLevelEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isUnloadLevel = true;
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

    static readonly UnloadLevelComponent unloadLevelComponent = new UnloadLevelComponent();

    public bool isUnloadLevel {
        get { return HasComponent(GameComponentsLookup.UnloadLevel); }
        set {
            if (value != isUnloadLevel) {
                var index = GameComponentsLookup.UnloadLevel;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : unloadLevelComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherUnloadLevel;

    public static Entitas.IMatcher<GameEntity> UnloadLevel {
        get {
            if (_matcherUnloadLevel == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.UnloadLevel);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherUnloadLevel = matcher;
            }

            return _matcherUnloadLevel;
        }
    }
}
