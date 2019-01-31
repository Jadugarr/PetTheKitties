using Entitas.Unity;
using UnityEngine;

namespace Entitas.Kitty.Behaviours
{
    public class GoalTriggerBehaviour : MonoBehaviour
    {
        private GameContext _context;

        private void Awake()
        {
            _context = Contexts.sharedInstance.game;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            EntityLink link = other.gameObject.GetEntityLink();
            if (link != null)
            {
                GameEntity entityInGoal = (GameEntity) link.entity;

                if (entityInGoal.isKitty)
                {
                    _context.CreateEntity().AddCharacterReachedGoal(entityInGoal.id.Id);
                }
            }
        }
    }
}