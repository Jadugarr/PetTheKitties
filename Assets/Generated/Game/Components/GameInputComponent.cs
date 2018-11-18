//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public InputComponent input { get { return (InputComponent)GetComponent(GameComponentsLookup.Input); } }
    public bool hasInput { get { return HasComponent(GameComponentsLookup.Input); } }

    public void AddInput(InputCommand newInputCommand, float newInputValue) {
        var index = GameComponentsLookup.Input;
        var component = (InputComponent)CreateComponent(index, typeof(InputComponent));
        component.InputCommand = newInputCommand;
        component.InputValue = newInputValue;
        AddComponent(index, component);
    }

    public void ReplaceInput(InputCommand newInputCommand, float newInputValue) {
        var index = GameComponentsLookup.Input;
        var component = (InputComponent)CreateComponent(index, typeof(InputComponent));
        component.InputCommand = newInputCommand;
        component.InputValue = newInputValue;
        ReplaceComponent(index, component);
    }

    public void RemoveInput() {
        RemoveComponent(GameComponentsLookup.Input);
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

    static Entitas.IMatcher<GameEntity> _matcherInput;

    public static Entitas.IMatcher<GameEntity> Input {
        get {
            if (_matcherInput == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Input);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherInput = matcher;
            }

            return _matcherInput;
        }
    }
}
