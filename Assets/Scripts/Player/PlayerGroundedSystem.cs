using Entitas.Scripts.Common.Systems;
using UnityEngine;

namespace Entitas.Player
{
    public class PlayerGroundedSystem : GameExecuteSystem
    {
        private IGroup<GameEntity> _playerGroup;

        public PlayerGroundedSystem(GameContext context) : base(context)
        {
            _playerGroup = _context.GetGroup(GameMatcher.Player);
        }

        protected override bool IsInValidState()
        {
            return _context.gameState.CurrentGameState == GameState.World &&
                   _context.subState.CurrentSubState == SubState.WorldNavigation;
        }

        protected override void ExecuteSystem()
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
                Debug.Log("Tag of hit target: " + hit.collider.gameObject.tag);
            }
        }
    }
}