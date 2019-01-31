using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class CharacterReachedGoalSystem : GameReactiveSystem, ICleanupSystem
{
    private IGroup<GameEntity> _goalReachedGroup;

    public CharacterReachedGoalSystem(IContext<GameEntity> context) : base(context)
    {
        _goalReachedGroup = _context.GetGroup(GameMatcher.CharacterReachedGoal);
    }

    protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
    {
        return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.CharacterReachedGoal,
            GroupEvent.Added));
    }

    protected override bool Filter(GameEntity entity)
    {
        return true;
    }

    protected override bool IsInValidState()
    {
        return _context.gameState.CurrentGameState == GameState.World &&
               _context.subState.CurrentSubState == SubState.WorldNavigation;
    }

    protected override void ExecuteSystem(List<GameEntity> entities)
    {
        foreach (GameEntity reachedGoalEntity in entities)
        {
            GameEntity characterEntity = _context.GetEntityWithId(reachedGoalEntity.characterReachedGoal.CharacterId);

            characterEntity?.view.View.GetComponent<AudioSource>().Play();

            if (characterEntity.hasFollowCharacter)
            {
                characterEntity.RemoveFollowCharacter();
            }
        }
    }

    public void Cleanup()
    {
        foreach (GameEntity gameEntity in _goalReachedGroup.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}