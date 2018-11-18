using Promises;

namespace Entitas.Scripts.Common.Systems
{
    public class InitPromisesSystem : IInitializeSystem
    {
        public void Initialize()
        {
            MainThreadDispatcher.Init();
        }
    }
}