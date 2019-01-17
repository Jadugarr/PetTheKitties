using System.Collections.Generic;
using UnityEngine;

namespace Entitas.World.Systems
{
    public class ProcessJumpInputSystem : GameReactiveSystem
    {
        private IGroup<GameEntity> _playerGroup;
        
        public ProcessJumpInputSystem(IContext<GameEntity> context) : base(context)
        {
            _playerGroup = _context.GetGroup(GameMatcher.Player);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context)
        {
            return context.CreateCollector(new TriggerOnEvent<GameEntity>(GameMatcher.Input, GroupEvent.Added));
        }

        protected override bool Filter(GameEntity entity)
        {
            return entity.input.InputCommand == InputCommand.Jump && _context.isJumpInputAvailable;
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.World &&
                   _context.subState.CurrentSubState == SubState.WorldNavigation;
        }

        protected override void ExecuteSystem(List<GameEntity> entities)
        {
            GameEntity playerEntity = _playerGroup.GetSingleEntity();
            GameObject playerView = playerEntity.view.View;

            if (playerView)
            {
                float distanceToGround = playerView.GetComponent<Collider2D>().bounds.extents.y;
                RaycastHit2D hit =
                    Physics2D.Raycast(
                        new Vector2(playerView.transform.position.x,
                            playerView.transform.position.y - distanceToGround - 0.01f), Vector2.down, 0.1f);

                if (hit.collider != null)
                {
                    Debug.Log("Tag of hit target: " + hit.collider.gameObject.tag);

                    if (hit.collider.gameObject.tag.Equals(Tags.Ground))
                    {
                        playerView.GetComponent<Rigidbody2D>().velocity = new Vector2(0f, 5f);
                    }
                }
            }
        }
    }
}