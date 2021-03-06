//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity winConditionsFulfilledEntity { get { return GetGroup(GameMatcher.WinConditionsFulfilled).GetSingleEntity(); } }

    public bool isWinConditionsFulfilled {
        get { return winConditionsFulfilledEntity != null; }
        set {
            var entity = winConditionsFulfilledEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isWinConditionsFulfilled = true;
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

    static readonly WinConditionsFulfilledComponent winConditionsFulfilledComponent = new WinConditionsFulfilledComponent();

    public bool isWinConditionsFulfilled {
        get { return HasComponent(GameComponentsLookup.WinConditionsFulfilled); }
        set {
            if (value != isWinConditionsFulfilled) {
                var index = GameComponentsLookup.WinConditionsFulfilled;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : winConditionsFulfilledComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherWinConditionsFulfilled;

    public static Entitas.IMatcher<GameEntity> WinConditionsFulfilled {
        get {
            if (_matcherWinConditionsFulfilled == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WinConditionsFulfilled);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWinConditionsFulfilled = matcher;
            }

            return _matcherWinConditionsFulfilled;
        }
    }
}
