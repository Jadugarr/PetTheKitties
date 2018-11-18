using UnityEngine;
using UnityEngine.UI;

public class ActionChooserItemWidget : AWidget
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
        return AssetTypes.ActionChooserItem;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    protected override void OnNewProperties()
    {
        textField.text = ((ActionChooserItemProperties) properties).ButtonText;
    }

    private void OnChooseButtonClicked()
    {
        ActionChooserItemProperties props = (ActionChooserItemProperties) properties;

        if (props.Callback != null)
        {
            props.Callback(props.ActionType);
        }
    }
}