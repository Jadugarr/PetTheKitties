using Entitas.Unity;
using UnityEngine;

namespace Entitas.Player
{
    public class InteractionTriggerBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            // Maybe put entity into pool, that just say which entities are currently colliding and remove it again on exit?
        }
    }
}