using UnityEngine;
using UnityEngine.UI;

public class CharacterChooserItemWidget : AWidget
{
    [SerializeField] private Button chooseButton;

    [SerializeField] private Text textField;

    public override void Open()
    {
        chooseButton.onClick.AddListener(OnChooseButtonClicked);
    }

    public override void Close()
    {
        chooseButton.onClick.RemoveListener(OnChooseButtonClicked);
    }

    public override string GetName()
    {
        return AssetTypes.CharacterChooserItem;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    protected override void OnNewProperties()
    {
        textField.text = ((CharacterChooserItemProperties) properties).ButtonText;
    }

    private void OnChooseButtonClicked()
    {
        CharacterChooserItemProperties props = (CharacterChooserItemProperties) properties;

        if (props.Callback != null)
        {
            props.Callback(props.EntityId);
        }
    }
}