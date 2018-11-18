using System;

public class ActionChooserItemProperties : IWidgetProperties
{
    public ActionType ActionType;
    public string ButtonText;
    public Action<ActionType> Callback;

    public ActionChooserItemProperties(ActionType actionType, string buttonText, Action<ActionType> callback)
    {
        ActionType = actionType;
        ButtonText = buttonText;
        Callback = callback;
    }
}