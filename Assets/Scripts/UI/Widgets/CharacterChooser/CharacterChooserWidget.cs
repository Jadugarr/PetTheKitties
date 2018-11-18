using System.Collections.Generic;
using UnityEngine;

public class CharacterChooserWidget : AWidget
{
    private List<CharacterChooserItemWidget> chooseItems = new List<CharacterChooserItemWidget>();
    private GameObject chooseItemPrefab;

    public override void Open()
    {
    }

    public override void Close()
    {
        foreach (CharacterChooserItemWidget characterChooserItemWidget in chooseItems)
        {
            characterChooserItemWidget.Close();
        }
    }

    protected override void OnShow()
    {
        foreach (CharacterChooserItemWidget characterChooserItemWidget in chooseItems)
        {
            characterChooserItemWidget.Show();
        }
    }

    protected override void OnHide()
    {
        foreach (CharacterChooserItemWidget characterChooserItemWidget in chooseItems)
        {
            characterChooserItemWidget.Hide();
        }
    }

    protected override void OnNewProperties()
    {
        DestroyItems();

        CharacterChooserProperties props = (CharacterChooserProperties) properties;

        if (chooseItemPrefab == null)
        {
            chooseItemPrefab = UIService.GetAsset(AssetTypes.CharacterChooserItem);
        }

        foreach (int possibleEntityId in props.PossibleEntityIds)
        {
            CharacterChooserItemWidget newItem = Instantiate(chooseItemPrefab, gameObject.transform)
                .GetComponent<CharacterChooserItemWidget>();
            newItem.Open();
            newItem.ApplyProperties(
                new CharacterChooserItemProperties(possibleEntityId, possibleEntityId.ToString(), OnItemClicked));
            chooseItems.Add(newItem);
        }
    }

    public override string GetName()
    {
        return AssetTypes.CharacterChooser;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Static;
    }

    private void DestroyItems()
    {
        if (chooseItems.Count > 0)
        {
            for (int i = chooseItems.Count - 1; i >= 0; i--)
            {
                chooseItems[i].Close();
                Destroy(chooseItems[i].gameObject);
            }

            chooseItems.Clear();
        }
    }

    private void OnItemClicked(int chosenEntityId)
    {
        CharacterChooserProperties props = (CharacterChooserProperties) properties;
        props.ActionEntity.AddTarget(chosenEntityId);
        UIService.HideWidget(AssetTypes.CharacterChooser);
    }
}