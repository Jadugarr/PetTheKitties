//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity timeSinceLastPauseInputEntity { get { return GetGroup(GameMatcher.TimeSinceLastPauseInput).GetSingleEntity(); } }
    public TimeSinceLastPauseInputComponent timeSinceLastPauseInput { get { return timeSinceLastPauseInputEntity.timeSinceLastPauseInput; } }
    public bool hasTimeSinceLastPauseInput { get { return timeSinceLastPauseInputEntity != null; } }

    public GameEntity SetTimeSinceLastPauseInput(float newValue) {
        if (hasTimeSinceLastPauseInput) {
            throw new Entitas.EntitasException("Could not set TimeSinceLastPauseInput!\n" + this + " already has an entity with TimeSinceLastPauseInputComponent!",
                "You should check if the context already has a timeSinceLastPauseInputEntity before setting it or use context.ReplaceTimeSinceLastPauseInput().");
        }
        var entity = CreateEntity();
        entity.AddTimeSinceLastPauseInput(newValue);
        return entity;
    }

    public void ReplaceTimeSinceLastPauseInput(float newValue) {
        var entity = timeSinceLastPauseInputEntity;
        if (entity == null) {
            entity = SetTimeSinceLastPauseInput(newValue);
        } else {
            entity.ReplaceTimeSinceLastPauseInput(newValue);
        }
    }

    public void RemoveTimeSinceLastPauseInput() {
        timeSinceLastPauseInputEntity.Destroy();
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

    public TimeSinceLastPauseInputComponent timeSinceLastPauseInput { get { return (TimeSinceLastPauseInputComponent)GetComponent(GameComponentsLookup.TimeSinceLastPauseInput); } }
    public bool hasTimeSinceLastPauseInput { get { return HasComponent(GameComponentsLookup.TimeSinceLastPauseInput); } }

    public void AddTimeSinceLastPauseInput(float newValue) {
        var index = GameComponentsLookup.TimeSinceLastPauseInput;
        var component = (TimeSinceLastPauseInputComponent)CreateComponent(index, typeof(TimeSinceLastPauseInputComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceTimeSinceLastPauseInput(float newValue) {
        var index = GameComponentsLookup.TimeSinceLastPauseInput;
        var component = (TimeSinceLastPauseInputComponent)CreateComponent(index, typeof(TimeSinceLastPauseInputComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveTimeSinceLastPauseInput() {
        RemoveComponent(GameComponentsLookup.TimeSinceLastPauseInput);
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

    static Entitas.IMatcher<GameEntity> _matcherTimeSinceLastPauseInput;

    public static Entitas.IMatcher<GameEntity> TimeSinceLastPauseInput {
        get {
            if (_matcherTimeSinceLastPauseInput == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.TimeSinceLastPauseInput);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherTimeSinceLastPauseInput = matcher;
            }

            return _matcherTimeSinceLastPauseInput;
        }
    }
}