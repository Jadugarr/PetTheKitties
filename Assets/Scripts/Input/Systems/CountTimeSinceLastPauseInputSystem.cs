using Entitas.Scripts.Common.Systems;
using UnityEngine;

namespace Entitas.Input.Systems
{
    public class CountTimeSinceLastPauseInputSystem : GameExecuteSystem
    {
        public CountTimeSinceLastPauseInputSystem(GameContext context) : base(context)
        {
        }

        protected override bool IsInValidState()
        {
            return true;
        }

        protected override void ExecuteSystem()
        {
            float timeSinceLastPauseInput =
                _context.hasTimeSinceLastPauseInput ? _context.timeSinceLastPauseInput.Value : 0f;
            _context.ReplaceTimeSinceLastPauseInput(timeSinceLastPauseInput + Time.unscaledDeltaTime);
        }
    }
}