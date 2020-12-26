using Entitas.Unity;
using UnityEngine;

public static class GameEntityExtensions
{
    public static void DestroyEntity(this GameEntity entity)
    {
        if (entity.hasView)
        {
            entity.view.View.Unlink();
            GameObject.Destroy(entity.view.View);
        }
        
        entity.Destroy();
    }
}