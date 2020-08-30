using System;
using Entitas;
using UnityEngine;

public abstract class AGameController : MonoBehaviour
{
    protected Systems updateSystems;
    protected Systems fixedUpdateSystems;
    protected Systems lateUpdateSystems;

    private GameEntity controllerEntity;

    public abstract GameControllerType GetGameControllerType();

    private void Awake()
    {
        updateSystems = new Feature("UpdateSystems");
        fixedUpdateSystems = new Feature("FixedUpdateSystems");
        lateUpdateSystems = new Feature("LateUpdateSystems");

        AfterAwake();
    }

    protected virtual void AfterAwake()
    {
        // for override
    }

    // Use this for initialization
    private void Start()
    {
        BeforeStart();

        IContext context = GetContext();
        controllerEntity = Contexts.sharedInstance.game.CreateEntity();
        controllerEntity.AddController(this);
        CreateSystems(context);

        updateSystems.Initialize();
        fixedUpdateSystems.Initialize();
        lateUpdateSystems.Initialize();

        AfterStart();
    }

    protected virtual void BeforeStart()
    {
        // for override
    }

    protected virtual void AfterStart()
    {
        // for override
    }

    private void OnDestroy()
    {
        updateSystems.ClearReactiveSystems();
        updateSystems.DeactivateReactiveSystems();
        updateSystems.Cleanup();
        updateSystems.TearDown();

        fixedUpdateSystems.ClearReactiveSystems();
        fixedUpdateSystems.DeactivateReactiveSystems();
        fixedUpdateSystems.Cleanup();
        fixedUpdateSystems.TearDown();

        lateUpdateSystems.ClearReactiveSystems();
        lateUpdateSystems.DeactivateReactiveSystems();
        lateUpdateSystems.Cleanup();
        lateUpdateSystems.TearDown();
        
        controllerEntity.Destroy();
    }

    public void RestartController()
    {
        updateSystems.ClearReactiveSystems();
        updateSystems.Cleanup();
        updateSystems.TearDown();

        fixedUpdateSystems.ClearReactiveSystems();
        fixedUpdateSystems.Cleanup();
        fixedUpdateSystems.TearDown();

        lateUpdateSystems.ClearReactiveSystems();
        lateUpdateSystems.Cleanup();
        lateUpdateSystems.TearDown();

        updateSystems.Initialize();
        fixedUpdateSystems.Initialize();
        lateUpdateSystems.Initialize();
    }

    protected abstract IContext GetContext();


    protected abstract void CreateSystems(IContext context);

    private void Update()
    {
        updateSystems.Execute();
    }

    private void FixedUpdate()
    {
        fixedUpdateSystems.Execute();
    }

    private void LateUpdate()
    {
        lateUpdateSystems.Execute();
        updateSystems.Cleanup();
        fixedUpdateSystems.Cleanup();
        lateUpdateSystems.Cleanup();
    }
}