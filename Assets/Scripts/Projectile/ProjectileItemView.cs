using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileItemView : MonoBehaviour, IPoolable<IMemoryPool>
    {
        [SerializeField] float _speed = 60f;
        [SerializeField] float _maxLifetime= 5f;
        [SerializeField] float _damage = 1f;

        private Rigidbody _rigidbody;
        private IMemoryPool _pool;

        public class Factory : PlaceholderFactory<ProjectileItemView> { }

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        public void Launch(Vector3 origin, Vector3 dir)
        {
            transform.position = origin;
            _rigidbody.velocity = dir * _speed;
            AutoRelease().Forget();
        }

        private async UniTaskVoid AutoRelease()
        {
            await UniTask.Delay(TimeSpan.FromSeconds(_maxLifetime));
            _pool.Despawn(this);
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IDamageable dmg))
            {
                dmg.TakeDamage(_damage);
                ChangeVelocity();
            }
        }

        private void ChangeVelocity()
        {
            Vector3 currentVelocity = _rigidbody.velocity;
            float speed = currentVelocity.magnitude * 0.5f; 

            Vector3 randomDirection = new Vector3(
                UnityEngine.Random.Range(-0.2f, 0.2f), 
                UnityEngine.Random.Range(-0.2f, 0.2f), 
                UnityEngine.Random.Range(-0.2f, -0.1f)  
            ).normalized;

            _rigidbody.velocity = (currentVelocity.normalized + randomDirection * 0.3f).normalized * speed;
        }

        public void OnDespawned() => _rigidbody.velocity = Vector3.zero;
        public void OnSpawned(IMemoryPool pool) => _pool = pool;
    }
}
