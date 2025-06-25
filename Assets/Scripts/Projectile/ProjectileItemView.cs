using System;
using UnityEngine;
using Cysharp.Threading.Tasks;
using Zenject;

namespace Projectile
{
    [RequireComponent(typeof(Rigidbody))]
    public class ProjectileItemView : MonoBehaviour
    {
        [SerializeField] private float _speed = 60f;
        [SerializeField] private float _maxLifetime = 10f;
        [SerializeField] private float _damage = 1f;

        private Rigidbody _rigidbody;
        private ProjectilePool _pool;

        private void Awake() => _rigidbody = GetComponent<Rigidbody>();

        [Inject]
        private void Construct(ProjectilePool pool)
        {
            _pool = pool;
        }

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

            float speed = currentVelocity.magnitude * 0.1f; 

            Vector3 randomDirection = new Vector3(
                UnityEngine.Random.Range(-0.2f, 0.2f), 
                UnityEngine.Random.Range(-0.2f, 0.2f), 
                UnityEngine.Random.Range(-0.2f, -0.1f)  
            ).normalized;

            _rigidbody.velocity = (currentVelocity.normalized + randomDirection * 0.3f).normalized * speed;
        }
    }
}
