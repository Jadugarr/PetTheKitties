using Entitas.Scripts.Common.Systems;
using UnityEngine;

namespace Entitas.Position
{
    public class SyncPositionAndViewSystem : GameExecuteSystem
    {
        private IGroup<GameEntity> relevantEntities;

        public SyncPositionAndViewSystem(GameContext context) : base(context)
        {
            relevantEntities = _context.GetGroup(GameMatcher.AllOf(GameMatcher.Position, GameMatcher.View));
        }

        protected override bool IsInValidState()
        {
            return true;
        }

        protected override void ExecuteSystem()
        {
            var entities = relevantEntities.GetEntities();
            for (var i = 0; i < entities.Length; i++)
            {
                GameEntity gameEntity = entities[i];
                Vector3 viewPosition = gameEntity.view.View.transform.position;
                Vector3 entityPosition = gameEntity.position.position;

                if (Vector3.Distance(viewPosition, entityPosition) > 0f)
                {
                    gameEntity.ReplacePosition(viewPosition);
                }
            }
        }
    }
}