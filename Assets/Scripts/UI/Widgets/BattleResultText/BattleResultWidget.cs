using TMPro;
using UnityEngine;

public class BattleResultWidget : AWidget
{
    [SerializeField] private TMP_Text resultTextfield;

    public override void Open()
    {
    }

    public override void Close()
    {
    }

    public override string GetName()
    {
        return AssetTypes.BattleResultText;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    protected override void OnNewProperties()
    {
        var props = (BattleResultWidgetProperties) properties;

        resultTextfield.text = props.TextToDisplay;
    }
}