using System;

public class CharacterChooserItemProperties : IWidgetProperties
{
    public int EntityId;
    public string ButtonText;
    public Action<int> Callback;

    public CharacterChooserItemProperties(int entityId, string buttonText, Action<int> callback)
    {
        EntityId = entityId;
        ButtonText = buttonText;
        Callback = callback;
    }
}