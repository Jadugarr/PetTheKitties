using System;
using System.Collections.Generic;
using Entitas;
using UnityEngine;

public class ATBBarWidget : AWidget
{
    [SerializeField] private GameObject StartMarker;
    [SerializeField] private GameObject ChooseMarker;
    [SerializeField] private GameObject ActMarker;

    // Distance between ATB start point and point where characters can choose an action
    private float chooseCommandDistance = 0f;

    // Distance between chosen action marker and actual execution marker
    private float executeActionDistance = 0f;

    private GameContext context;
    private IGroup<GameEntity> battleEntityGroup;
    private IGroup<GameEntity> executionTimeEntityGroup;
    private List<ATBItemWidget> atbItems = new List<ATBItemWidget>(6);
    private GameObject itemPrefab;

    public override void Open()
    {
        itemPrefab = UIService.GetAsset(AssetTypes.AtbItem);
        chooseCommandDistance = ChooseMarker.transform.localPosition.x - StartMarker.transform.localPosition.x;
        executeActionDistance = ActMarker.transform.localPosition.x - ChooseMarker.transform.localPosition.x;

        context = Contexts.sharedInstance.game;
        battleEntityGroup = context.GetGroup(GameMatcher.Battle);
        executionTimeEntityGroup = context.GetGroup(GameMatcher.ExecutionTime);

        battleEntityGroup.OnEntityAdded += OnBattleEntityAdded;
        battleEntityGroup.OnEntityRemoved += OnBattleEntityRemoved;
        executionTimeEntityGroup.OnEntityUpdated += OnExecutionTimeUpdated;
    }

    public override void Close()
    {
        DestroyItems();
        battleEntityGroup.OnEntityAdded -= OnBattleEntityAdded;
        battleEntityGroup.OnEntityRemoved -= OnBattleEntityRemoved;
        executionTimeEntityGroup.OnEntityUpdated -= OnExecutionTimeUpdated;

        battleEntityGroup = null;
        executionTimeEntityGroup = null;
    }

    public override string GetName()
    {
        return AssetTypes.Atb;
    }

    public override UiComponentType GetComponentType()
    {
        return UiComponentType.Dynamic;
    }

    protected override void OnNewProperties()
    {
        DestroyItems();
        InitItems();
    }

    private void DestroyItems()
    {
        for (int i = atbItems.Count - 1; i >= 0; i--)
        {
            atbItems[i].Close();
            Destroy(atbItems[i].gameObject);
        }

        atbItems.Clear();
    }

    private void OnExecutionTimeUpdated(IGroup<GameEntity> @group, GameEntity actionEntity, int index,
        IComponent previousComponent, IComponent newComponent)
    {
        ATBItemWidget linkedItem = GetLinkedItem(actionEntity.battleAction.EntityId);

        if (linkedItem == null)
        {
            Debug.LogError("No linked item found for action id: " + actionEntity.battleAction.EntityId);
        }

        ExecutionTimeComponent executionTimeComponent = actionEntity.executionTime;
        float progressPercentage = 1 - (executionTimeComponent.RemainingTime / executionTimeComponent.TotalTime);

        if (actionEntity.battleAction.ActionAtbType == ActionATBType.Waiting)
        {
            linkedItem.gameObject.transform.localPosition = new Vector3(Mathf.Clamp(
                    StartMarker.transform.localPosition.x +
                    (chooseCommandDistance * progressPercentage), StartMarker.transform.localPosition.x,
                    ChooseMarker.transform.localPosition.x),
                linkedItem.transform.localPosition.y, linkedItem.transform.localPosition.z);
        }
        else if (actionEntity.battleAction.ActionAtbType == ActionATBType.Acting)
        {
            linkedItem.gameObject.transform.localPosition = new Vector3(
                Mathf.Clamp(ChooseMarker.transform.localPosition.x +
                            (executeActionDistance * progressPercentage), ChooseMarker.transform.localPosition.x,
                    ActMarker.transform.localPosition.x),
                linkedItem.transform.localPosition.y, linkedItem.transform.localPosition.z);
        }
        else
        {
            Debug.LogWarning("Not handling atb type: " + actionEntity.battleAction.ActionAtbType);
        }
    }

    private void InitItems()
    {
        foreach (GameEntity gameEntity in battleEntityGroup)
        {
            CreateNewItem(gameEntity);
        }
    }

    private void CreateNewItem(GameEntity gameEntity)
    {
        ATBItemProperties newProps;

        if (gameEntity.hasBattleImage)
        {
            newProps = new ATBItemProperties(gameEntity.battleImage.BattleImage, gameEntity.id.Id);
        }
        else
        {
            newProps = new ATBItemProperties(null, gameEntity.id.Id);
        }

        ATBItemWidget newItem = GameObject.Instantiate(itemPrefab, gameObject.transform)
            .GetComponent<ATBItemWidget>();
        Vector3 itemPosition = new Vector3(StartMarker.transform.position.x, newItem.gameObject.transform.position.y,
            newItem.gameObject.transform.position.z);
        newItem.gameObject.transform.SetPositionAndRotation(itemPosition,
            StartMarker.transform.rotation);
        newItem.Open();
        newItem.ApplyProperties(newProps);
        atbItems.Add(newItem);
    }

    private ATBItemWidget GetLinkedItem(int entityId)
    {
        foreach (ATBItemWidget atbItemWidget in atbItems)
        {
            if (atbItemWidget.GetLinkedCharacterId() == entityId)
            {
                return atbItemWidget;
            }
        }

        return null;
    }

    private void OnBattleEntityRemoved(IGroup<GameEntity> group, GameEntity entity, int index,
        IComponent component)
    {
        ATBItemWidget item = GetLinkedItem(entity.id.Id);
        item.Close();
        atbItems.Remove(item);
        Destroy(item.gameObject);
    }

    private void OnBattleEntityAdded(IGroup<GameEntity> group, GameEntity entity, int index,
        IComponent component)
    {
        CreateNewItem(entity);
    }
}