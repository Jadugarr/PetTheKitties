//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public CharacterDirectionComponent characterDirection { get { return (CharacterDirectionComponent)GetComponent(GameComponentsLookup.CharacterDirection); } }
    public bool hasCharacterDirection { get { return HasComponent(GameComponentsLookup.CharacterDirection); } }

    public void AddCharacterDirection(CharacterDirection newCharacterDirection) {
        var index = GameComponentsLookup.CharacterDirection;
        var component = (CharacterDirectionComponent)CreateComponent(index, typeof(CharacterDirectionComponent));
        component.CharacterDirection = newCharacterDirection;
        AddComponent(index, component);
    }

    public void ReplaceCharacterDirection(CharacterDirection newCharacterDirection) {
        var index = GameComponentsLookup.CharacterDirection;
        var component = (CharacterDirectionComponent)CreateComponent(index, typeof(CharacterDirectionComponent));
        component.CharacterDirection = newCharacterDirection;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterDirection() {
        RemoveComponent(GameComponentsLookup.CharacterDirection);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterDirection;

    public static Entitas.IMatcher<GameEntity> CharacterDirection {
        get {
            if (_matcherCharacterDirection == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterDirection);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterDirection = matcher;
            }

            return _matcherCharacterDirection;
        }
    }
}
