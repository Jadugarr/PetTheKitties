//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CharacterGroundStateComponent characterGroundState { get { return (CharacterGroundStateComponent)GetComponent(GameComponentsLookup.CharacterGroundState); } }
    public bool hasCharacterGroundState { get { return HasComponent(GameComponentsLookup.CharacterGroundState); } }

    public void AddCharacterGroundState(Entitas.World.CharacterGroundState newCharacterGroundState) {
        var index = GameComponentsLookup.CharacterGroundState;
        var component = (CharacterGroundStateComponent)CreateComponent(index, typeof(CharacterGroundStateComponent));
        component.CharacterGroundState = newCharacterGroundState;
        AddComponent(index, component);
    }

    public void ReplaceCharacterGroundState(Entitas.World.CharacterGroundState newCharacterGroundState) {
        var index = GameComponentsLookup.CharacterGroundState;
        var component = (CharacterGroundStateComponent)CreateComponent(index, typeof(CharacterGroundStateComponent));
        component.CharacterGroundState = newCharacterGroundState;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterGroundState() {
        RemoveComponent(GameComponentsLookup.CharacterGroundState);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterGroundState;

    public static Entitas.IMatcher<GameEntity> CharacterGroundState {
        get {
            if (_matcherCharacterGroundState == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterGroundState);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterGroundState = matcher;
            }

            return _matcherCharacterGroundState;
        }
    }
}
