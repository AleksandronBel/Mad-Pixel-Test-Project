using UnityEngine;
using Zenject;

namespace Projectile
{
    public class ProjectilePool : MonoMemoryPool<Vector3, Vector3, ProjectileItemView>
    {
        protected override void Reinitialize(Vector3 origin, Vector3 direction, ProjectileItemView item)
        {
            item.Launch(origin, direction);
        }

        protected override void OnDespawned(ProjectileItemView item)
        {
            item.gameObject.SetActive(false);
        }

        protected override void OnSpawned(ProjectileItemView item)
        {
            item.gameObject.SetActive(true);
        }
    }
}
