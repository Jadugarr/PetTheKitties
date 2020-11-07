//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public ParticleComponent particle { get { return (ParticleComponent)GetComponent(GameComponentsLookup.Particle); } }
    public bool hasParticle { get { return HasComponent(GameComponentsLookup.Particle); } }

    public void AddParticle(UnityEngine.ParticleSystem newValue) {
        var index = GameComponentsLookup.Particle;
        var component = (ParticleComponent)CreateComponent(index, typeof(ParticleComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceParticle(UnityEngine.ParticleSystem newValue) {
        var index = GameComponentsLookup.Particle;
        var component = (ParticleComponent)CreateComponent(index, typeof(ParticleComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveParticle() {
        RemoveComponent(GameComponentsLookup.Particle);
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

    static Entitas.IMatcher<GameEntity> _matcherParticle;

    public static Entitas.IMatcher<GameEntity> Particle {
        get {
            if (_matcherParticle == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.Particle);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherParticle = matcher;
            }

            return _matcherParticle;
        }
    }
}
