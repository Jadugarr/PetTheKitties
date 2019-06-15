using Entitas;
using Entitas.Scripts.Common.Systems;

public class HandleJumpEndingStateSystem : GameExecuteSystem
{
    private readonly IGroup<GameEntity> jumpingCharacterGroup;
    private readonly IGroup<GameEntity> jumpCommandEntities;

    public HandleJumpEndingStateSystem(GameContext context) : base(context)
    {
        jumpingCharacterGroup =
            context.GetGroup(GameMatcher.AllOf(GameMatcher.JumpState, GameMatcher.CharacterVelocity));
        jumpCommandEntities = context.GetGroup(GameMatcher.JumpCharacter);
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity gameEntity in jumpingCharacterGroup)
        {
            bool isHoldingJump = false;
            if (gameEntity.hasJumpState && gameEntity.jumpState != null &&
                gameEntity.jumpState.JumpState == JumpState.Jumping)
            {
                foreach (GameEntity commandEntity in jumpCommandEntities)
                {
                    if (commandEntity.jumpCharacter.JumpEntityId == gameEntity.id.Id)
                    {
                        isHoldingJump = true;
                        break;
                    }
                }

                if (!isHoldingJump)
                {
                    gameEntity.ReplaceJumpState(JumpState.JumpEnding);
                }
            }
        }
    }
}