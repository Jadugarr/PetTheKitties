using System.Collections.Generic;
using Configurations;
using Entitas;
using Entitas.World;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CharacterHitGroundSystem : GameReactiveSystem
{
    public CharacterHitGroundSystem(IContext<GameEntity> context) : base(context)
    {
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CharacterGroundState,
            GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCharacterGroundState && entity.hasPosition &&
               (entity.characterGroundState.Value == CharacterGroundState.OnGround
                || entity.characterGroundState.Value == CharacterGroundState.OnSlopeAhead
                || entity.characterGroundState.Value == CharacterGroundState.OnSlopeBehind);
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity gameEntity in entities)
        {
            Vector3 currentPos = gameEntity.position.position;
            Vector3 newPosition = new Vector3(currentPos.x,
                currentPos.y - Mathf.Abs(gameEntity.distanceToGround.Value), currentPos.z);

            // spawn particles
            AssetReference particleReference = GameConfigurations.AssetReferenceConfiguration.HitGroundParticles;
            Addressables.InstantiateAsync(particleReference, newPosition, Quaternion.identity).Completed += handle =>
            {
                GameEntity particleEntity = _context.CreateEntity();
                ParticleSystem particles = handle.Result.GetComponent<ParticleSystem>();
                //particleEntity.AddPosition(newPosition);
                //particleEntity.AddView(handle.Result);
                particleEntity.AddParticle(particles);
                particles.Play();
            };
        }
    }
}