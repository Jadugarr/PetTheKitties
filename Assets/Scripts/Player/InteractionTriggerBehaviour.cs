using Entitas.Unity;
using UnityEngine;

namespace Entitas.Player
{
    public class InteractionTriggerBehaviour : MonoBehaviour
    {
        private GameContext _context;

        private void Awake()
        {
            _context = Contexts.sharedInstance.game;
        }

        private void OnDestroy()
        {
            if (_context.hasPlayerInteraction)
            {
                _context.RemovePlayerInteraction();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EntityLink entityLink = other.gameObject.GetEntityLink();

            if (entityLink != null)
            {
                GameEntity gameEntity = entityLink.entity as GameEntity;

                if (gameEntity != null)
                {
                    if (gameEntity.isInteractable)
                    {
                        _context.ReplacePlayerInteraction(gameEntity.id.Id);
                    }
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            // Problem: If there are two or more objects in interaction range and one gets removed, the player can't interact
            // with the other object unless they move out and back in again
            if (_context.hasPlayerInteraction)
            {
                _context.RemovePlayerInteraction();
            }
        }
    }
}