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
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(
            GameMatcher.AllOf(GameMatcher.CharacterGroundState, GameMatcher.PreviousCharacterGroundState),
            GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return entity.hasCharacterGroundState && entity.hasPosition &&
               IsValidGroundState(entity.characterGroundState.Value) && !IsValidGroundState(entity.previousCharacterGroundState.Value);
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    private bool IsValidGroundState(CharacterGroundState stateToCheck)
    {
        return (stateToCheck == CharacterGroundState.OnGround
                || stateToCheck == CharacterGroundState.OnSlopeAhead
                || stateToCheck == CharacterGroundState.OnSlopeBehind);
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