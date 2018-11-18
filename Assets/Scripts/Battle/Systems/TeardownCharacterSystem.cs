using Entitas;

public class TeardownCharacterSystem : ITearDownSystem
{
    private IGroup<GameEntity> characterEntities;

    public TeardownCharacterSystem(GameContext context)
    {
        characterEntities = context.GetGroup(Matcher<GameEntity>.AnyOf(GameMatcher.Enemy, GameMatcher.Player));
    }

    public void TearDown()
    {
        foreach (GameEntity gameEntity in characterEntities.GetEntities())
        {
            gameEntity.Destroy();
        }
    }
}