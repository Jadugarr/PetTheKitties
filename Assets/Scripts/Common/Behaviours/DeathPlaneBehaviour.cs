using System;
using Entitas.Unity;
using UnityEngine;

namespace Entitas.Common.Behaviours
{
    public class DeathPlaneBehaviour : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.tag.Equals(Tags.Player))
            {
                GameEntity playerEntity = (GameEntity)other.gameObject.GetEntityLink().entity;
                playerEntity.ReplaceHealth(0);
                
//                Contexts.sharedInstance.game.isRestartLevel = true;
            }
        }
    }
}