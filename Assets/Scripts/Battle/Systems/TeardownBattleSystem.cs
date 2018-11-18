using Entitas;

public class TeardownBattleSystem : ITearDownSystem
{
    private GameContext context;
    private IGroup<GameEntity> battleEntities;

    public TeardownBattleSystem(GameContext context)
    {
        battleEntities =
            context.GetGroup(Matcher<GameEntity>.AnyOf(GameMatcher.BattleAction));
        this.context = context;
    }

    public void TearDown()
    {
        foreach (GameEntity gameEntity in battleEntities.GetEntities())
        {
            gameEntity.Destroy();
        }

        context.RemoveWinCondition();
        context.RemoveLoseCondition();
    }
}