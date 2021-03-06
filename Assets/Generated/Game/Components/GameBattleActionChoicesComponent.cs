//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public BattleActionChoicesComponent battleActionChoices { get { return (BattleActionChoicesComponent)GetComponent(GameComponentsLookup.BattleActionChoices); } }
    public bool hasBattleActionChoices { get { return HasComponent(GameComponentsLookup.BattleActionChoices); } }

    public void AddBattleActionChoices(System.Collections.Generic.List<BattleActionChoice> newBattleActionChoices) {
        var index = GameComponentsLookup.BattleActionChoices;
        var component = (BattleActionChoicesComponent)CreateComponent(index, typeof(BattleActionChoicesComponent));
        component.BattleActionChoices = newBattleActionChoices;
        AddComponent(index, component);
    }

    public void ReplaceBattleActionChoices(System.Collections.Generic.List<BattleActionChoice> newBattleActionChoices) {
        var index = GameComponentsLookup.BattleActionChoices;
        var component = (BattleActionChoicesComponent)CreateComponent(index, typeof(BattleActionChoicesComponent));
        component.BattleActionChoices = newBattleActionChoices;
        ReplaceComponent(index, component);
    }

    public void RemoveBattleActionChoices() {
        RemoveComponent(GameComponentsLookup.BattleActionChoices);
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

    static Entitas.IMatcher<GameEntity> _matcherBattleActionChoices;

    public static Entitas.IMatcher<GameEntity> BattleActionChoices {
        get {
            if (_matcherBattleActionChoices == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.BattleActionChoices);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherBattleActionChoices = matcher;
            }

            return _matcherBattleActionChoices;
        }
    }
}
