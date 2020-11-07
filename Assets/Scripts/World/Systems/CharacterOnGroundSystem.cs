using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;
using UnityEngine;

public class CharacterOnGroundSystem : GameExecuteSystem
{
    private IGroup<GameEntity> charactersOnGround;

    public CharacterOnGroundSystem(GameContext context) : base(context)
    {
        charactersOnGround = context.GetGroup(GameMatcher.CharacterGroundState);
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity gameEntity in charactersOnGround.GetEntities())
        {
            if (gameEntity.characterGroundState.Value == CharacterGroundState.OnGround)
            {
                // put character directly on the ground
                Vector3 currentPos = gameEntity.position.position;
                Vector3 newPosition = new Vector3(currentPos.x,
                    currentPos.y - Mathf.Abs(gameEntity.distanceToGround.Value), currentPos.z);
                gameEntity.ReplacePosition(newPosition);
            }
        }
    }
}