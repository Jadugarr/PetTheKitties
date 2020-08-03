using System;
using UnityEngine;

namespace Entitas.Controllers
{
    public abstract class AGameController : MonoBehaviour
    {
        protected Systems updateSystems;
        protected Systems fixedUpdateSystems;
        protected Systems lateUpdateSystems;

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
            
            CreateSystems(GetContext());
            
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
        }

        protected abstract IContext GetContext();

        protected abstract void CreateSystems(IContext context);

        // Update is called once per frame
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
}