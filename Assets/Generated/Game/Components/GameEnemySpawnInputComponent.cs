//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly EnemySpawnInputComponent enemySpawnInputComponent = new EnemySpawnInputComponent();

    public bool isEnemySpawnInput {
        get { return HasComponent(GameComponentsLookup.EnemySpawnInput); }
        set {
            if (value != isEnemySpawnInput) {
                var index = GameComponentsLookup.EnemySpawnInput;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : enemySpawnInputComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherEnemySpawnInput;

    public static Entitas.IMatcher<GameEntity> EnemySpawnInput {
        get {
            if (_matcherEnemySpawnInput == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.EnemySpawnInput);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherEnemySpawnInput = matcher;
            }

            return _matcherEnemySpawnInput;
        }
    }
}
