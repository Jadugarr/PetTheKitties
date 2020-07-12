//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using UnityEngine;

public partial class GameEntity {

    public CharacterVelocityComponent characterVelocity { get { return (CharacterVelocityComponent)GetComponent(GameComponentsLookup.CharacterVelocity); } }
    public bool hasCharacterVelocity { get { return HasComponent(GameComponentsLookup.CharacterVelocity); } }

    public void AddCharacterVelocity(UnityEngine.Vector2 newVelocity) {
        var index = GameComponentsLookup.CharacterVelocity;
        var component = (CharacterVelocityComponent)CreateComponent(index, typeof(CharacterVelocityComponent));
        component.Velocity = newVelocity;
        AddComponent(index, component);
    }

    public void ReplaceCharacterVelocity(UnityEngine.Vector2 newVelocity) {
        var index = GameComponentsLookup.CharacterVelocity;
        var component = (CharacterVelocityComponent)CreateComponent(index, typeof(CharacterVelocityComponent));
        component.Velocity = newVelocity;
        ReplaceComponent(index, component);
    }

    public void RemoveCharacterVelocity() {
        RemoveComponent(GameComponentsLookup.CharacterVelocity);
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

    static Entitas.IMatcher<GameEntity> _matcherCharacterVelocity;

    public static Entitas.IMatcher<GameEntity> CharacterVelocity {
        get {
            if (_matcherCharacterVelocity == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.CharacterVelocity);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherCharacterVelocity = matcher;
            }

            return _matcherCharacterVelocity;
        }
    }
}
