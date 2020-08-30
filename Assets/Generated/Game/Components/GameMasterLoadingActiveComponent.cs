//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity masterLoadingActiveEntity { get { return GetGroup(GameMatcher.MasterLoadingActive).GetSingleEntity(); } }

    public bool isMasterLoadingActive {
        get { return masterLoadingActiveEntity != null; }
        set {
            var entity = masterLoadingActiveEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isMasterLoadingActive = true;
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

    static readonly MasterLoadingActiveComponent masterLoadingActiveComponent = new MasterLoadingActiveComponent();

    public bool isMasterLoadingActive {
        get { return HasComponent(GameComponentsLookup.MasterLoadingActive); }
        set {
            if (value != isMasterLoadingActive) {
                var index = GameComponentsLookup.MasterLoadingActive;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : masterLoadingActiveComponent;

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

    static Entitas.IMatcher<GameEntity> _matcherMasterLoadingActive;

    public static Entitas.IMatcher<GameEntity> MasterLoadingActive {
        get {
            if (_matcherMasterLoadingActive == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MasterLoadingActive);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMasterLoadingActive = matcher;
            }

            return _matcherMasterLoadingActive;
        }
    }
}
