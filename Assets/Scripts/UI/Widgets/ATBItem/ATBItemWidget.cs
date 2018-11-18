using UnityEngine;
using UnityEngine.UI;

public class ATBItemWidget : AWidget
{
    [SerializeField] public Image CharacterImage;

    public override void Open()
    {
    }

    public override void Close()
    {
        CharacterImage = null;
    }

    public override string GetName()
    {
        return AssetTypes.AtbItem;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Dynamic;
    }

    public int GetLinkedCharacterId()
    {
        return ((ATBItemProperties) properties).CharacterId;
    }

    protected override void OnNewProperties()
    {
        ATBItemProperties props = (ATBItemProperties) properties;

        CharacterImage.sprite = props.CharacterSprite;
    }
}