using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ValueDisplayWidget : AWidget
{
    [SerializeField] private Image valueImage;
    [SerializeField] private TMP_Text valueText;

    public override void Open()
    {
        
    }

    public override void Close()
    {
        
    }

    public override string GetName()
    {
        return UiAssetTypes.KittyAmountDisplay;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }
}
