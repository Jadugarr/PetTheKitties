using Entitas;

public class CharacterChooserProperties : IWidgetProperties
{
    public int[] PossibleEntityIds;
    public GameContext Context;
    public GameEntity ActionEntity;

    public CharacterChooserProperties(int[] possibleEntityIds, GameContext context, GameEntity actionEntity)
    {
        PossibleEntityIds = possibleEntityIds;
        Context = context;
        ActionEntity = actionEntity;
    }
}