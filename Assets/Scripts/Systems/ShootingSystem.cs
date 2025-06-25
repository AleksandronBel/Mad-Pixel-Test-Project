using MessagePipe;
using Player;
using Projectile;
using R3;
using UnityEngine;
using Zenject;
using static Messages.Messages;

namespace Systems
{
    public class ShootingSystem : MonoBehaviour
    {
        [Inject] private ISubscriber<ShootRequest> _shootSubscriber;
        [Inject] private IPublisher<PlayerShoot> _playerShoot;

        [SerializeField] private Camera _camera;

        private PlayerStats _playerState;
        private ProjectilePool _projectilePool;
        private Vector3 _startedProjectileOffset = new Vector3(0f, 0f, 3f);

        [Inject]
        private void Construct(PlayerStats state, ProjectilePool projectilePool)
        {
            _playerState = state;
            _projectilePool = projectilePool;

            _shootSubscriber.Subscribe(message => OnShootRequested(message.Position)).AddTo(this);
        }

        private void OnShootRequested(Vector3 position)
        {
            var ray = _camera.ScreenPointToRay(position);

            if (_playerState.Ammo.CurrentValue <= 0) return;

            _playerShoot.Publish(new());

            _projectilePool.Spawn(_camera.transform.position + _startedProjectileOffset, ray.direction);
        }
    }
}
