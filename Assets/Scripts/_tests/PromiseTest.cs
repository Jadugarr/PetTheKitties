using System;
using System.Collections;
using Promises;
using UnityEngine;

namespace Entitas._tests
{
    public class PromiseTest : MonoBehaviour
    {
        private Deferred<int> promise;
        private Deferred<int> thenPromise;

        private int controlValue = 0;

        void Start()
        {
            MainThreadDispatcher.Init();
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                var testPromise = Promise.WithCoroutine<int>(Coroutine);
                testPromise.OnFulfilled += OnFulfilled;
                testPromise.OnFailed += OnFailed;
            }

            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                IncreaseValue();
                //FulfillPromise();
            }

            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                FailPromise();
            }

            if (Input.GetKeyDown(KeyCode.Alpha4))
            {
                FailPromise();
            }
        }

        private void IncreaseValue()
        {
            controlValue++;
            Debug.Log("Current value: " + controlValue);
        }

        private IEnumerator Coroutine()
        {
            while (controlValue < 5)
            {
                yield return 0;
            }

            yield return 10;
        }

        private void OnFailed(Exception error)
        {
            Debug.Log(error.Message);
        }

        private Deferred<int> StartPromiseSetup()
        {
            promise = new Deferred<int>();
            promise.action = () =>
            {
                Debug.Log("Start promise logic gets executed now!");
                return 0;
            };
            Debug.Log("Start promise setup complete");
            return promise;
        }

        private Deferred<int> NextPromiseSetup()
        {
            thenPromise = new Deferred<int>();
            thenPromise.action = () =>
            {
                Debug.Log("Next promise logic gets executed now!");
                return 0;
            };
            Debug.Log("Next promise setup complete");
            return thenPromise;
        }

        private void FulfillPromise()
        {
            promise.Fulfill(5);
        }

        private void FulfillThenPromise()
        {
            thenPromise.Fulfill(5);
        }

        private void FailPromise()
        {
            promise.Fail(new Exception("Failed start promise"));
        }

        private void OnFulfilled(int result)
        {
            Debug.Log("Everything Fulfilled: " + result);
        }
    }
}