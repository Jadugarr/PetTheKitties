using Entitas;
using Entitas.Scripts.Common.Systems;
using Entitas.World;

public class CheckCharacterGroundStateSystem : GameExecuteSystem
{
    private IGroup<GameEntity> characterGroup;
    
    public CheckCharacterGroundStateSystem(GameContext context) : base(context)
    {
        characterGroup = context.GetGroup(GameMatcher.AllOf(GameMatcher.CharacterGroundState, GameMatcher.View));
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity characterEntity in characterGroup.GetEntities())
        {
            if (GroundCheckUtil.CheckIfCharacterOnGround(characterEntity.view.View))
            {
                characterEntity.ReplaceCharacterGroundState(CharacterGroundState.OnGround);
            }
            else
            {
                characterEntity.ReplaceCharacterGroundState(CharacterGroundState.Airborne);
            }
        }
    }
}