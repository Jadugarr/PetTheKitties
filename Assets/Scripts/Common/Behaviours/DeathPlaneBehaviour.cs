using System;
using UnityEngine;

namespace Entitas.Common.Behaviours
{
    public class DeathPlaneBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals(Tags.Player))
            {
                Contexts.sharedInstance.game.isRestartLevel = true;
            }
        }
    }
}