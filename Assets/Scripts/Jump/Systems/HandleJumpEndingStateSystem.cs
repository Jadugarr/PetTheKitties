using Entitas;
using Entitas.Scripts.Common.Systems;

public class HandleJumpEndingStateSystem : GameExecuteSystem
{
    private IGroup<GameEntity> _jumpingCharacterGroup;
    private IGroup<GameEntity> _inputEntities;

    public HandleJumpEndingStateSystem(GameContext context) : base(context)
    {
        _jumpingCharacterGroup =
            context.GetGroup(GameMatcher.AllOf(GameMatcher.JumpState, GameMatcher.CharacterVelocity));
        _inputEntities = context.GetGroup(GameMatcher.Input);
    }

    protected override bool IsInValidState()
    {
        return true;
    }

    protected override void ExecuteSystem()
    {
        foreach (GameEntity gameEntity in _jumpingCharacterGroup)
        {
            bool isHoldingJump = false;
            if (gameEntity.hasJumpState && gameEntity.jumpState != null &&
                gameEntity.jumpState.JumpState == JumpState.Jumping)
            {
                foreach (GameEntity inputEntity in _inputEntities)
                {
                    if (inputEntity.input.InputCommand == InputCommand.Jump)
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